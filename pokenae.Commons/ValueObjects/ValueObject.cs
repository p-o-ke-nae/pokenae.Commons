using System;
using System.Collections.Generic;
using System.Linq;

namespace pokenae.Commons.ValueObjects
{
    /// <summary>
    /// �l�I�u�W�F�N�g�̊��N���X
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// �������𔻒f���邽�߂̃R���|�[�l���g���擾���܂��B
        /// </summary>
        /// <returns>�������𔻒f���邽�߂̃R���|�[�l���g�̗�</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// �l�I�u�W�F�N�g�̓������𔻒f���܂��B
        /// </summary>
        /// <param name="obj">��r�Ώۂ̃I�u�W�F�N�g</param>
        /// <returns>�������ꍇ�� true�A����ȊO�̏ꍇ�� false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// �l�I�u�W�F�N�g�̃n�b�V���R�[�h���擾���܂��B
        /// </summary>
        /// <returns>�n�b�V���R�[�h</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// �l�I�u�W�F�N�g�̕�����\�����擾���܂��B
        /// </summary>
        /// <returns>�l�I�u�W�F�N�g�̕�����\��</returns>
        public override string ToString()
        {
            return string.Join(", ", GetEqualityComponents());
        }
    }
}
