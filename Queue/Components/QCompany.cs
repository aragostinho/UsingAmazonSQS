using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS.Model;
using UsingAmazonSQS.Queue.Core;
using System.Configuration;
using UsingAmazonSQS.Model;

namespace UsingAmazonSQS.Queue.Components
{
    public class QCompany : SqsQuee
    {

        public string QueueUrl
        {
            get
            {
                return string.Concat(ConfigurationManager.AppSettings["SQSServiceQueeUrl"], "Company");
            }
        }

        public void Send(Company pCompany)
        {
            try
            {
                SqsMessage _sqsmessage = new SqsMessage();
                _sqsmessage.QueueUrl = this.QueueUrl;
                _sqsmessage.MessageBody = "Company data information";
                Dictionary<string, MessageAttributeValue> _data = new Dictionary<string, MessageAttributeValue>();
                _data.Add("Id", new MessageAttributeValue() { StringValue = pCompany.Id.ToString(), DataType = "String" });
                _data.Add("Name", new MessageAttributeValue() { StringValue = pCompany.Name, DataType = "String" });
                _data.Add("Email", new MessageAttributeValue() { StringValue = pCompany.Email, DataType = "String" });

                _sqsmessage.MessageAttributes = _data;

                base.Send(_sqsmessage);
            }
            catch (Exception oException)
            {
                throw oException;
            }

        }

        public IList<Message> List()
        {
            try
            {
                SqsMessage _sqsmessage = new SqsMessage();
                _sqsmessage.QueueUrl = this.QueueUrl;
                IList<Message> _olMessage = base.List(_sqsmessage, true);

                return _olMessage;
            }
            catch (Exception oException)
            {
                throw oException;
            }
        }
    }
}
