using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using UsingAmazonSQS.Queue.Interfaces;

namespace UsingAmazonSQS.Queue.Core
{
    public class SqsQuee : ISqsQueue
    { 

        public void Send(SqsMessage pQueue)
        {
            try
            {
                AmazonSQSClient sqs = SqsConfig.Initialize;
                SendMessageRequest requestToSend = new SendMessageRequest
                {
                    MessageBody = pQueue.MessageBody,
                    MessageAttributes = pQueue.MessageAttributes,
                    QueueUrl = pQueue.QueueUrl
                };
                sqs.SendMessage(requestToSend);
            }
            catch (Exception oException)
            {
                throw oException;
            }


        }
        public Message Receive(SqsMessage pQueue, bool pPeek = true)
        {
            try
            {
                AmazonSQSClient sqs = SqsConfig.Initialize;
                Message message = null;
                ReceiveMessageRequest requestToReceive = new ReceiveMessageRequest(pQueue.QueueUrl);
                requestToReceive.MessageAttributeNames = new List<string> { "All" };
                ReceiveMessageResponse response = sqs.ReceiveMessage(requestToReceive);

                if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                    throw new ApplicationException("Problems in the endpoint communication!");

                if (response.Messages.Count > 0)
                    message = response.Messages.First();

                if (pPeek == true)
                    sqs.DeleteMessage(new DeleteMessageRequest() { QueueUrl = pQueue.QueueUrl, ReceiptHandle = message.ReceiptHandle });


                return message;
            }
            catch (Exception oException)
            {
                throw oException;
            }


        } 
        public IList<Message> List(SqsMessage pQueue, bool pPeek = true)
        {
            try
            {
                IList<Message> message = new List<Message>();
                int messagetotal = GetTotalMessages(pQueue);
                if (messagetotal == 0)
                    return null;
                 
                for (int i = 0; i < messagetotal; i++)
                    message.Add(Receive(pQueue, pPeek));


                return message;
            }
            catch (Exception oException)
            {
                throw oException;
            }


        }  
        private static int GetTotalMessages(SqsMessage pQueue)
        {

            AmazonSQSClient sqs = SqsConfig.Initialize;
            GetQueueAttributesRequest queueAttributesRequest = new GetQueueAttributesRequest(pQueue.QueueUrl,new List<string> { "All" });
            GetQueueAttributesResult queueAttributesResult = sqs.GetQueueAttributes(queueAttributesRequest);

            if (queueAttributesResult.HttpStatusCode != System.Net.HttpStatusCode.OK)
                throw new ApplicationException("Problems in the endpoint communication!");

            return queueAttributesResult.ApproximateNumberOfMessages;
        }

    }
}
