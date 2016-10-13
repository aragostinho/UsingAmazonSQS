using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace UsingAmazonSQS.Queue.Core
{
    public class SqsMessage  
    {  
        public string MessageBody { get; set; }
        public Dictionary<string, MessageAttributeValue> MessageAttributes { get; set; }
        public string QueueUrl { get; set; }  
    }
}
