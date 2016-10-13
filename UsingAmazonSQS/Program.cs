using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsingAmazonSQS
{
    public class Program
    {
        public static Dictionary<int, AbstractInterpreter> _ActionList;

        static void Main(string[] args)
        {
            string accessKey = null;
            string privateKey = null;
            string argumentOption = null;

            if (args.Count() < 3)
            {
                accessKey = ReadAccessKey(accessKey);
                privateKey = ReadPrivateKey(privateKey);
            }
            else
            {
                accessKey = args[0];
                privateKey = args[1];
                argumentOption = args[2];
            }

            int _intOption = 1;
            _ActionList = new Dictionary<int, AbstractInterpreter>();
            _ActionList.Add(1, new SendMessage());
            _ActionList.Add(2, new ListMessages());

            try
            {
                string option = ShowOptions(_ActionList, argumentOption);
                while (int.TryParse(option, out _intOption) == false && !_ActionList.ContainsKey(_intOption))
                {
                    Console.Clear();
                    Console.WriteLine("Inform the code number:");
                    option = ShowOptions(_ActionList, argumentOption);
                }

                _ActionList[_intOption].Execute(accessKey, privateKey);

                if (args.Length < 3)
                    Console.ReadKey();
                
                return;
            }
            catch (Exception oException)
            {
                Console.WriteLine("Err in {0} às {1}: {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), oException.Message);
                Console.WriteLine("StackTrace: {0}", oException.StackTrace);
                Console.ReadKey();
            }

        }

        private static string ReadPrivateKey(string privateKey)
        {
            Console.Write("Private Key: ");
            privateKey = Console.ReadLine();
            Console.WriteLine("");
            return privateKey;
        }

        private static string ReadAccessKey(string accessKey)
        {
            Console.Write("Access Key: ");
            accessKey = Console.ReadLine();
            Console.WriteLine("");
            return accessKey;
        }


        private static string ShowOptions(Dictionary<int, AbstractInterpreter> _ActionList, string option = null)
        {
            Console.WriteLine("Select one of these options below:");
            foreach (var item in _ActionList)
                Console.WriteLine("{0}) {1}", item.Key, item.Value.Description());

            return string.IsNullOrEmpty(option) ? Console.ReadLine() : option;
        }
    }
}
