using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CECS_328_Asignment_2
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       
        public static List<Token> tokens = new List<Token>(); 
        public static msdbDataSetTableAdapters.TableTableAdapter tabletableAdapter;
        public static msdbDataSet.TableDataTable dt;
        static void Main()
        {

            ChainHashTable table = new ChainHashTable(6);
            table.insert("Spain");
            table.insert("Spain");
            table.insert("France");
            table.insert("Germany");
            table.insert("England");
            Console.WriteLine(table.print());
            //Console.WriteLine(table.getLength(ele))
            Console.WriteLine(table.getLength(3));

            //QuadProbeHashTable table2 = new QuadProbeHashTable(10);
            //table2.insert("Spain");
            //table2.insert("Spain");
            //table2.insert("Spain");
            //Console.WriteLine(table2.print());

            /**
             * Following Code Reads from the database of words and then stores them in a list of tokens. 
             **/
            tabletableAdapter = new msdbDataSetTableAdapters.TableTableAdapter();
           
            dt = tabletableAdapter.GetData();

            
            

           // foreach (DataRow dr in dt.Rows)
           // {
           //     tabletableAdapter.Delete(dr["Token_Name"].ToString(), Convert.ToInt16(dr["A_Count"].ToString()), Convert.ToInt16(dr["B_Count"].ToString()));
           // }
           //  tabletableAdapter.Delete("The",40,60);
            
            //foreach(DataRow dr in dt)
            //{
            //    tokens.Add(new Token(dr["Token_Name"].ToString(), Convert.ToInt16(dr["A_Count"].ToString()), Convert.ToInt16(dr["B_Count"].ToString())));
            //}
            //foreach(Token tk in tokens)
            //{
            //   Console.WriteLine(tk.ToString());
            //}

            var thread = new System.Threading.Thread(delegate ()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            
        }
    }
}
