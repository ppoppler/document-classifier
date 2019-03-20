using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_328_Asignment_2
{
class QuadProbeHashTable
{

        double loadFactor;
        class Node
        {
            long key;
            string data;

        const int MAGICNUM = 37;
            public Node(string data)
            {
                this.data = data;
                this.key = calculateKey();

            }
            public long getkey()
            {
                return key;
            }
            public string getdata()
            {
                return data;
            }
           
            private long calculateKey()
            {
                key = 0;
                for(int i = 0; i<this.data.Length; i++)
                {
                    key += Convert.ToChar(data[i]) * Convert.ToInt64(Math.Pow(MAGICNUM, this.data.Length-1-i));
                }
                //Console.WriteLine("Key of " + data + ": " + key);
                return key;
            }
        }


        Node[] table;
        int size = 10;
        int probeCount = 0;

        public QuadProbeHashTable()
        {
            table = new Node[this.size];
            for (int i = 0; i < size; i++)
            {
                table[i] = null;
            }
        }

        public QuadProbeHashTable(int size)
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
            loadFactor = calculateLoadFactor();
            if (loadFactor >= 0.5)
            {
                resize();
            }
            Node nObj = new Node(data);
            long hash = nObj.getkey() % size;
            while (table[hash] != null && table[hash].getkey() % size != nObj.getkey() % size)
            {
                hash = (hash + 1) % size;
            }
            if (table[hash] != null && hash == table[hash].getkey() % size)
            {

                probe(nObj, hash);
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
            //while (current.getkey() != key && current.getNextNode() != null)
            //{
            //    current = current.getNextNode();
            //}
            if (current.getkey() == key)
            {
                return current.getdata();
            }
            else
            {
                return "nothing found!";
            }
        }
        public string remove(int key)
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
                    table[hash] = null;
                    Console.WriteLine("Removed");
                    isRemoved = true;
                    return current.getdata();
                }
                
            }

           
                Console.WriteLine("nothing found to delete!");
                return "";
           
        }
        public String print()
        {
            Node current = null;
            String text="";
            for (int i = 0; i < size; i++)
            {
                text += (i + ": ");
                current = table[i];
                if (current!= null)
                    text += (current.getdata());
             
                text+="\n";
            }
            return text;
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
                    Console.WriteLine((hash + movement) % 3);
                    probeCount = 0;
                    return;
                }

            }
        }

        private void resize()
        {
            int newSize = HashHelpers.GetPrime(size*2);
            Node[] temp = new Node[size];
            int oldsize = size;
            for (int i = 0; i < size; i++)
                if(table[i]!=null)
                temp[i] = new Node(table[i].getdata());
            
            this.table = new Node[newSize];
            this.size = newSize;
            for (int i = 0; i< oldsize; i++)
            {
                if(temp[i]!=null)
                    this.insert(temp[i].getdata());
            }
            
        }

        private double calculateLoadFactor()
        {
            int elements = 0;
            Node current = table[0];
            for(int i =0; i<size; i++)
            {
                current = table[i];
                if (current != null)
                    elements++;
                
            }
            Console.WriteLine(elements);
            return (elements / (double)(size));
        }
    }
    
}
