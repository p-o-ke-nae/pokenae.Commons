using System;
using System.ComponentModel.DataAnnotations;

namespace pokenae.Commons.Domain.DTOs
{
    /// <summary>
    /// �C���t���X�g���N�`���w����h���C���w�Ƀf�[�^��]������DTO�̊��N���X
    /// </summary>
    public abstract class InfrastructureDto
    {
        /// <summary>
        /// �쐬��
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// �쐬����
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// �쐬�v���O����ID
        /// </summary>
        [Required]
        public string CreatedProgramId { get; set; }

        /// <summary>
        /// �X�V��
        /// </summary>
        [Required]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// �X�V����
        /// </summary>
        [Required]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// �X�V�v���O����ID
        /// </summary>
        [Required]
        public string UpdatedProgramId { get; set; }

        /// <summary>
        /// �폜��
        /// </summary>
        public string? DeletedBy { get; set; }

        /// <summary>
        /// �폜����
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// �폜�v���O����ID
        /// </summary>
        public string? DeletedProgramId { get; set; }

        /// <summary>
        /// �o�[�W����
        /// </summary>
        [ConcurrencyCheck]
        public int Version { get; set; }

        /// <summary>
        /// �f�t�H���g�R���X�g���N�^
        /// </summary>
        protected InfrastructureDto()
        {
            CreatedBy = string.Empty;
            CreatedProgramId = string.Empty;
            UpdatedBy = string.Empty;
            UpdatedProgramId = string.Empty;
            DeletedBy = string.Empty;
            DeletedProgramId = string.Empty;
        }
    }
}
