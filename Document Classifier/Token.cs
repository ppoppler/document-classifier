using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CECS_328_Asignment_2
{
    public class Token : IComparable<Token>
    {
        public String token;
        public int a_count;
        public int b_count;
        public int count;
        public double a_corr;
        public double b_corr;
        public double frequency;

        public Token()
        {
            token = "";
            a_count = 0;
            b_count = 0;
        }


        private long keyPoly(int[] numbers, long x)
        {
            long result = 0;

            for (int i = numbers.Length - 1; i >= 0; i--)
            {
                result = result * x + numbers[i];
            }

            return result;
        }
        public long GetLongHashCode()
        {
            int Length = 0;
            if (token.Length > 10)
                Length = 10;
            else
                Length = token.Length;
            int[] numbers = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                numbers[i] = token[i];

            }
           long key = (long)keyPoly(numbers, 37);
            
            return key;

        }

        public Token(String token)
        {
            this.token = token;
            a_count = 0;
            b_count = 0;
        }

        public Token(String token, double frequency)
        {
            this.token = token;
            this.frequency = frequency;
            a_count = 0;
            b_count = 0;
        }

        public Token(String token, int a_count, int b_count)
        {
            this.token = token;
            this.a_count = a_count;
            this.b_count = b_count;
        }

        public Token(String token, int count)
        {
            this.token = token;
            this.count = count;
            this.frequency = 1/count;
            a_count = 0;
            b_count = 0;
        }
        
        public override String ToString()
        {
            return "Token: " + token + " | Frequency: " + frequency +"\r\n";
        }

        public String ACorrelation()
        {
            String str = "Religious Correlations: \r\n";
            if(token.Length<=16)
                str += ("Token: " + token).PadRight(80) + ("\r\t\tCorrelation: " + a_corr).PadLeft(5) + "\r\n";
            else
                str += ("Token: " + token).PadRight(80) + ("\r\tCorrelation: " + a_corr).PadLeft(5) + "\r\n";
            return str;
        }

        public String BCorrelation()
        {
            String str = "Religious Correlations: ";
            if (token.Length <= 16)
                str += ("Token: " + token).PadRight(80) + ("\r\t\tCorrelation: " + b_corr).PadLeft(5) + "\r\n";
            else
                str += ("Token: " + token).PadRight(80) + ("\r\tCorrelation: " + b_corr).PadLeft(5) + "\r\n";
            return str;
        }


        public override bool Equals(Object tok1)
        {
            Token tok = (Token)tok1;
            if (ReferenceEquals(tok, null))
                return false;
            if (tok.token == this.token)
                return true;
            return false;
        }

        public int CompareTo(Token other)
        {
            return (int)(other.frequency - this.frequency);
        }

        // public override String ToString()
        //{
        //    return "Token: " + token + " | A Count: " + a_count + " | B Count: " + b_count;
        // }
    }
}
