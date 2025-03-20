using System;
using System.ComponentModel.DataAnnotations;

namespace pokenae.Commons.Domain.Entities
{
    /// <summary>
    /// ��{�I�ȃG���e�B�e�B�̊��N���X
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// �G���e�B�e�B�̈�ӂ̎��ʎq
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// �G���e�B�e�B�̍쐬����
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// �G���e�B�e�B�̍X�V����
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// �f�t�H���g�R���X�g���N�^
        /// </summary>
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// �G���e�B�e�B�̓������𔻒f���܂�
        /// </summary>
        /// <param name="obj">��r�Ώۂ̃I�u�W�F�N�g</param>
        /// <returns>�������ꍇ�� true�A����ȊO�̏ꍇ�� false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var entity = (BaseEntity)obj;
            return Id == entity.Id;
        }

        /// <summary>
        /// �G���e�B�e�B�̃n�b�V���R�[�h���擾���܂�
        /// </summary>
        /// <returns>�n�b�V���R�[�h</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// �G���e�B�e�B�̍X�V���������݂̓����ɍX�V���܂�
        /// </summary>
        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
