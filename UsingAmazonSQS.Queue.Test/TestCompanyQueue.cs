using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UsingAmazonSQS.Queue.Components;
using UsingAmazonSQS.Model;
using System.Collections.Generic;
using Amazon.SQS.Model;

namespace UsingAmazonSQS.Queue.Test
{
    [TestClass]
    public class TestCompanyQueue
    {

        private QCompany oQCompany;
        private Company oCompany;

        [TestInitialize]
        public void Setup()
        {
            oQCompany = new QCompany();
            oCompany = new Company();
            oCompany.Id = 125;
            oCompany.Name = "Microsoft Corp.";
            oCompany.Email = "sample@ms.com";
        }


        [TestMethod]
        public void Should_Send_Company_Message_To_SQS()
        {
            bool _isSent = false;
            try
            {
                oQCompany.Send(oCompany);
                _isSent = true;
            }
            catch (Exception)
            {
                throw;
            }
            Assert.IsTrue(_isSent, "Error to send a message to SQS");

        }

        [TestMethod]
        public void Should_List_Company_Message_From_SQS()
        {
            bool _isListing = false;
            try
            {
                IList<Message> messages = oQCompany.List();
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
            oQCompany = null;
            oCompany = null;
        }

    }
}
