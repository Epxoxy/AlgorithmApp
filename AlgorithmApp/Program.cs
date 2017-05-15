using System;

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

            Console.ReadKey();
        }

        static void printf(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + " ");
            Console.WriteLine();
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
}
