using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Routing;
using System.ComponentModel.DataAnnotations;

namespace pokenae.Commons.Data
{
    /// <summary>
    /// アプリケーションのデータベースコンテキスト
    /// </summary>
    public class ApplicationDbContext<TContext> : DbContext where TContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="options">データベースコンテキストのオプション</param>
        /// <param name="httpContextAccessor">HTTPコンテキストアクセサ</param>
        public ApplicationDbContext(DbContextOptions<TContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// モデルの作成時に呼び出されるメソッド
        /// </summary>
        /// <param name="modelBuilder">モデルビルダー</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 論理削除のフィルタリング
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
                }
            }
        }

        /// <summary>
        /// 論理削除のフィルタリング条件を取得するメソッド
        /// </summary>
        /// <param name="type">エンティティの型</param>
        /// <returns>論理削除のフィルタリング条件</returns>
        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var param = Expression.Parameter(type, "e");
            var prop = Expression.Property(param, "DeletedAt");
            var condition = Expression.Equal(prop, Expression.Constant(null));
            var lambda = Expression.Lambda(condition, param);
            return lambda;
        }

        /// <summary>
        /// 物理削除を行うメソッド
        /// </summary>
        /// <param name="entity">削除するエンティティ</param>
        public void HardDelete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Set<TEntity>().Remove(entity);
            SaveChanges();
        }

        /// <summary>
        /// 変更を保存するメソッド
        /// </summary>
        /// <returns>保存されたエンティティの数</returns>
        public override int SaveChanges()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";
            var controllerName = _httpContextAccessor.HttpContext?.GetRouteData()?.Values["controller"]?.ToString() ?? "UnknownController";
            var actionName = _httpContextAccessor.HttpContext?.GetRouteData()?.Values["action"]?.ToString() ?? "UnknownAction";
            var programId = $"{controllerName}-{actionName}";

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    try
                    {
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.CreatedProgramId = programId;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId;
                        entry.Entity.UpdatedProgramId = programId;
                        base.SaveChanges();
                    }
                    catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("duplicate key") == true)
                    {
                        // 同一キーのエンティティが存在する場合、論理削除されたエンティティを復活させる
                        entry.State = EntityState.Modified;
                        entry.Entity.DeletedAt = null;
                        entry.Entity.DeletedBy = null;
                        entry.Entity.DeletedProgramId = null;
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId;
                        entry.Entity.UpdatedProgramId = programId;
                        base.SaveChanges();
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = userId;
                    entry.Entity.UpdatedProgramId = programId;
                    entry.Entity.Version++;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userId;
                    entry.Entity.DeletedProgramId = programId;
                }
            }

            return base.SaveChanges();
        }
    }
}
