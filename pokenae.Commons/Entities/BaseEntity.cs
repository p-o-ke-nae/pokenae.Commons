using System;
using System.ComponentModel.DataAnnotations;

namespace pokenae.Commons.Entities
{
    /// <summary>
    /// 共通フィールドを持つ基底クラス
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        protected BaseEntity()
        {
            CreatedBy = string.Empty;
            CreatedProgramId = string.Empty;
            UpdatedBy = string.Empty;
            UpdatedProgramId = string.Empty;
            DeletedBy = string.Empty;
            DeletedProgramId = string.Empty;
        }

        /// <summary>
        /// 作成者
        /// </summary>
        [Required]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 作成プログラムID
        /// </summary>
        [Required]
        public string CreatedProgramId { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [Required]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        [Required]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 更新プログラムID
        /// </summary>
        [Required]
        public string UpdatedProgramId { get; set; }

        /// <summary>
        /// 削除者
        /// </summary>
        public string? DeletedBy { get; set; }

        /// <summary>
        /// 削除日時
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        /// 削除プログラムID
        /// </summary>
        public string? DeletedProgramId { get; set; }

        /// <summary>
        /// バージョン
        /// </summary>
        [ConcurrencyCheck]
        public int Version { get; set; }
    }
}
