UsingAmazonSQS
==============

Sample .NET project that uses AWS SDK and SQS (Simples Queue Service)
- Send a simple message to AWS SQS
- List all messages from AWS SQS

> **IMPORTANT:** Be guaranteed that the SQS service is configured correctly in your AWS Console. Remember, it's necessary to config some keys in App.config like: SQSServiceUrl, SQSServiceQueeUrl, AwsAccessId and AwsSecretAccessKey

###UsingAmazonSQS.Queue
Class Library that contains:
- Core: 
  - SqsConfig class for configuration and initialize of the service
  - SqsMessage class (body and attributes) to use in requests 
  - SqsQueue class that implement ISqsQueue
- Interfaces:
  - ISqsQueue: 3 methods to be implement: Send, Receive and List
- Components:
  - Concrete class for samples: QCompany and QUser
- QueueFactory: A factory pattern to create concrete class (Components)


###UsingAmazonSQS.Queue.Test
Two classes for tests:
- TestCompanyQueue
  - Test:Should_Send_Company_Message_To_Sqs
  - Test:Should_List_Company_Message_From_Sqs
- TestUserQueue
  - Test:Should_Send_User_Message_To_Sqs
  - Test:Should_List_User_Message_From_Sqs  
  
###UsingAmazonSQS.Model
Two examples of domain classes:
- Company
- User
  
