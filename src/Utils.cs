using System.Numerics;
using System.Security.Cryptography;

public class Utils() {
	private readonly BigInteger p = (BigInteger) Math.Pow(2, 89) - 1;

	public BigInteger Gen89BitRnd() {
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
}
