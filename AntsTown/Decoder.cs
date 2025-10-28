using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntsTown
{
    internal class Decoder
    {
        public static void Run(Dictionary<char, string> codes )
        {
            Console.WriteLine("Decoder is running.\n");


            byte[] bytes = ReadOrder();
            if (bytes.Length == 0)
            {
                Console.WriteLine("Error: No bytes read from file.");
                return;
            }

            string bits = BytesToBits(bytes);

            string decodedString = DecodeBits(bits, codes);
            Console.WriteLine("Decoded String:");
            //for (int i = 0; i < countChilds; i++)
            //{
            //    Console.Write(decodedString[i]);
            //}
            Console.WriteLine(decodedString);

            Console.WriteLine("\nDecoder Program is done.\n");
        }

        public static byte[] ReadOrder()
        {
            string path = FindOrder();
            if (path == null)
                return new byte[0];

            List<byte> bytes = new List<byte>();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (BinaryReader reader = new BinaryReader(fs))
                {
                    for (int i = 0; i < fs.Length; i++)
                    {
                        bytes.Add(reader.ReadByte());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }

            return bytes.ToArray();
        }

        public static string FindOrder()
        {
            string[] searchPaths = new string[]
            {
                Path.GetFullPath(@"order.bin"),
                Path.GetFullPath(@"..\..\..\order.bin"),
                Path.GetFullPath(@"..\..\order.bin"),
                Path.GetFullPath(@"..\..\..\..\order.bin"),
                Path.GetFullPath(@"..\order.bin")
            };

            foreach (string path in searchPaths)
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("order File found at: " + path);
                    return path;
                }
                else
                {
                    Console.WriteLine("order File not found at: " + path);
                }
            }

            Console.WriteLine("Error: order.bin file not found at any of the searched paths.");
            return null;
        }

        public static string BytesToBits(byte[] bytes)
        {
            StringBuilder bits = new StringBuilder();
            foreach (byte b in bytes)
            {
                bits.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return bits.ToString();
        }

        public static string DecodeBits(string bits, Dictionary<char, string> codes)
        {
            Dictionary<string, char> reverseCodes = new Dictionary<string, char>();
            foreach (var kvp in codes)
            {
                reverseCodes[kvp.Value] = kvp.Key;
            }

            StringBuilder decodedString = new StringBuilder();
            StringBuilder currentBits = new StringBuilder();

            foreach (char bit in bits)
            {
                currentBits.Append(bit);
                if (reverseCodes.TryGetValue(currentBits.ToString(), out char decodedChar))
                {
                    decodedString.Append(decodedChar);
                    currentBits.Clear();
                }
            }

            return decodedString.ToString();
        }
    }
}
