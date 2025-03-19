using System;
using System.Collections.Generic;
using System.Linq;

namespace pokenae.Commons.ValueObjects
{
    /// <summary>
    /// 値オブジェクトの基底クラス
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// 等価性を判断するためのコンポーネントを取得します。
        /// </summary>
        /// <returns>等価性を判断するためのコンポーネントの列挙</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// 値オブジェクトの等価性を判断します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト</param>
        /// <returns>等しい場合は true、それ以外の場合は false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// 値オブジェクトのハッシュコードを取得します。
        /// </summary>
        /// <returns>ハッシュコード</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// 値オブジェクトの文字列表現を取得します。
        /// </summary>
        /// <returns>値オブジェクトの文字列表現</returns>
        public override string ToString()
        {
            return string.Join(", ", GetEqualityComponents());
        }
    }
}
