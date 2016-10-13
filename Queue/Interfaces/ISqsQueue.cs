using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsingAmazonSQS.Queue.Core;

namespace UsingAmazonSQS.Queue.Interfaces
{
    public interface ISqsQueue
    {
        void Send(SqsMessage pQueue);
        Message Receive(SqsMessage pQueue, bool pPeek);
        IList<Message> List(SqsMessage pQueue, bool pPeek);

    }
}
