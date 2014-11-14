using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SQS;
using System.Configuration;
using Amazon.SQS.Model;

namespace UsingAmazonSQS
{
    public class SendMessage : AbstractInterpreter
    {

        public override string Description()
        {
            return "Send a sample message";
        }



        public override void Execute(string[] args)
        {

            string QueueUrl = ConfigurationManager.AppSettings["SQSServiceQueeUrl"];
            string QueueName = ConfigurationManager.AppSettings["QueueName"];
            string QueueFullUrl = string.Concat(QueueUrl, QueueName);

            AmazonSQSClient sqs = new SQSConfig().Initialize();
            Console.Write(string.Format("Sending a message to {0}", QueueName));

            SendMessageRequest requestToSend = new SendMessageRequest
            {
                MessageBody = string.Format("Hellow World!  Sent in: {0}", DateTime.Now.ToString()),
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                  {
                    {
                      "Name", new MessageAttributeValue 
                        { DataType = "String", StringValue = "John Doe" }
                    },
                    {
                      "DateCreated", new MessageAttributeValue 
                        { DataType = "String", StringValue = DateTime.Now.ToString() }
                    }
                  },
                QueueUrl = QueueFullUrl
            };
            sqs.SendMessage(requestToSend);
            Console.WriteLine();
            Console.WriteLine("Message has been sent!");
            Console.ReadKey();

        }

    }
}
