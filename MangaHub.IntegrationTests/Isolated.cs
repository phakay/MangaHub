using NUnit.Framework;
using System;
using System.Transactions;

namespace MangaHub.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public ActionTargets Targets
        {
            get { return ActionTargets.Test ; }
        }

        public void AfterTest(TestDetails testDetails)
        {
            _transactionScope.Dispose();   
        }

        public void BeforeTest(TestDetails testDetails)
        {
            _transactionScope = new TransactionScope();
        }
    }
}
