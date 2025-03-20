using System;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace pokenae.Commons.Domain.Attributes
{
    /// <summary>
    /// メソッドにトランザクションスコープを適用するための属性です
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class TransactionalAttribute : Attribute
    {
        /// <summary>
        /// メソッドの実行前に呼び出され、トランザクションスコープを開始します
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
        /// メソッドの実行後に呼び出され、トランザクションを完了します
        /// </summary>
        public void OnMethodExecuted()
        {
            TransactionScope?.Complete();
            TransactionScope?.Dispose();
        }

        /// <summary>
        /// メソッドの実行中に例外が発生した場合に呼び出され、トランザクションを破棄します
        /// </summary>
        public void OnMethodException()
        {
            TransactionScope?.Dispose();
        }

        /// <summary>
        /// トランザクションスコープを保持するプロパティです
        /// </summary>
        private TransactionScope TransactionScope { get; set; }
    }
}
