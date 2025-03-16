using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using pokenae.Commons.Entities;

namespace pokenae.Commons.Repositories
{
    /// <summary>
    /// 共通のリポジトリインターフェース
    /// </summary>
    /// <typeparam name="T">エンティティの型</typeparam>
    public interface IEntityRepository<T, TContext> 
        where T : BaseEntity
        where TContext : DbContext
    {
        /// <summary>
        /// 特定の条件に一致するエンティティを取得
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>エンティティ</returns>
        T Find(Func<T, bool> predicate);

        /// <summary>
        /// 全てのエンティティを取得
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// 新しいエンティティを追加
        /// </summary>
        /// <param name="entity">エンティティ</param>
        void Add(T entity);

        /// <summary>
        /// 既存のエンティティを更新
        /// </summary>
        /// <param name="entity">エンティティ</param>
        void Update(T entity);

        /// <summary>
        /// エンティティを削除
        /// </summary>
        /// <param name="entity">エンティティ</param>
        void Delete(T entity);

        /// <summary>
        /// エンティティをアップサート（存在しない場合は追加し、存在する場合は更新）
        /// </summary>
        /// <param name="entity">エンティティ</param>
        void Upsert(T entity, Func<T, bool> predicate);

        /// <summary>
        /// 論理削除されたエンティティを含めて全てのエンティティを取得
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        IEnumerable<T> GetAllIncludingDeleted();

        /// <summary>
        /// 論理削除されたエンティティを含めて特定の条件に一致するエンティティを取得
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>エンティティ</returns>
        T FindIncludingDeleted(Func<T, bool> predicate);

        /// <summary>
        /// 特定の条件に一致するエンティティが存在するか確認
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>存在する場合は true、それ以外は false</returns>
        bool IsExists(Func<T, bool> predicate);

        /// <summary>
        /// 複数のエンティティを一度に追加
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に更新
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に削除
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// トランザクションを開始
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// トランザクションをコミット
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// トランザクションをロールバック
        /// </summary>
        void RollbackTransaction();
    }
}
