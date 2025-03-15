using pokenae.Commons.DTOs;
using System.Collections.Generic;

namespace pokenae.Commons.Services.Application
{
    /// <summary>
    /// アプリケーション層のサービスインターフェース。
    /// DTOを使用してデータの授受を行います。
    /// </summary>
    /// <typeparam name="TDto">DTOの型</typeparam>
    public interface IApplicationService<TDto> where TDto : BaseDto
    {
        /// <summary>
        /// 指定された条件に一致するDTOを取得します。
        /// </summary>
        /// <param name="predicate">条件を指定する関数</param>
        /// <returns>条件に一致するDTO</returns>
        TDto Find(Func<TDto, bool> predicate);

        /// <summary>
        /// すべてのDTOを取得します。
        /// </summary>
        /// <returns>DTOのコレクション</returns>
        IEnumerable<TDto> GetAll();

        /// <summary>
        /// 新しいDTOを追加します。
        /// </summary>
        /// <param name="dto">追加するDTO</param>
        void Add(TDto dto);

        /// <summary>
        /// DTOを更新します。
        /// </summary>
        /// <param name="dto">更新するDTO</param>
        void Update(TDto dto);

        /// <summary>
        /// DTOを削除します。
        /// </summary>
        /// <param name="dto">削除するDTO</param>
        void Delete(TDto dto);

        /// <summary>
        /// DTOを追加または更新します。
        /// </summary>
        /// <param name="dto">追加または更新するDTO</param>
        /// <param name="predicate">条件を指定する関数</param>
        void Upsert(TDto dto, Func<TDto, bool> predicate);

        /// <summary>
        /// 削除されたものを含むすべてのDTOを取得します。
        /// </summary>
        /// <returns>DTOのコレクション</returns>
        IEnumerable<TDto> GetAllIncludingDeleted();

        /// <summary>
        /// 削除されたものを含む、指定された条件に一致するDTOを取得します。
        /// </summary>
        /// <param name="predicate">条件を指定する関数</param>
        /// <returns>条件に一致するDTO</returns>
        TDto FindIncludingDeleted(Func<TDto, bool> predicate);

        /// <summary>
        /// 指定された条件に一致するDTOが存在するかどうかを確認します。
        /// </summary>
        /// <param name="predicate">条件を指定する関数</param>
        /// <returns>DTOが存在する場合はtrue、それ以外の場合はfalse</returns>
        bool IsExists(Func<TDto, bool> predicate);

        /// <summary>
        /// 複数のDTOを追加します。
        /// </summary>
        /// <param name="dtos">追加するDTOのコレクション</param>
        void AddRange(IEnumerable<TDto> dtos);

        /// <summary>
        /// 複数のDTOを更新します。
        /// </summary>
        /// <param name="dtos">更新するDTOのコレクション</param>
        void UpdateRange(IEnumerable<TDto> dtos);

        /// <summary>
        /// 複数のDTOを削除します。
        /// </summary>
        /// <param name="dtos">削除するDTOのコレクション</param>
        void DeleteRange(IEnumerable<TDto> dtos);
    }
}
