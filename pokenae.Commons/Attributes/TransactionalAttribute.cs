using System;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace pokenae.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class TransactionalAttribute : Attribute
    {
        public void OnMethodExecuting()
        {
            TransactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.DefaultTimeout
            });
        }

        public void OnMethodExecuted()
        {
            TransactionScope?.Complete();
            TransactionScope?.Dispose();
        }

        public void OnMethodException()
        {
            TransactionScope?.Dispose();
        }

        private TransactionScope TransactionScope { get; set; }
    }
}
