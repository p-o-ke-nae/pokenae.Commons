using System;

namespace pokenae.Commons.DTOs
{
    /// <summary>
    /// データ転送オブジェクトのベースクラス。
    /// </summary>
    public abstract class BaseDto
    {
        /// <summary>
        /// エンティティの一意識別子。
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// エンティティの作成日時。
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// エンティティの最終更新日時。
        /// </summary>
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// コンストラクタ。
        /// 新しいインスタンスを初期化します。
        /// </summary>
        protected BaseDto()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// 共通のバリデーションロジックを実行します。
        /// </summary>
        /// <exception cref="ArgumentException">Idが空の場合にスローされます。</exception>
        public virtual void Validate()
        {
            if (Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
        }
    }
}
