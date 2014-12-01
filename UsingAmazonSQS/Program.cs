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

            if (args.Count() < 2)
            {
                Console.Write("Chave de acesso: ");
                accessKey = Console.ReadLine();
                Console.WriteLine("");
                Console.Write("Chave privada: ");
                privateKey = Console.ReadLine();
                Console.WriteLine("");
            }

            int _intOption = 1;
            _ActionList = new Dictionary<int, AbstractInterpreter>();
            _ActionList.Add(1, new SendMessage());
            _ActionList.Add(2, new ReceiveMessage());

            try
            {
                string option = ShowOptions(_ActionList, args);
                while (int.TryParse(option, out _intOption) == false && !_ActionList.ContainsKey(_intOption))
                {
                    Console.Clear();
                    Console.WriteLine("Inform the code number:");
                    option = ShowOptions(_ActionList, args);
                }

                _ActionList[_intOption].Execute(accessKey, privateKey);

                if (args.Length != 0)
                    return;


            }
            catch (Exception oException)
            {
                Console.WriteLine("Err in {0} às {1}: {2}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), oException.Message);
                Console.WriteLine("StackTrace: {0}", oException.StackTrace);
                Console.ReadKey();
            }

        }


        private static string ShowOptions(Dictionary<int, AbstractInterpreter> _ActionList, string[] args)
        {
            Console.WriteLine("Select one of these options below:");
            foreach (var item in _ActionList)
                Console.WriteLine("{0}) {1}", item.Key, item.Value.Description());

            var hasArguments = args.Length != 0;

            return hasArguments ? args[0] : Console.ReadLine();
        }
    }
}
