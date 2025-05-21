using System.Numerics;
using System.Security.Cryptography;

public class MulModPrimeHash
// This class implements a hash function that combines multiplication and bit shifting.
{

    private readonly Random rnd;
    private readonly BigInteger p;
    private readonly BigInteger a;
    private readonly BigInteger b;
    private readonly UInt16 l;

    public MulModPrimeHash(UInt16 l )
    {
        rnd = new Random();
        this.p = (BigInteger) Math.Pow(2, 89) - 1;
        this.a = Gen89BitRnd();
        this.b = Gen89BitRnd();

        this.l = l;
    }
    
    private BigInteger Gen89BitRnd()
    {
        const int bitLength = 89;
        int byteLength = (bitLength + 7) / 8; // 89 bits → 12 bytes (96 bits, but we'll mask later)

        byte[] randomBytes = new byte[byteLength];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        // Mask the highest byte to ensure only 89 bits are used
        int excessBits = (byteLength * 8) - bitLength;
        if (excessBits > 0)
        {
            byte mask = (byte)(0xFF >> excessBits);
            randomBytes[^1] &= mask; // Apply mask to the last byte
        }

        // Convert to BigInteger (unsigned, little-endian)
        BigInteger result = new BigInteger(randomBytes, isUnsigned: true, isBigEndian: false);

        if (result == p)
        {
            return Gen89BitRnd();
        }
        return result;
    }


    public UInt64 Hash(Int64 x)
    {
        // Perform a multiplication followed by a right shift
        BigInteger bigX = new BigInteger(x);
        BigInteger hashValue = (a * bigX + b) % p;
        UInt64 modValue = (UInt64) 1 << l; 

        return (UInt64)(hashValue % modValue);
    }
}