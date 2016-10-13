using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsingAmazonSQS.Queue.Components;

namespace UsingAmazonSQS.Queue
{
    public class QueueFactory
    {
        private static QCompany _SqsCompany;
        public static QCompany SqsCompany
        {
            get
            {
                if (_SqsCompany == null)
                    _SqsCompany = new QCompany();

                return _SqsCompany;
            }
        }
        private static QUser _SqsUser;
        public static QUser SqsUser
        {
            get
            {
                if (_SqsUser == null)
                    _SqsUser = new QUser();

                return _SqsUser;
            }
        }
    }
}
