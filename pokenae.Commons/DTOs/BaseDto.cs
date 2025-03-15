using System;

namespace pokenae.Commons.DTOs
{
    /// <summary>
    /// �f�[�^�]���I�u�W�F�N�g�̃x�[�X�N���X�B
    /// </summary>
    public abstract class BaseDto
    {
        /// <summary>
        /// �G���e�B�e�B�̈�ӎ��ʎq�B
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// �G���e�B�e�B�̍쐬�����B
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// �G���e�B�e�B�̍ŏI�X�V�����B
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// �R���X�g���N�^�B
        /// �V�����C���X�^���X�����������܂��B
        /// </summary>
        protected BaseDto()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// ���ʂ̃o���f�[�V�������W�b�N�����s���܂��B
        /// </summary>
        /// <exception cref="ArgumentException">Id����̏ꍇ�ɃX���[����܂��B</exception>
        public virtual void Validate()
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
        }
    }
}
