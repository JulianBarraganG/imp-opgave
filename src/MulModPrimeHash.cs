public class MulModPrimeHash
// This class implements a hash function that combines multiplication and bit shifting.
{

    private readonly Random rnd;
    private readonly int p;
    private readonly int a;
    private readonly int b;
    private readonly int l;

    public MulModPrimeHash()
    {
        Console.WriteLine("MulModPrimeHash constructor called");
        rnd = new Random();
        this.p = Convert.ToInt32(Math.Pow(2, 13)) - 1;
        this.a = rnd.Next(p);
        this.b = rnd.Next(p);
        this.l = rnd.Next(1, 64);
        
    }    
    

    public int Hash(int x)
    {
        Console.WriteLine($"a: {a}, b: {b}, l: {l}");
        // Perform a multiplication followed by a right shift
        return ((a * x + b) % p) % (Convert.ToInt32(Math.Pow(2, l)));
    }
}