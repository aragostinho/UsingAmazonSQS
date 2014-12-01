using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingAmazonSQS
{
    public abstract class AbstractInterpreter
    {
        public abstract void Execute(string accessKey, string privateKey);
        public abstract string Description();
    }
}
