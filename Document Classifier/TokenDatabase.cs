using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_328_Asignment_2
{
    public partial class TokenDatabase : DataContext
    {
        public Table<Token> Tokens;
        public TokenDatabase(string connection) : base(connection) { }
    }
}