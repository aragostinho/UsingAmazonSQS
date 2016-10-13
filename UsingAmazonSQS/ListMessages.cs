using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SQS;
using System.Configuration;
using Amazon.SQS.Model;

namespace UsingAmazonSQS
{
    public class ListMessages : AbstractInterpreter
    {

        public override string Description()
        {
            return "List messages";
        }


        public override void Execute(string accessKey, string privateKey)
        {
            
            Console.WriteLine("All messages have been received");
        }

        

    }
}
