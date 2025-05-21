public class Opg3
{
    public static void Run()
    {

        UInt16 l = 16;
        int n = 94967298; 
        Tuple<ulong, int>[] stream = Stream.CreateStream(n, l).ToArray();
        MulShiftHash mulShiftHash = new MulShiftHash(l);

        HashTable hashTable = populateHashTable(mulShiftHash.Hash, stream, l);
        Int64 sum = 0;

        foreach (var list in hashTable.table)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    sum += item.Item2 * item.Item2;
                }
            }
        }

        Console.WriteLine($"Sum: {sum}");

    }

    public static HashTable populateHashTable(Func<Int64, UInt64> h, Tuple<ulong, int>[] dataStream, int l)
    {
        HashTable hashTable = new HashTable(h, l);
        Console.WriteLine($"HashTable size: {hashTable.table.Length}");
        foreach (var item in dataStream)
        {
            hashTable.increment((Int64)item.Item1, item.Item2);
        }
        return hashTable;
    }
        

}