using System;

namespace pokenae.Commons.Application.DTOs
{
    /// <summary>
    /// �A�v���P�[�V�����w����v���[���e�[�V�����w�Ƀf�[�^��]������DTO�̊��N���X
    /// </summary>
    public abstract class ApplicationDto
    {
        /// <summary>
        /// �G���e�B�e�B�̈�ӎ��ʎq
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// �G���e�B�e�B�̍쐬����
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// �G���e�B�e�B�̍ŏI�X�V����
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// �V�����C���X�^���X�����������܂�
        /// </summary>
        protected ApplicationDto()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// ���ʂ̃o���f�[�V�������W�b�N�����s���܂�
        /// </summary>
        /// <exception cref="ArgumentException">Id����̏ꍇ�ɃX���[����܂�</exception>
        public virtual void Validate()
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }
        }
    }
}
