using System.Diagnostics;

public class Opg3
{
    public static void Run()
    {
        Stopwatch sw = new Stopwatch();

        UInt16[] l_values = { 2, 4, 6, 8, 16, 20};
        int n = (1<<20) + 1;
        Console.WriteLine($"For all experiments, we are using n = {n}");
        foreach (UInt16 l in l_values)
        {
            Tuple<ulong, int>[] stream = Stream.CreateStream(n, l).ToArray();

            Console.WriteLine($"For l = {l}");
            sw.Restart();
            MulShiftHash mulShiftHash = new MulShiftHash(l);


            HashTable shifthashTable = populateHashTable(mulShiftHash.Hash, stream, l);
            Int64 shiftsum = 0;

            foreach (var list in shifthashTable.table)
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        shiftsum += item.Item2 * item.Item2;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"MulShiftHash sum is = {shiftsum}"); 
            Console.WriteLine($"MulShiftHas spent {sw.ElapsedMilliseconds} ms");

            sw.Restart();
            MulModPrimeHash mulModPrimeHash = new MulModPrimeHash(l);


            HashTable modhashTable = populateHashTable(mulModPrimeHash.Hash, stream, l);
            Int64 modsum = 0;

            foreach (var list in modhashTable.table)
            {
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        modsum += item.Item2 * item.Item2;
                    }
                }
            }
            sw.Stop();
            Console.WriteLine($"MulModPrimeHash sum is = {modsum}");
            Console.WriteLine($"MulModPrime Has spent {sw.ElapsedMilliseconds} ms");

        }


    }

    public static HashTable populateHashTable(Func<UInt64, UInt64> h, Tuple<ulong, int>[] dataStream, int l)
    {
        HashTable hashTable = new HashTable(h, l);
        foreach (var item in dataStream)
        {
            hashTable.increment(item.Item1, item.Item2);
        }
        return hashTable;
    }


}
