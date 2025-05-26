using System.Numerics;
using System.Security.Cryptography;

public class MulModPrimeHash
// This class implements a hash function that combines multiplication and bit shifting.
{

    private readonly BigInteger p;
    private readonly BigInteger a;
    private readonly BigInteger b;
    private readonly UInt16 l;
    private readonly Utils utils;

    public MulModPrimeHash(UInt16 l )
    {
	this.utils = new Utils();
        this.p = (BigInteger) Math.Pow(2, 89) - 1;
        this.a = utils.Gen89BitRnd();
        this.b = utils.Gen89BitRnd();
        this.l = l;
    }


    public UInt64 Hash(UInt64 x)
    {
        // Perform a multiplication followed by a right shift
        BigInteger bigX = new BigInteger(x);
        BigInteger hashValue = (a * bigX + b) % p;
        UInt64 modValue = (UInt64) 1 << l; 

        return (UInt64)(hashValue % modValue);
    }
}
