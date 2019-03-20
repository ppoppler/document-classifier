using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_328_Asignment_2
{
class ChainHashTable
{
        class Node
        {
            long key;
            string data;
            Node next;
        const int MAGICNUM = 37;
            public Node(string data)
            {
                this.data = data;
                this.key = calculateKey();
                next = null;
            }
            public long getkey()
            {
                return key;
            }
            public string getdata()
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
                if (data.Length > 10)
                    Length = 10;
                else
                    Length = data.Length;
                for(int i = 0; i<Length; i++)
                {

                    key += Convert.ToChar(data[i]) * Convert.ToInt64(Math.Pow(MAGICNUM, Length-1-i));
                }
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
        }


        Node[] table;
        int size = 10;

        public ChainHashTable()
        {
            table = new Node[this.size];
            for (int i = 0; i < size; i++)
            {
                table[i] = null;
            }
        }

        public ChainHashTable(int size)
    {
        this.size = size;
        table = new Node[this.size];
        for(int i =0; i <size; i++)
        {
            table[i] = null;
        }
    }
        public void insert(string data)
        {
            Node nObj = new Node(data);
            long hash = nObj.getkey() % size;
            while (table[hash] != null && table[hash].getkey() % size != nObj.getkey() % size)
            {
                hash = (hash + 1) % size;
            }
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
        public string retrieve(int key)
        {
            int hash = key % size;
            while (table[hash] != null && table[hash].getkey() % size != key % size)
            {
                hash = (hash + 1) % size;
            }
            Node current = table[hash];
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
                return "nothing found!";
            }
        }
        public void remove(int key)
        {
            int hash = key % size;
            while (table[hash] != null && table[hash].getkey() % size != key % size)
            {
                hash = (hash + 1) % size;
            }
            //a current node pointer used for traversal, currently points to the head
            Node current = table[hash];
            bool isRemoved = false;
            while (current != null)
            {
                if (current.getkey() == key)
                {
                    table[hash] = current.getNextNode();
                    Console.WriteLine("Remeoved");
                    isRemoved = true;
                    break;
                }

                if (current.getNextNode() != null)
                {
                    if (current.getNextNode().getkey() == key)
                    {
                        Node newNext = current.getNextNode().getNextNode();
                        current.setNextNode(newNext);
                        Console.WriteLine("Removed");
                        isRemoved = true;
                        break;
                    }
                    else
                    {
                        current = current.getNextNode();
                    }
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
            Node current = null;
            String text="";
            for (int i = 0; i < size; i++)
            {
                text += (i + ": ");
                current = table[i];
                while (current != null)
                {
                    text+=(current.getdata() + ", ");
                    current = current.getNextNode();
                }
                text+="\n";
            }
            return text;
        }

        public int getLength(int hash)
        {
            return table[hash].getLength();
        }

       
    }

   
    
}
