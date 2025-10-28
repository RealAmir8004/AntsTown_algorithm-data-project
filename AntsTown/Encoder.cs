using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AntsTown
{
    internal class Encoder
    {
        public static void Run(Dictionary<char, string> codes , string[][] childs)
        {
            Console.WriteLine("Encoder is running...\n");

            string[] queue = getQueueUser();

            string[] passedQ = Radix.search(queue, childs); // fix me : im corrupting childs array(call by ref!!)
            
            Console.WriteLine("The following ants has reconized and passed in this order :");
            foreach (string child in passedQ)
            {
                Console.WriteLine(child);
            }


            byte[] byteArray = ConvertQueueToBytes(passedQ, codes);


            string path  = WriteToOrder(byteArray);
            
            Console.WriteLine($"order.bin file created successfully at this location:\n{path}.\n");

            Console.WriteLine("Encoder Program is done.\n");
            return;

        }
  
        public static byte[] ConvertQueueToBytes(string[] passedQ , Dictionary<char, string> codes)
        {
            // bitList 
            List<bool> bitList = new List<bool>();

            foreach (string child in passedQ)
            {
                foreach (char c in child)
                {
                    foreach(char b in codes[c] )
                    {
                        bitList.Add(b == '1');
                    }
                }
                foreach (char b in codes['\n'])
                {
                    bitList.Add(b == '1');
                }
            }
            // Convert bitList to bytes
            byte[] byteArray = ConvertBitsToBytes(bitList);

            return byteArray;
        }

        public static string WriteToOrder(byte[] byteArray)
        {
            string path = Path.GetFullPath("order.bin");
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                try
                {
                    writer.Write(byteArray);
                }
                catch (IOException ioexp)
                {
                    Console.WriteLine("Error: {0}", ioexp.Message);
                }
            }
            return path;
        }

        public static byte[] ConvertBitsToBytes(List<bool> bits)
        {
            int byteCount = (bits.Count + 7) / 8;
            byte[] bytes = new byte[byteCount];

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[i / 8] |= (byte)(1 << (7 - (i % 8)));
            }

            return bytes;
        }

        public static string[] getQueueUser()
        {
            //standard input
            Console.Write("enter the length of queue (number of ants wich wants to enter town) : ");
            int len_queue = int.Parse(Console.ReadLine());
            string[] queue = new string[len_queue];
            Console.WriteLine($"u can now enter {len_queue} line of ants");
            for (int i = 0; i < len_queue; i++)
            {
                queue[i] = Console.ReadLine();
            }

            Console.WriteLine("------input colected------");
            return queue;
        }
    }
}
