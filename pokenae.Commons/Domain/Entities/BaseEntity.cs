using System;
using System.ComponentModel.DataAnnotations;

namespace pokenae.Commons.Domain.Entities
{
    /// <summary>
    /// 基本的なエンティティの基底クラス
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// エンティティの一意の識別子
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// エンティティの作成日時
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// エンティティの更新日時
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// エンティティの等価性を判断します
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>等しい場合は true、それ以外の場合は false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var entity = (BaseEntity)obj;
            return Id == entity.Id;
        }

        /// <summary>
        /// エンティティのハッシュコードを取得します
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// エンティティの更新日時を現在の日時に更新します
        /// </summary>
        public void Update()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
