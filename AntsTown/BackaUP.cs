//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//// fix me :
//namespace AntsTown
//{

//    public class MinHeapNode
//    {
//        public char data;
//        public uint number;
//        public MinHeapNode left, right;

//        public MinHeapNode(char data, uint number)
//        {
//            left = right = null;
//            this.data = data;
//            this.number = number;
//        }
//    }

//    // For comparison of two heap nodes (needed in min heap) 
//    public class CompareMinHeapNode : IComparer<MinHeapNode>
//    {
//        public int Compare(MinHeapNode x, MinHeapNode y)
//        {
//            return x.number.CompareTo(y.number);
//        }
//    }

//    public class CharBited
//    {
//        public char character;
//        public bool[] code;
//        public CharBited(char character, bool[] bits)
//        {
//            this.character = character;
//            this.code = bits;
//        }
//    }

//    //public class CharReapet
//    //{
//    //    public char character;
//    //    public int reapet;
//    //    public CharReapet(char character, int reapet)
//    //    {
//    //        this.character = character;
//    //        this.reapet = reapet;
//    //    }
//    //}


//    public class Huffman  //fix me : internal ? public ?
//    {
//        static void makeCodes(MinHeapNode root, string temp, Dictionary<char, string> codes)
//        {
//            if (root == null)
//                return;

//            if (root.data != '$')
//                codes.Add(root.data, temp);


//            makeCodes(root.left, temp + "0", codes);
//            makeCodes(root.right, temp + "1", codes);
//        }

//        public static Dictionary<char, string> makeTree(Dictionary<char, uint> charReapet, int size)
//        {
//            foreach (var c in charReapet)
//            {
//                Console.WriteLine($"<<< char={c.Key} : num={c.Value} >>> ");
//            }
//            MinHeapNode left, right, top;

//            // Create mean heap & inserts 
//            var minHeap = new SortedSet<MinHeapNode>(new CompareMinHeapNode());// fix me : khademi : i used sorted list : sort method = red black 

//            foreach (var c in charReapet)
//            {
//                minHeap.Add(new MinHeapNode(c.Key, c.Value));
//                Console.WriteLine($"<< char={c.Key} : num={c.Value} >> ");
//            }


//            foreach (var c in minHeap)
//            {
//                Console.WriteLine($"# char={c.data} : num={c.number} # ");
//            }

//            while (minHeap.Count != 1)
//            {
//                left = minHeap.Min;
//                minHeap.Remove(left);

//                right = minHeap.Min;
//                minHeap.Remove(right);

//                top = new MinHeapNode('$', left.number + right.number);
//                top.left = left;
//                top.right = right;

//                minHeap.Add(top);
//            }

//            //List <CharBited> codes = new List<CharBited>(); //res
//            //List <bool> temp = new List<bool>(); 
//            Dictionary<char, string> result = new Dictionary<char, string>();

//            Console.WriteLine("HeapTree :");

//            Console.WriteLine(":PrintHeapTree ");


//            //PrintHeapTree(minHeap.Min);
//            makeCodes(minHeap.Min, "", result);

//            //Console.WriteLine("res:");
//            //foreach (var item in result) //fix me : test
//            //{
//            //    Console.WriteLine($">{item.Key} = {item.Value}");
//            //}


//            return result;
//        }

//        public static void PrintHeapTree(MinHeapNode root) //fix me : delete me
//        {
//            if (root == null)
//                return;
//            Console.WriteLine($"< char={root.data} : num={root.number} > ");
//            PrintHeapTree(root.left);
//            PrintHeapTree(root.right);
//        }
//    }





//    public class BinaryReadWriteClass
//    {
//        public void WriteBinary()
//        {

//        }
//    }
//}