using System;
using System.Diagnostics;

namespace ParallelCalculation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParallelCalc calc = new ParallelCalc((long)10E05,10);
            long result = calc.Sum();

            Console.WriteLine($"result = {result}");

        }
    }
    public class ParallelCalc
    {
        private int threadCount;
        private long max;
        long[] partialSums;

        public ParallelCalc(long max, int threadCount)
        {
            this.max = max;
            this.threadCount = threadCount;

            partialSums = new long[threadCount];
        }

        private void PartialSum(object? r)
        {
            Range? range = r as Range;
            if (range == null)
            {
                return;
            }

            long partialSum = 0;
            for (long i = range.Start + 1; i <= range.End; i++)
            {
                partialSum += i;
            }

            partialSums[range.Index] = partialSum;
        }

        public long Sum ()
        {
            Thread[] threads = new Thread[threadCount];
            long range = max / threadCount;

            for (int i = 0; i < threads.Length; i++)
            {
                long start = i * range;
                long end = (i == threadCount - 1) ? max : (i + 1) * range;
                threads[i] = new Thread(PartialSum);
                threads[i].Name = $"Sum Thread {i}";
                threads[i].Start(new Range()
                {
                    Start = start,
                    End = end,
                    Index = i
                });

            }
            foreach (Thread thread in threads)
            {
                thread.Join();
            }
            long result = partialSums.Sum();
            return partialSums.Sum();
        }
       
        private class Range
        {
            public long Start {  get; set; }
            public long End { get; set; }
            public int Index { get; set; }
        }

    }
}