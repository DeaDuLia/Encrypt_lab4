using System.Numerics;

namespace lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1)
            ulong n = 565570077646207;
            ulong e = 12341;
            string s = "";
            string decr_str = "277140870674302260217431481485329310844916399448964498705119";
            ulong[] decr = new ulong[decr_str.Length];
            decr = Blocks(decr_str, n);
            N pq = Find_P_and_Q(n); 
            ulong p = pq.P;
            ulong q = pq.Q;
            // функци Эйлера Ф(n)=(p-1)(q-1) 
            ulong phi = (pq.P - 1) * (pq.Q - 1);
            // Поиск секретного ключа d
            ulong d = mod(e, phi);
            ulong[] M = new ulong[decr.Length];
            for (int i = 0; i < decr.Length; i++)
                M[i] = (ulong)BigInteger.ModPow(decr[i], d, n);

            for (int i = 0; i < decr.Length; i++)
                s += M[i].ToString();
            s = toString(s);
            Console.WriteLine(s);
        }

        static ulong[] Blocks(string S, ulong N)
        {
            string str = S;

            int t = N.ToString().Length;
            ulong[] C = new ulong[str.Length];
            int k = 0;
            while (str != "")
            {
                string s = "";
                if (t < str.Length)
                    s = str.Substring(0, t);
                else
                    s = str.Substring(0, str.Length);
                int p = 0;
                if (Convert.ToUInt64(s) <= N)
                    p = t;
                else
                    p = t - 1;
                string st = "";
                if (t < str.Length)
                {
                    st = str.Substring(0, p);
                    str = str.Substring(p);
                }
                else
                {
                    st = str.Substring(0, str.Length);
                    str = "";
                }
                C[k] = Convert.ToUInt64(st);
                k++;
            }
            Array.Resize(ref C, k);
            return C;
        }

        static N Find_P_and_Q(ulong N)
        {
            ulong s = (ulong)Math.Round(Math.Sqrt(N));
            ulong k = 1;
            ulong a = 0, b = 0;
            while (true)
            {
                ulong y = (s + k) * (s + k) - N;
                if ((ulong)Math.Sqrt(y) * (ulong)Math.Sqrt(y) - y == 0)
                {
                    a = s + k;
                    b = (ulong)Math.Sqrt(y);
                    break;
                }
                k++;
            }
            N pq = new N();
            pq.P = a + b;
            pq.Q = a - b;
            return pq;

        }

        static ulong mod(ulong e, ulong p)
        {
            ulong x = 0;
            uint k = 1;
            while (true)
            {
                ulong xx = 1 + k * p;
                if (xx % e == 0)
                {
                    x = xx / e;
                    break;
                }
                k++;
            }
            return x % p;
        }

        static string toString(string S)
        {
            string str = S;
            int k = 0;
            string[] C = new string[(S.Length / 2) + 1];
            while (str != "")
            {
                string st = "";
                if (2 < str.Length)
                {
                    st = str.Substring(0, 2);
                    str = str.Substring(2);
                }
                else
                {
                    st = str.Substring(0, str.Length);
                    str = "";
                }
                C[k] = st;
                k++;
            }
            for (int i = 0; i < C.Length; i++)
                str += Convert.ToChar(Convert.ToInt32(C[i]));

            return str;
        }

        struct N
        {
            public ulong P { get; set; }
            public ulong Q { get; set; }
        }
    }
}