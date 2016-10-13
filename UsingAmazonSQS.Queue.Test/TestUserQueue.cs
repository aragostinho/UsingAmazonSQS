using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsingAmazonSQS.Queue.Components;
using UsingAmazonSQS.Model;
using System.Collections.Generic;
using Amazon.SQS.Model;

namespace UsingAmazonSQS.Queue.Test
{
    [TestClass]
    public class TestUserQueue
    {

        private QUser oQUser;
        private User oUser;

        [TestInitialize]
        public void Setup()
        {
            oQUser = new QUser();
            oUser = new User();
            oUser.Id = 80;
            oUser.Name = "Linus torvalds";
            oUser.Email = "sample@ms.com";
        }


        [TestMethod]
        public void Should_Send_User_Message_To_SQS()
        {
            bool _isSent = false;
            try
            {
                oQUser.Send(oUser);
                _isSent = true;
            }
            catch (Exception)
            {
                throw;
            }
            Assert.IsTrue(_isSent, "Error to send a message to SQS");

        }

        [TestMethod]
        public void Should_List_User_Message_From_SQS()
        {
            bool _isListing = false;
            try
            {
                IList<Message> messages = oQUser.List();
                if (messages != null && messages.Count > 0)
                    _isListing = true;
            }
            catch (Exception)
            {

                throw;
            }

            Assert.IsTrue(_isListing, "Error to list messages from SQS");

        }


        [TestCleanup]
        public void Cleanup()
        {
            oQUser = null;
            oUser = null;
        }

    }
}
