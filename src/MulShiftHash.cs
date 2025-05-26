public class MulShiftHash
// This class implements a hash function that combines multiplication and bit shifting.
{
    private readonly UInt64 a;
    private readonly UInt16 l; 


    private readonly Random rnd;

    
    public MulShiftHash(UInt16 l )
    {
        this.rnd = new Random();
        // Set the values of a and l
        this.a = (UInt64) (rnd.NextInt64() << 1) - 1;
        this.l = l; 
    }

    public UInt64 Hash(UInt64 x)
    {
        // Perform a multiplication followed by a right shift
        UInt64 leftside = (a * x);
        int rightside = 64 - l;
        UInt64 rightshifted = leftside >> rightside; 

        return rightshifted;
    }
}
