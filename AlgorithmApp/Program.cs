using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AlgorithmApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = { 80, 30, 60, 40, 20, 10, 50, 70 };
            int[] b = { 80, 30, 60, 40, 20, 10, 50, 70 };

            Console.WriteLine("\nMerge sort");
            printf(b);
            split(ref b, 0, b.Length - 1);
            printf(b);

            Console.WriteLine("\nQuick sort");
            printf(a);
            quickSort(a, 0, a.Length - 1);
            printf(a);

            Console.WriteLine("\nMake change");
            int[] changes = makeChange(83, new int[] { 50, 20, 10, 5, 1 });
            printf(changes);

            Console.WriteLine("\nEgypt fraction");
            egyptFraction(6, 7);
            
            Console.WriteLine("\nMin matrix multiply times");
            int[] matrix = new int[] { 30, 35, 15, 5, 10, 20, 25 };
            int[,] m, s;
            minMatrixMultiplyTimes(matrix, matrix.Length - 1, out m, out s);
            Console.WriteLine(m[0, m.GetLength(0) - 1]);
            printMatrix(s, 0, m.GetLength(0) - 1);

            //For testing prime ring
            //Not solve -> Use long time
            //new PrimeRing().run();

            //For testing horse go
            //Not solve -> Use long time
            //new HorseGoTest().run();

            new WordChains().run();

            Console.ReadKey();
        }

        static void printf(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");
            Console.WriteLine();
        }

        static void debugf(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                System.Diagnostics.Debug.Write(a[i] + " ");
            System.Diagnostics.Debug.WriteLine("");
        }

        static void printMatrix(int[,] s, int l, int r)
        {
            if (l == r)
                Console.Write("A" + l);
            else
            {
                Console.Write("(");
                printMatrix(s, l, l + s[l, r]);
                printMatrix(s, l + s[l, r] + 1, r);
                Console.Write(")");
            }
        }
        
        static void minMatrixMultiplyTimes(int[] p, int num, out int[,] m, out int[,] s)
        {
            int i, j, k;
            m = new int[num, num];
            s = new int[num, num];

            for (i = 0; i < num; i++)
                m[i, i] = 0;

            for (i = 2; i <= num; i++)
            {
                for (j = 0; j < num - i + 1; j++)
                {
                    m[j, j + i - 1] = int.MaxValue;
                    int current;
                    for (k = 0; k < i - 1; k++)
                    {
                        current = m[j, j + k] + m[j + k + 1, j + i - 1] + p[j] * p[j + k + 1] * p[j + i];
                        if (m[j, j + i - 1] > current)
                        {
                            m[j, j + i - 1] = current;
                            s[j, j + i - 1] = k;
                        }
                    }
                }
            }
        }
        
        static int[] makeChange(int money, int[] changes)
        {
            int[] result = new int[changes.Length];
            for(int i = 0; i < changes.Length; i++)
            {
                result[i] = money / changes[i];
                money = money % changes[i];
                if (money == 0)
                    break;
            }
            return result;
        }

        //n: numerator
        //m: denominator
        static void egyptFraction(int n, int m)
        {
            int c;
            while(m % n != 0)
            {
                c = m / n + 1;
                n = n * c - m;
                m = m * c;
                Console.Write("1/" + c +" ");
            }
            Console.Write("1/" + m / n+ "\n");
        }

        static void quickSort(int[] a, int l, int r)
        {
            if(l < r)
            {
                int i = l, j = r, x = a[l];
                while(i < j)
                {
                    while (i < j && a[j] >= x)
                        j--;
                    if (i < j)
                        a[i++] = a[j];
                    while (i < j && a[i] < x)
                        i++;
                    if (i < j)
                        a[j--] = a[i];
                }
                a[i] = x;
                quickSort(a, l, i - 1);
                quickSort(a, i + 1, r);
            }
        }

        static void split(ref int[] a, int start, int end)
        {
            if (a == null || start >= end) return;
            int mid = (start + end) / 2;
            split(ref a, start, mid);
            split(ref a, mid + 1, end);
            merge(ref a, start, mid, end);
        }

        static void merge(ref int[] a, int start, int mid, int end)
        {
            int i = start, j = mid + 1, k = 0;
            int[] tmp = new int[end - start + 1];

            while(i <= mid && j <= end)
            {
                if (a[i] <= a[j])
                    tmp[k++] = a[i++];
                else
                    tmp[k++] = a[j++];
            }

            while (i <= mid)
                tmp[k++] = a[i++];

            while (j <= end)
                tmp[k++] = a[j++];

            for (i = 0; i < k; i++)
                a[start + i] = tmp[i];
        }

    }

    class PrimeRing
    {
        private int[] primes = null;
        private int total = 0;
        public void run()
        {
            Console.WriteLine("\nPrimeRing start.");
            int[] src = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            int[] rt = new int[src.Length];
            //Help array
            primes = genPrimes(sumMaxSum(src) + 1);
            total = 0;

            rt[0] = src[0];
            src[0] = 0;
            fitNext(src, rt, rt[0], 1);
            Console.WriteLine("Total " + total);
            Console.WriteLine("PrimeRing completed.");
        }

        int sumMaxSum(int[] a)
        {
            int max = 0, subMax = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > max)
                {
                    subMax = max;
                    max = a[i];
                }
            }
            return subMax + max;
        }

        void printf(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");
            Console.WriteLine();
        }
        
        int[] genPrimes(int n)
        {
            int[] a = new int[n];
            for (int i = 2; i < a.Length; i++)
                a[i] = 1;
            for (int i = 2; i < n; i++)
                if (a[i] == 1)
                    for (int j = i + i; j < n; j += i)
                        if (j % i == 0)
                            a[j] = 0;
            return a;
        }
        
        void fitNext(int[] s, int[] rs, int last, int k)
        {
            if (k == rs.Length)
            {
                if (primes[rs[k - 1] + rs[0]] == 1)
                {
                    ++total;
                    //printf(rs);
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] <= 0) continue;
                    if (primes[last + s[i]] == 1)
                    {
                        //set value to find next.
                        int nlast = s[i];
                        rs[k] = nlast;
                        s[i] = 0;

                        fitNext(s, rs, rs[k], k + 1);

                        //restore value when last brach completed.
                        rs[k] = 0;
                        s[i] = nlast;
                    }
                }
            }
        }

        //Not in use
        static bool isPrime6(int n)
        {
            if (n < 4) return n > 1;
            else if (n % 2 == 0 || n % 3 == 0)
                return false;

            int k = (int)Math.Sqrt(n) + 1;
            for (int i = 5; i <= k; i += 2)
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
            return true;
        }

    }

    class HorseGoTest
    {
        private int[] dx = new int[] { -2, -1, 1, 2,  2,  1, -1, -2 };
        private int[] dy = new int[] {  1,  2, 2, 1, -1, -2, -2, -1 };
        private int[,] board;
        private int valid = 0;
        private int max = 6;
        private int total;
        
        public void run()
        {
            Console.WriteLine("HorseGoTest start.");
            board = new int[max, max];
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    board[i, j] = 0;
            total = 0;
            board[0, 0] = 1;
            goNext(0, 0, 1);
            Console.WriteLine("Total " + total);
            System.Diagnostics.Debug.WriteLine("Total " + total);
            Console.WriteLine("HorseGoTest completed.");
            //524486
        }

        private void printf(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(string.Format(" | {0:D2}", a[i, j]));
                }
                Console.WriteLine("\n -----------------------------");
            }
        }

        private void goNext(int x, int y, int k)
        {
            if (k == max * max)
            {
                ++total;
                //524486
                //Console.WriteLine("\n -------- graph start --------");
                //printf(board);
            }
            else
            {
                for (int i = 0; i < dx.Length; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];
                    if(isValid(nx, ny))
                    {
                        board[nx, ny] = k + 1;
                        goNext(nx, ny, k + 1);
                        board[nx, ny] = valid;
                    }
                }
            }
        }

        bool isValid(int x, int y)
        {
            return x >= 0 && y >= 0
                && x < board.GetLength(0) 
                && y < board.GetLength(1)
                && board[x, y] == valid;
        }
    }


    class WordChains
    {
        private int max;
        private List<int[]> maxList;
        private char yes = '1';
        private char no = '0';
        public void run()
        {
            string[] words = { "arachnid", "aloha", "dog", "rat", "tiger", "gopher" };
            char[,] set = new char[words.Length, 3];

            //Initilize
            for (int i = 0; i < words.Length; i++)
            {
                set[i, 0] = words[i][0];//First char
                set[i, 1] = words[i][words[i].Length - 1];//Last char
                set[i, 2] = yes;//State
            }

            Console.WriteLine("\nWordChains test");

            max = 0;
            maxList = new List<int[]>();
            doFind(set, new int[words.Length], char.MinValue, 0);
            //Print result
            foreach (var rt in maxList)
            {
                for (int i = 0; i < rt.Length; i++)
                    Console.Write(words[rt[i] - 1] + " ");
                Console.WriteLine();
            }
        }

        private void doFind(char[,] c, int[] rt, char last, int k)
        {
            if (k == rt.Length)
            {
                tryAddMax(rt);
            }
            else
            {
                for (int i = 0; i < c.GetLength(0); i++)
                {
                    if (c[i, 2] == no) continue;
                    if (k == 0 || last == c[i, 0])
                    {
                        //Set status to no
                        //Ensure not visit it again
                        c[i, 2] = no;
                        rt[k] = i + 1;

                        //Base on this one found
                        //To find next one
                        doFind(c, rt, c[i, 1], k + 1);

                        //Recover default value
                        //For continue next test
                        rt[k] = 0;
                        c[i, 2] = yes;
                    }
                }
                tryAddMax(rt);
            }
        }

        private void tryAddMax(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == 0)
                {
                    if (i < max) break;
                    if (i > max)
                    {
                        max = i;
                        maxList.Clear();
                    }
                    int[] rt = new int[i];
                    for (int j = 0; j < rt.Length; j++)
                        rt[j] = a[j];
                    maxList.Add(rt);
                }
            }
        }
    }
}
