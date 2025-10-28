using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// fix me :
namespace AntsTown
{

    public class MinHeapNode
    {
        public char data;
        public uint number;
        public MinHeapNode left, right;

        public MinHeapNode(char data, uint number)
        {
            left = right = null;
            this.data = data;
            this.number = number;
        }
    }

    // For comparison of two heap nodes (needed in min heap) 
    public class CompareMinHeapNode : IComparer<MinHeapNode>
    {
        public int Compare(MinHeapNode x, MinHeapNode y)
        {
            return x.number.CompareTo(y.number);
        }
    }




    public class CharBited
    {
        public char character;
        public bool[] code;
        public CharBited(char character, bool[] bits)
        {
            this.character = character;
            this.code = bits;
        }
    }

    //public class CharReapet
    //{
    //    public char character;
    //    public int reapet;
    //    public CharReapet(char character, int reapet)
    //    {
    //        this.character = character;
    //        this.reapet = reapet;
    //    }
    //}

    
    public class Huffman  //fix me : internal ? public ?
    {
        static void makeCodes(MinHeapNode root, string temp, Dictionary<char, string> codes)
        {
            if (root == null)
                return;

            if (root.data != '$')
                codes.Add(root.data, temp);


            makeCodes(root.left, temp + "0", codes);
            makeCodes(root.right, temp + "1", codes);
        }

        public static Dictionary<char, string> makeTree(Dictionary<char, uint> charReapet)
        {
            MinHeapNode left, right, top;

            // Create mean heap & inserts 
            var minHeap = new List<MinHeapNode>();
                        
            foreach (var c in charReapet)
                minHeap.Add(new MinHeapNode(c.Key, c.Value));

            minHeap.Sort((a, b) => a.number.CompareTo(b.number)); 


            while (minHeap.Count > 1)
            {
                left = minHeap[0];
                minHeap.Remove(left);

                right = minHeap[0];
                minHeap.Remove(right);

                top = new MinHeapNode('$', left.number + right.number);
                top.left = left;
                top.right = right;

                minHeap.Add(top);
                minHeap.Sort((a, b) => a.number.CompareTo(b.number)); //fix me : sort is not good
            }

            Dictionary <char , string> result = new Dictionary<char , string>();

            makeCodes(minHeap[0] ,"", result);

            return result;
        }
    }





    public class BinaryReadWriteClass
    {
        public void WriteBinary()
        {

        }
    }
}