using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SQS;
using System.Configuration;
using Amazon.SQS.Model;

namespace UsingAmazonSQS
{
    public class ReceiveMessage : AbstractInterpreter
    {

        public override string Description()
        {
            return "Receive a message";
        }


        public override void Execute(string accessKey, string privateKey)
        {
            string QueueUrl = ConfigurationManager.AppSettings["SQSServiceQueeUrl"];
            string QueueName = ConfigurationManager.AppSettings["QueueName"];
            string QueueFullUrl = string.Concat(QueueUrl, QueueName);

            AmazonSQSClient sqs = new SQSConfig().Initialize(accessKey, privateKey);

            Console.WriteLine(string.Format("Getting messages from '{0}' queue...", QueueName));

            int total = GetTotalMessages(QueueFullUrl, sqs);

            Console.WriteLine("There is {0} messages in the queue.\n", total);

            for (int i = 0; i < total; i++)
            {
                Message message = null;
                message = GetMessage(QueueFullUrl, sqs);

                Console.WriteLine();
                if (message == null) break;
                Console.WriteLine();
                Console.WriteLine("Message Content");
                Console.WriteLine(message.Body);
                Console.WriteLine();

                //delete message after peek it
                sqs.DeleteMessage(new DeleteMessageRequest() { QueueUrl = QueueFullUrl, ReceiptHandle = message.ReceiptHandle });


                Console.WriteLine();
                Console.WriteLine("Message have been received!");
            }

            Console.WriteLine("All messages have been received");
        }

        private static Message GetMessage(string QueueFullUrl, AmazonSQSClient sqs)
        {
            ReceiveMessageRequest requestToReceive = new ReceiveMessageRequest(QueueFullUrl);
            ReceiveMessageResponse response = sqs.ReceiveMessage(requestToReceive);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new ApplicationException("Problems in the endpoint communication!");

            if (response.Messages.Count > 0)
                return response.Messages.First();
            return null;
        }

        private static int GetTotalMessages(string QueueFullUrl, AmazonSQSClient sqs)
        {
            GetQueueAttributesRequest queueAttributesRequest = new GetQueueAttributesRequest(QueueFullUrl, new List<string> { "All" });
            GetQueueAttributesResult queueAttributesResult = sqs.GetQueueAttributes(queueAttributesRequest);

            if (queueAttributesResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new ApplicationException("Problems in the endpoint communication!");

            return queueAttributesResult.ApproximateNumberOfMessages;
        }

    }
}
