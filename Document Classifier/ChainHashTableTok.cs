using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_328_Asignment_2
{
class ChainHashTableTok
{
        class Node : Object
        {
            long key;
            public Token data;
            Node next;
        const int MAGICNUM = 37;
            public Node(Token data)
            {
                this.data = data;
                this.key = data.GetLongHashCode();
                next = null;
            }
            public long getkey()
            {
                return key;
            }
            public Token getdata()
            {
                return data;
            }
            public void setNextNode(Node obj)
            {
                next = obj;
            }
            public Node getNextNode()
            {
                return this.next;
            }
            private long calculateKey()
            {
                int Length = 0;
                key = 0;
                if (data.token.Length > 10)
                    Length = 10;
                else
                    Length = data.token.Length;
                int[] numbers = new int[Length];
                for(int i = 0; i<Length; i++)
                {
                    numbers[i] = data.token[i];
                    
                }
                key = (long)keyPoly(numbers, 37);
                //Console.WriteLine("Key of " + data + ": " + key);
                return key;
            }
            public int getLength()
            {
                Node currNode = this.next;
                int length = 1;
                while (currNode != null)
                {
                    length++;
                    currNode =currNode.getNextNode();
                }
                return length;
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
        }


        Node[] table;
        int size = 10;

        public ChainHashTableTok()
        {
            table = new Node[this.size];
            for (int i = 0; i < size; i++)
            {
                table[i] = null;
            }
        }

        public ChainHashTableTok(int size)
    {
        this.size = size;
        table = new Node[this.size];
        for(int i =0; i <size; i++)
        {
            table[i] = null;
        }
    }
        public void insert(Token data)
        {
            Node nObj = new Node(data);
            long hash = nObj.getkey() % size;
            
            if (table[hash] != null && hash == table[hash].getkey() % size)
            {
                nObj.setNextNode(table[hash].getNextNode());
                table[hash].setNextNode(nObj);
                return;
            }
            else
            {
                table[hash] = nObj;
                return;
            }
        }
        public Token retrieve(long key)
        {
            long hash = key % size;
            
            Node current = table[hash];
            if (ReferenceEquals(current, null))
                return null;
            while (current.getkey() != key && current.getNextNode() != null)
            {
                current = current.getNextNode();
            }
            if (current.getkey() == key)
            {
                return current.getdata();
            }
            else
            {
                return null;
            }
        }

        public Token retrieveIndex(int key)
        {
            int hash = key % size;
      
            Node current = table[hash];
            if (ReferenceEquals(current, null))
                return null;
            while (current.getkey() != key && current.getNextNode() != null)
            {
                current = current.getNextNode();
            }
            if (current.getkey() % size == key)
            {
                return current.getdata();
            }
            else
            {
                return null;
            }
        }

        public Token retrieve(Token tok)
        {
            long key = calculateKey(tok.token);
            long hash = key % size;
            
          
            Node current = table[hash];
            if (current == null)
                return null;
            while (current.getkey() != key && current.getNextNode() != null)
            {
                current = current.getNextNode();
            }
            if (current.getkey() == key)
            {
                return current.getdata();
            }
            else
            {
                return null;
            }
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
            
            Node current = table[hash];
            bool isRemoved = false;
            while (current!=null)
            {
                if (current.getkey() == key)
                {
                    
                    //Console.WriteLine("Removed " + table[hash].getdata().token);
                    //if (table[hash].getdata().token == "colorado")
                    //    Debugger.Break();
                    table[hash] = current.getNextNode();
                    
                    isRemoved = true;
                    break;
                }

                if (current.getNextNode() != null)
                {
                    if (current.getNextNode().getkey() == key)
                    {
                        Node newNext = current.getNextNode().getNextNode();
                        
                        //Console.WriteLine("Removed " + table[hash].getdata().token);
                        //if (current.getNextNode().getdata().token == "colorado")
                        //    Debugger.Break();
                        current.setNextNode(newNext);
                        
                        isRemoved = true;
                        break;
                    }
                    else
                    {
                        current = current.getNextNode();
                    }
                }
                else
                    break;

            }

            if (!isRemoved)
            {
                Console.WriteLine("nothing found to delete!");
                return;
            }
        }

        public void incrementToken(long key)
        {
            long hash = key % size;
      
            //a current node pointer used for traversal, currently points to the head
            Node current = table[hash];
            bool isRemoved = false;
            while (current != null)
            {
                if (current.getkey() == key)
                {
                    table[hash].data.frequency++;
                    break;
                }

                if (current.getNextNode() != null)
                {
                    if (current.getNextNode().getkey() == key)
                    {
                        Node newNext = current.getNextNode().getNextNode();
                        current.getNextNode().data.frequency++;
                        break;
                    }
                    else
                    {
                        current = current.getNextNode();
                    }
                }
                else
                    break;

            }
        }
        public String print()
        {
            Node current = null;
            StringBuilder text= new StringBuilder("");
            List<Token> tok = new List<Token>();
            for (int i = 0; i < size; i++)
            {
                //text += (i + ": ");
                current = table[i];
                while (current != null)
                {
                    tok.Add(current.getdata());
                    //text+=(current.getdata());
                    current = current.getNextNode();
                }
                //text+="\n";
            }
            var CountQuery = from x in tok
                             orderby x.frequency descending
                             select new { Value = x.token, Frequency = x.frequency };
            foreach(var tk in CountQuery)
            {
                text.Append("Value: " + tk.Value + " | Frequency: " + tk.Frequency + "\r\n");
            }

            return text.ToString();
        }

        public List<Token> ToList()
        {
            List<Token> list = new List<Token>();
            Node current = null;
            for(int i =0; i < size; i++)
            {
                if (!ReferenceEquals(this.retrieveIndex(i), null))
                {
                    current = table[i];
                    while (current != null)
                    {
                        list.Add(current.data);
                        current = current.getNextNode();
                    }
                    //if (i == 97)
                    //     Debugger.Break();
                    //Console.WriteLine("added " + i);
                    //list.Sort();
                }

                
            }
            list.Sort();
            //Debugger.Break();
            return list;
        }

        public int getLength(int hash)
        {
            return table[hash].getLength();
        }

        public int Size()
        {
            return size;
        }
    }

   
    
}
