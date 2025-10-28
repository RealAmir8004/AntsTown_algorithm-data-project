using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace AntsTown
{
    internal class Program
    {
        static void Main()
        {
            while (true)
            {
                string[] linesParent = OpenParents();
                string[] males = null;
                string[] queens = null;
                getParentsFromLines(linesParent, out queens , out males);

                //string[] queens = { "abc", "bcde" };//test
                //string[] males = { "abbb", "abcd" };//test

                string[][] childs = makeChilds(queens, males);

                Dictionary<char, string> codes = getCodes(childs);

                //int countChilds = childs.Count(child => child != null);
                //int totalCharacters = childs.Sum(childGroup => childGroup.Sum(child => child.Length));


                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine("Encode = 1\nDecode = 2\nExit = 3");
                int choice = int.Parse(Console.ReadLine());
                Console.WriteLine("-------------------------------------------------------------");

                if (choice == 1)
                    Encoder.Run(codes , childs);
                else if (choice == 2)
                    Decoder.Run(codes );
                else if (choice == 3)
                    break;
                else
                    Console.WriteLine("Invalid choice");
            }
        }

        public static string[] OpenParents()
        {
            string[] linesParent = null;
            string path = null;
            try
            {
                path = FindParents();
                linesParent = File.ReadAllLines(path);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"File not found \n");
                Environment.Exit(1);
                //return;
            }
            catch (IOException)
            {
                Console.WriteLine($"File is not accessible in {path} \n");
                Environment.Exit(1);
                //return;
            }
            Console.WriteLine($"parent file succsesfuly opened  \n");

            return linesParent;
        }

        //public static string FindParents()
        //{

        //    string path = Path.GetFullPath(@"..\..\..\parents.txt");
        //    if (File.Exists(path))
        //        return path;

        //    path = Path.GetFullPath(@"..\..\..\..\parents.txt");
        //    if (File.Exists(path))
        //        return path;

        //    path = Path.GetFullPath(@"..\..\parents.txt");
        //    if (File.Exists(path))
        //        return path;

        //    path = Path.GetFullPath(@"..\parents.txt");
        //    if (File.Exists(path))
        //        return path;

        //    path = Path.GetFullPath(@"parents.txt");
        //    if (File.Exists(path))
        //        return path;

        //    return null;
        //}

        public static string FindParents()
        {
            string[] searchPaths = new string[]
            {
                Path.GetFullPath(@"..\..\..\..\parents.txt"),
                Path.GetFullPath(@"..\..\..\parents.txt"),
                Path.GetFullPath(@"..\..\parents.txt"),
                Path.GetFullPath(@"..\parents.txt"),
                Path.GetFullPath(@"parents.txt")
            };

            foreach (string path in searchPaths)
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("parent File found at: " + path);
                    return path;
                }
                else
                {
                    Console.WriteLine("parent File not found at: " + path);
                }
            }

            Console.WriteLine("Error: parent.txt file not found at any of the searched paths.");
            return null;
        }


        public static void getParentsFromLines(string[] linesParent, out string[] queens, out string[] males)
        {

            string[] numbers = linesParent[0].Split(' ');

            int queensNo = int.Parse(numbers[0]);
            int malesNo = int.Parse(numbers[1]);


            queens = new string[queensNo];
            males = new string[malesNo];

            Array.Copy(linesParent, 1, queens, 0, queensNo);
            Array.Copy(linesParent, queensNo + 1, males, 0, malesNo);
        }

        public static string[][] makeChilds(string[] queens, string[] males)
        {
            // initilaze childs array:
            string[][] childs = new string[10][];
            for (int i = 0; i < 10; i++) // fix me : 10
            {
                childs[i] = new string[0];
            }

            // making and sorting all childs:
            foreach (string queen in queens)
            {
                foreach (string male in males)
                {
                    string[][] result = Radix.sort(Kmp.KMPSearch(queen, male));  // patt = queen , text = male
                    for (int i = 0; i < 10; i++)
                    {
                        if (result[i] != null && result[i].Length > 0)
                        {
                            childs[i] = childs[i].Concat(result[i]).ToArray();
                        }
                    }
                }
            }
            return childs;
        }

        public static Dictionary<char, string> getCodes(string[][] childs)
        {

            Dictionary<char, uint> charReapet = countChars(childs);



            Dictionary<char, string> codes = Huffman.makeTree(charReapet); // attention : size +1 : for '\n' char


            return codes;
        }

        public static Dictionary<char, uint> countChars(string[][] childs)
        {
            //getting charReapeated :
            var charReapet = new Dictionary<char, uint>();
            foreach (string[] childGroup in childs)
            {
                foreach (string child in childGroup) // fix me : in esma ro doros kon ... 
                {
                    foreach (char ch in child)
                    {
                        if (charReapet.ContainsKey(ch))
                            charReapet[ch]++;
                        else
                            charReapet.Add(ch, 1);
                    }
                    if (charReapet.ContainsKey('\n'))
                        charReapet['\n']++;
                    else
                        charReapet.Add('\n', 1);
                }
            }
            return charReapet;
        }

    }

}