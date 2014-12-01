using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SQS;
using System.Configuration;

namespace UsingAmazonSQS
{
    public class SQSConfig
    {
        protected AmazonSQSConfig config;
        protected AmazonSQSClient sqs;

        public AmazonSQSClient Initialize(string accessKey, string privateKey)
        {
            config = new AmazonSQSConfig();
            config.ServiceURL = ConfigurationManager.AppSettings["SQSServiceUrl"];
            sqs = new AmazonSQSClient(accessKey, privateKey, Amazon.RegionEndpoint.SAEast1);
            return sqs;
        }

    }
}
