using pokenae.Commons.DTOs;
using System.Collections.Generic;

namespace pokenae.Commons.Services.Application
{
    /// <summary>
    /// �A�v���P�[�V�����w�̃T�[�r�X�C���^�[�t�F�[�X�B
    /// DTO���g�p���ăf�[�^�̎�����s���܂��B
    /// </summary>
    /// <typeparam name="TDto">DTO�̌^</typeparam>
    public interface IApplicationService<TDto> where TDto : BaseDto
    {
        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����DTO���擾���܂��B
        /// </summary>
        /// <param name="predicate">�������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����DTO</returns>
        TDto Find(Func<TDto, bool> predicate);

        /// <summary>
        /// ���ׂĂ�DTO���擾���܂��B
        /// </summary>
        /// <returns>DTO�̃R���N�V����</returns>
        IEnumerable<TDto> GetAll();

        /// <summary>
        /// �V����DTO��ǉ����܂��B
        /// </summary>
        /// <param name="dto">�ǉ�����DTO</param>
        void Add(TDto dto);

        /// <summary>
        /// DTO���X�V���܂��B
        /// </summary>
        /// <param name="dto">�X�V����DTO</param>
        void Update(TDto dto);

        /// <summary>
        /// DTO���폜���܂��B
        /// </summary>
        /// <param name="dto">�폜����DTO</param>
        void Delete(TDto dto);

        /// <summary>
        /// DTO��ǉ��܂��͍X�V���܂��B
        /// </summary>
        /// <param name="dto">�ǉ��܂��͍X�V����DTO</param>
        /// <param name="predicate">�������w�肷��֐�</param>
        void Upsert(TDto dto, Func<TDto, bool> predicate);

        /// <summary>
        /// �폜���ꂽ���̂��܂ނ��ׂĂ�DTO���擾���܂��B
        /// </summary>
        /// <returns>DTO�̃R���N�V����</returns>
        IEnumerable<TDto> GetAllIncludingDeleted();

        /// <summary>
        /// �폜���ꂽ���̂��܂ށA�w�肳�ꂽ�����Ɉ�v����DTO���擾���܂��B
        /// </summary>
        /// <param name="predicate">�������w�肷��֐�</param>
        /// <returns>�����Ɉ�v����DTO</returns>
        TDto FindIncludingDeleted(Func<TDto, bool> predicate);

        /// <summary>
        /// �w�肳�ꂽ�����Ɉ�v����DTO�����݂��邩�ǂ������m�F���܂��B
        /// </summary>
        /// <param name="predicate">�������w�肷��֐�</param>
        /// <returns>DTO�����݂���ꍇ��true�A����ȊO�̏ꍇ��false</returns>
        bool IsExists(Func<TDto, bool> predicate);

        /// <summary>
        /// ������DTO��ǉ����܂��B
        /// </summary>
        /// <param name="dtos">�ǉ�����DTO�̃R���N�V����</param>
        void AddRange(IEnumerable<TDto> dtos);

        /// <summary>
        /// ������DTO���X�V���܂��B
        /// </summary>
        /// <param name="dtos">�X�V����DTO�̃R���N�V����</param>
        void UpdateRange(IEnumerable<TDto> dtos);

        /// <summary>
        /// ������DTO���폜���܂��B
        /// </summary>
        /// <param name="dtos">�폜����DTO�̃R���N�V����</param>
        void DeleteRange(IEnumerable<TDto> dtos);
    }
}
