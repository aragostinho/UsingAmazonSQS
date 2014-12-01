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


        public override void Execute(string[] args)
        {

            string QueueUrl = ConfigurationManager.AppSettings["SQSServiceQueeUrl"];
            string QueueName = ConfigurationManager.AppSettings["QueueName"];
            string QueueFullUrl = string.Concat(QueueUrl, QueueName);

            AmazonSQSClient sqs = new SQSConfig().Initialize();

            Console.Write(string.Format("Receive message from {0}", QueueName));

            int total = GetTotalMessages(QueueFullUrl, sqs);

            for (int i = 0; i < total; i++)
            {
                List<Message> messages = null;
                messages = GetMessages(QueueFullUrl, sqs);
            }

            Console.ReadKey();

        }

        private static List<Message> GetMessages(string QueueFullUrl, AmazonSQSClient sqs)
        {
            ReceiveMessageRequest requestToReceive = new ReceiveMessageRequest(QueueFullUrl);
            ReceiveMessageResponse response = sqs.ReceiveMessage(requestToReceive);

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                return response.Messages;

            throw new ApplicationException("Problems in the endpoint communication!");
        }

        private static int GetTotalMessages(string QueueFullUrl, AmazonSQSClient sqs)
        {
            int total = 0;
            GetQueueAttributesRequest queueAttributesRequest = new GetQueueAttributesRequest(QueueFullUrl, new List<string> { "All" });
            GetQueueAttributesResult queueAttributesResult = sqs.GetQueueAttributes(queueAttributesRequest);

            if (queueAttributesResult.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                total = queueAttributesResult.ApproximateNumberOfMessages;
            }
            return total;
        }


    }
}
