public class Opg3
{
    public static void Run()
    {


    }

    public static void populateHashTable(Func<Int64, Int64> h, Tuple<ulong, int>[] dataStream)
    {
        HashTable hashTable = new HashTable(h, 64);
        foreach (var item in dataStream)
        {
            hashTable.increment((Int64)item.Item1, item.Item2);
        }
    }
        

}