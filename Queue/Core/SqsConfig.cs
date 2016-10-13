using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using System.Configuration;

namespace UsingAmazonSQS.Queue.Core
{
    public static class SqsConfig
    {

        private static AmazonSQSConfig _config;
        private static AmazonSQSClient _sqs;

        public static AmazonSQSClient Initialize
        {
            get
            {
                if (_config == null)
                    _config = new AmazonSQSConfig();

                _config.ServiceURL = ConfigurationManager.AppSettings["SQSServiceQueeUrl"];

                if (_sqs == null)
                    _sqs = new AmazonSQSClient(ConfigurationManager.AppSettings["AwsAccessId"], ConfigurationManager.AppSettings["AwsSecretAccessKey"], Amazon.RegionEndpoint.SAEast1);

                return _sqs;

            }

        }

    }


}
