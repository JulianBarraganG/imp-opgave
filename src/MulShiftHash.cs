public class MulShiftHash
// This class implements a hash function that combines multiplication and bit shifting.
{
    private readonly Int64 a;
    private readonly UInt16 l; 


    private readonly Random rnd;

    
    public MulShiftHash(UInt16 l )
    {
        this.rnd = new Random();
        // Set the values of a and l
        this.a = (rnd.NextInt64() << 1) - 1;
        this.l = l; 
    }

    public UInt64 Hash(Int64 x)
    {
        // Perform a multiplication followed by a right shift
        UInt64 leftside = (UInt64) (a * x);
        int rightside = 64 - l;
        UInt64 rightshifted = leftside >> rightside; 

        return (UInt64)rightshifted;
    }
}