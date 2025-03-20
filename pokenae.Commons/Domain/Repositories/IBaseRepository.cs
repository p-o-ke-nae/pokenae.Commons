using pokenae.Commons.Domain.DTOs;

namespace pokenae.Commons.Domain.Repositories
{
    /// <summary>
    /// 共通のリポジトリインターフェース
    /// </summary>
    /// <typeparam name="T">エンティティの型</typeparam>
    public interface IBaseRepository<T>
        where T : InfrastructureDto
    {
        /// <summary>
        /// 特定の条件に一致するエンティティを取得
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>エンティティ</returns>
        T Find(Func<T, bool> predicate);

        /// <summary>
        /// 特定の条件に一致するエンティティを非同期で取得
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>エンティティ</returns>
        Task<T> FindAsync(Func<T, bool> predicate);

        /// <summary>
        /// 全てのエンティティを取得
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// 全てのエンティティを非同期で取得
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// 新しいエンティティを追加
        /// </summary>
        /// <param name="entity">エンティティ</param>
        void Add(T entity);

        /// <summary>
        /// 新しいエンティティを非同期で追加
        /// </summary>
        /// <param name="entity">エンティティ</param>
        Task AddAsync(T entity);

        /// <summary>
        /// 既存のエンティティを更新
        /// </summary>
        /// <param name="entity">エンティティ</param>
        void Update(T entity);

        /// <summary>
        /// 既存のエンティティを非同期で更新
        /// </summary>
        /// <param name="entity">エンティティ</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// エンティティを削除
        /// </summary>
        /// <param name="entity">エンティティ</param>
        void Delete(T entity);

        /// <summary>
        /// エンティティを非同期で削除
        /// </summary>
        /// <param name="entity">エンティティ</param>
        Task DeleteAsync(T entity);

        /// <summary>
        /// エンティティをアップサート（存在しない場合は追加し、存在する場合は更新）
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <param name="predicate">条件</param>
        void Upsert(T entity, Func<T, bool> predicate);

        /// <summary>
        /// エンティティを非同期でアップサート（存在しない場合は追加し、存在する場合は更新）
        /// </summary>
        /// <param name="entity">エンティティ</param>
        /// <param name="predicate">条件</param>
        Task UpsertAsync(T entity, Func<T, bool> predicate);

        /// <summary>
        /// 論理削除されたエンティティを含めて全てのエンティティを取得
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        IEnumerable<T> GetAllIncludingDeleted();

        /// <summary>
        /// 論理削除されたエンティティを含めて全てのエンティティを非同期で取得
        /// </summary>
        /// <returns>エンティティのリスト</returns>
        Task<IEnumerable<T>> GetAllIncludingDeletedAsync();

        /// <summary>
        /// 論理削除されたエンティティを含めて特定の条件に一致するエンティティを取得
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>エンティティ</returns>
        T FindIncludingDeleted(Func<T, bool> predicate);

        /// <summary>
        /// 論理削除されたエンティティを含めて特定の条件に一致するエンティティを非同期で取得
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>エンティティ</returns>
        Task<T> FindIncludingDeletedAsync(Func<T, bool> predicate);

        /// <summary>
        /// 特定の条件に一致するエンティティが存在するか確認
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>存在する場合は true、それ以外は false</returns>
        bool IsExists(Func<T, bool> predicate);

        /// <summary>
        /// 特定の条件に一致するエンティティが存在するか非同期で確認
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>存在する場合は true、それ以外は false</returns>
        Task<bool> IsExistsAsync(Func<T, bool> predicate);

        /// <summary>
        /// 複数のエンティティを一度に追加
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に非同期で追加
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        Task AddRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に更新
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に非同期で更新
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        Task UpdateRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に削除
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        void DeleteRange(IEnumerable<T> entities);

        /// <summary>
        /// 複数のエンティティを一度に非同期で削除
        /// </summary>
        /// <param name="entities">エンティティのリスト</param>
        Task DeleteRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// トランザクションを開始
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// トランザクションを非同期で開始
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// トランザクションをコミット
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// トランザクションを非同期でコミット
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// トランザクションをロールバック
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// トランザクションを非同期でロールバック
        /// </summary>
        Task RollbackTransactionAsync();
    }
}
