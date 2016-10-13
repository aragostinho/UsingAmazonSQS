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
    public class QUser : SqsQuee
    {

        public string QueueUrl
        {
            get
            {
                return string.Concat(ConfigurationManager.AppSettings["SQSServiceQueeUrl"], "User");
            }
        }

        public void Send(User pUser)
        {
            try
            {
                SqsMessage _sqsmessage = new SqsMessage();
                _sqsmessage.QueueUrl = this.QueueUrl;
                _sqsmessage.MessageBody = "User Data Information";
                Dictionary<string, MessageAttributeValue> _data = new Dictionary<string, MessageAttributeValue>();
                _data.Add("Id", new MessageAttributeValue() { StringValue = pUser.Id.ToString(), DataType = "String" });
                _data.Add("Name", new MessageAttributeValue() { StringValue = pUser.Name, DataType = "String" });
                _data.Add("Email", new MessageAttributeValue() { StringValue = pUser.Email, DataType = "String" });

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
