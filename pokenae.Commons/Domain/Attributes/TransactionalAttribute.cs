using System;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace pokenae.Commons.Domain.Attributes
{
    /// <summary>
    /// ���\�b�h�Ƀg�����U�N�V�����X�R�[�v��K�p���邽�߂̑����ł�
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class TransactionalAttribute : Attribute
    {
        /// <summary>
        /// ���\�b�h�̎��s�O�ɌĂяo����A�g�����U�N�V�����X�R�[�v���J�n���܂�
        /// </summary>
        public void OnMethodExecuting()
        {
            TransactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.DefaultTimeout
            });
        }

        /// <summary>
        /// ���\�b�h�̎��s��ɌĂяo����A�g�����U�N�V�������������܂�
        /// </summary>
        public void OnMethodExecuted()
        {
            TransactionScope?.Complete();
            TransactionScope?.Dispose();
        }

        /// <summary>
        /// ���\�b�h�̎��s���ɗ�O�����������ꍇ�ɌĂяo����A�g�����U�N�V������j�����܂�
        /// </summary>
        public void OnMethodException()
        {
            TransactionScope?.Dispose();
        }

        /// <summary>
        /// �g�����U�N�V�����X�R�[�v��ێ�����v���p�e�B�ł�
        /// </summary>
        private TransactionScope TransactionScope { get; set; }
    }
}
