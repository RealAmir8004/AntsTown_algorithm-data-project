using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntsTown
{
    public static class Radix
    {
        public static string[][] sort(string[] arr)
        {
            int arr_len = arr.Length;

            int[] count = new int[10];  
            for (int j = 0; j < arr_len; j++)
            {
                count[arr[j].Length]++;
            }

            // declare result variable:
            string[][] res = new string[10][]; // fix me : max len of dna =10? // res[1] => len =1
            for (int i = 0; i < 10; i++)
            {
                res[i] = new string[count[i]];
            }
            // radix :
            for (int j = 0, x = count[j]; j < arr_len; j++, x--)
            {
                int z = arr[j].Length;
                res[z][--count[z]] = arr[j];
            }
            return res;
        }
        public static string[] search(string[] queue, string[][] childs)
        {
            int len_queue = queue.Length;
            List<string> result = new List<string>();
            for (int i = 0; i < len_queue; i++)
            {
                int x = queue[i].Length;

                for (int j = 0; j < childs[x].Length; j++)
                {
                    if (queue[i] == childs[x][j])
                    {
                        result.Add(childs[x][j]);
                        childs[x][j] = null;
                        break;
                    }
                }
            }
            return result.ToArray();
        }
    }

    public static class Kmp
    {
        public static string[] KMPSearch(string pattern, string text)
        {
            int pat_len = pattern.Length;
            int text_len = text.Length;
            int[] lps = new int[pat_len];

            List<string> childs = new List<string>();

            ComputeLPSArray(pattern, pat_len, lps);

            int i = 0, j = 0;
            while (i < text_len)
            {
                if (text[i] == pattern[j])
                {
                    string child = "";
                    for (int k = j; k >= 0; k--)
                    {
                        child += pattern[j - k];
                    }
                    childs.Add(child);

                    if (j + 1 == pat_len)
                    {
                        j = lps[j];
                    }

                    j++;
                    i++;
                }
                else if (i < text_len && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i++;
                }
            }
            return childs.ToArray();
        }

        private static void ComputeLPSArray(string pattern, int pat_len, int[] lps)
        {
            int len = 0;
            lps[0] = 0;
            int i = 1;

            while (i < pat_len)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
        }
    }


}
