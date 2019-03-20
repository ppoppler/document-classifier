using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_328_Asignment_2
{
class QuadProbeHashTableTok
{
        class Node
        {
            public long key;
            public Token data;

        const int MAGICNUM = 37;
            public Node(Token data)
            {
                this.data = data;
                this.key = calculateKey(data.token);

            }
            public long getkey()
            {
                return key;
            }
            public Token getdata()
            {
                return data;
            }

            public long keyPoly(int[] numbers, long x)
            {
                long result = 0;

                for (int i = numbers.Length - 1; i >= 0; i--)
                {
                    result = result * x + numbers[i];
                }

                return result;
            }

            private long calculateKey(String data)
            {
                int Length = 0;
                long key = 0;
                if (data.Length > 10)
                    Length = 10;
                else
                    Length = data.Length;
                int[] numbers = new int[Length];
                for (int i = 0; i < Length; i++)
                {
                    numbers[i] = data[i];

                }
                key = (long)keyPoly(numbers, 37);
                //Console.WriteLine("Key of " + data + ": " + key);
                return key;
            }
        }


        Node[] table;
        int size = 10;
        int probeCount = 0;

        public QuadProbeHashTableTok()
        {
            table = new Node[this.size];
            for (int i = 0; i < size; i++)
            {
                table[i] = null;
            }
        }

        public QuadProbeHashTableTok(int size)
        {
            this.size = size;
            table = new Node[this.size];
            for(int i =0; i <size; i++)
        {
            table[i] = null;
        }
            //Debugger.Break();
    }
        public void insert(Token data)
        {
            
            Node nObj = new Node(data);

            long hash = nObj.key % size;
            
            probeCount = 0;
            //while (table[hash] != null && hash == table[hash].getkey() % size)
            //{
            //    probeCount++;
            //    int movement = Convert.ToInt32(Math.Pow(probeCount, 2));
            //    hash = (hash + movement) % size;
            //}
            //table[hash] = nObj;

            if (table[hash] != null)
            {
                probe(nObj, hash);
                return;
            }
            else if (table[hash] == null)
            {
                table[hash] = nObj;
                return;
            }
        }
        public void howMany()
        {
            int count = 0;
            for(int i = 0; i <size; i++)
            {
                if (table[i] != null)
                    count++;
            }
            Console.WriteLine(count);
        }
        public Token retrieve(long key)
        {
            long hash = key % size;
            Node current = table[hash];
            while (current != null)
            {
                if (current.getkey() == key)
                {
                    probeCount = 0;
                    return current.getdata();
                }
                else if (Equals(current.key, null)) { 
                probeCount = 0;
                break;
                }
                else
                {
                    long movement = Convert.ToInt32(Math.Pow(probeCount, 2));
                    current = table[(hash + movement) % size];
                    probeCount++;
                }
            }
            return null;
        }



        public void incrementToken(long key)
        {
            long hash = key % size;
            probeCount = 0;
            //a current node pointer used for traversal, currently points to the head
            //Node original = table[hash];
            Node current = table[hash];
            while (current != null)
            {
                if (current.getkey() == key)
                {
                    table[hash].data.frequency++;
                    probeCount = 0;
                    break;
                }
                else if (Equals(current.key, null))
                {
                    probeCount = 0;
                    return;
                }
                else
                {
                    long movement = Convert.ToInt32(Math.Pow(probeCount, 2));
                    current = table[(hash + movement) % size];
                    probeCount++;
                }
            }
        }

        public List<Token> ToList()
        {
            
            List<Token> list = new List<Token>();
            Node current = null;
            
            for(int i =0; i<size; i++)
            {
                current = table[i];
                if(table[i]!=null)
                    list.Add(new CECS_328_Asignment_2.Token(current.data.token, current.data.frequency));
            }
            //Debugger.Break();
            list.Sort();
            //howMany();
            //Debugger.Break();
            return list;
        }

        public long keyPoly(int[] numbers, long x)
        {
            long result = 0;

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                result = result * x + numbers[i];
            }

            return result;
        }

        private long calculateKey(String data)
        {
            int Length = 0;
            long key = 0;
            if (data.Length > 10)
                Length = 10;
            else
                Length = data.Length;
            int[] numbers = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                numbers[i] = data[i];

            }
            key = (long)keyPoly(numbers, 37);
            //Console.WriteLine("Key of " + data + ": " + key);
            return key;
        }

        public void remove(long key)
        {
            long hash = key % size;
          
            //a current node pointer used for traversal, currently points to the head
            Node current = table[hash];
            bool isRemoved = false;
            while (current != null)
            {
                if (current.getkey() == key)
                {
                    table[hash] = null;
                    Console.WriteLine("Removed");
                    isRemoved = true;
                    break;
                }
                
            }

            if (!isRemoved)
            {
                Console.WriteLine("nothing found to delete!");
                return;
            }
        }
        public String print()
        {
            StringBuilder text = new StringBuilder("");
            Node current = null;
            //String text = "";
            List<Token> tok = new List<Token>();
            for (int i = 0; i < size; i++)
            {
                //text += (i + ": ");
                current = table[i];
                if (current != null)
                {
                    tok.Add(current.getdata());
                }
                //text+="\n";
            }
            var CountQuery = from x in tok
                             orderby x.frequency descending
                             select new { Value = x.token, Frequency = x.frequency };
            foreach (var tk in CountQuery)
            {
                text.Append("Value: " + tk.Value + " | Frequency: " + tk.Frequency + "\r\n");
            }

            return text.ToString();
        }
        private void probe(Node nObj, long hash)
        {
            probeCount = 0;
            long movement = Convert.ToInt32(Math.Pow(probeCount, 2));
            while (table[(hash + movement) % size] != null && hash == table[hash].getkey() % size)
            {
                
                probeCount++;
                movement = Convert.ToInt32(Math.Pow(probeCount, 2));
                if (table[(hash + movement) % size] == null) {
                    table[(hash + movement) % size] = nObj;
                    probeCount = 0;
                    return;
                }

            }
        }
    }
    
}
