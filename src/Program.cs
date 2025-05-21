
public class Program
{
    static public void Main(string[] args)
    {


        MulModPrimeHash mulModPrimeHash = new MulModPrimeHash();
        MulShiftHash mulShiftHash = new MulShiftHash();

        //instantiate hastable with the hash function and size
        HashTable hashTableMod = new HashTable(h: x => mulModPrimeHash.Hash(x), 64);
        HashTable hashTableShift = new HashTable(h: x => mulShiftHash.Hash(x), 64);
        

    }


}
