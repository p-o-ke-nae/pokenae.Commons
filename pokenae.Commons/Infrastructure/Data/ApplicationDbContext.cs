using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Domain.DTOs;
using pokenae.Commons.Domain.Entities;
using pokenae.Commons.Exceptions;
using System.Linq.Expressions;
using System.Security.Claims;

namespace pokenae.Commons.Infrastructure.Data
{
    /// <summary>
    /// �A�v���P�[�V�����̃f�[�^�x�[�X�R���e�L�X�g
    /// </summary>
    public class ApplicationDbContext<TContext> : DbContext where TContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="options">�f�[�^�x�[�X�R���e�L�X�g�̃I�v�V����</param>
        /// <param name="httpContextAccessor">HTTP�R���e�L�X�g�A�N�Z�T</param>
        public ApplicationDbContext(DbContextOptions<TContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// ���f���̍쐬���ɌĂяo����郁�\�b�h
        /// </summary>
        /// <param name="modelBuilder">���f���r���_�[</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // �_���폜�̃t�B���^�����O
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
                }
            }
        }

        /// <summary>
        /// �_���폜�̃t�B���^�����O�������擾���郁�\�b�h
        /// </summary>
        /// <param name="type">�G���e�B�e�B�̌^</param>
        /// <returns>�_���폜�̃t�B���^�����O����</returns>
        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var param = Expression.Parameter(type, "e");
            var prop = Expression.Property(param, "DeletedAt");
            var condition = Expression.Equal(prop, Expression.Constant(null));
            var lambda = Expression.Lambda(condition, param);
            return lambda;
        }

        /// <summary>
        /// �����폜���s�����\�b�h
        /// </summary>
        /// <param name="entity">�폜����G���e�B�e�B</param>
        public void HardDelete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Set<TEntity>().Remove(entity);
            SaveChanges();
        }


        public override int SaveChanges()
        {
            return SaveChangesAsync(false).GetAwaiter().GetResult();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(true, cancellationToken);
        }

        private async Task<int> SaveChangesAsync(bool isAsync, CancellationToken cancellationToken = default)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "system";
            var controllerName = _httpContextAccessor.HttpContext?.GetRouteData()?.Values["controller"]?.ToString() ?? "UnknownController";
            var actionName = _httpContextAccessor.HttpContext?.GetRouteData()?.Values["action"]?.ToString() ?? "UnknownAction";
            var programId = $"{controllerName}-{actionName}";

            foreach (var entry in ChangeTracker.Entries<InfrastructureDto>())
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

                        if (isAsync)
                        {
                            await base.SaveChangesAsync(cancellationToken);
                        }
                        else
                        {
                            base.SaveChanges();
                        }
                    }
                    catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("duplicate key") == true)
                    {
                        throw new DuplicateKeyException("An entity with the same key already exists.");
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    var databaseValues = isAsync ? await entry.GetDatabaseValuesAsync(cancellationToken) : entry.GetDatabaseValues();
                    if (databaseValues != null)
                    {
                        var databaseVersion = (int?)databaseValues["Version"];
                        if (databaseVersion.HasValue && entry.Entity.Version != databaseVersion.Value)
                        {
                            throw new ConcurrencyException("The entity you are trying to update has been modified by another user.");
                        }
                    }

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

            return isAsync ? await base.SaveChangesAsync(cancellationToken) : base.SaveChanges();
        }
    }
}
