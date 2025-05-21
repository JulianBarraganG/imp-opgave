
    public class Program
{
    static public void Main(string[] args)
    {
        MulShiftHash hash = new MulShiftHash();
        Int64 x = 2;
        Int64 result = hash.Hash(x);
        Console.WriteLine($"Hash value of {x} is: {result}");


    }
}
