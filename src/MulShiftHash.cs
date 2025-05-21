public class MulShiftHash
// This class implements a hash function that combines multiplication and bit shifting.
{
    private readonly Int64 a;
    private readonly UInt16 l; 


    private readonly Random rnd;

    
    public MulShiftHash()
    {
        this.rnd = new Random();
        // Set the values of a and l
        this.a = (rnd.NextInt64() << 1) - 1;
        this.l = (UInt16)rnd.Next(1, 64);
    }

    public Int64 Hash(Int64 x)
    {   
        // Perform a multiplication followed by a right shift
        return (a * x) >> (64 - l);
    }
}