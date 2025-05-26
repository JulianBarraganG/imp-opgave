using System.Numerics;

public class FourUniHash 
{
	// Attributes
	private readonly Utils utils = new Utils();
	private readonly BigInteger a0;
	private readonly BigInteger a1;
	private readonly BigInteger a2;
	private readonly BigInteger a3;
	private readonly BigInteger[] aArray = new BigInteger[4]; // Pre-allocated array
	private readonly BigInteger p = (BigInteger.One << 89) - 1;

	public FourUniHash() {
		this.a0 = this.utils.Gen89BitRnd();
		this.a1 = this.utils.Gen89BitRnd();
		this.a2 = this.utils.Gen89BitRnd();
		this.a3 = this.utils.Gen89BitRnd();
		
		// Assign values to pre-allocated array
		this.aArray[0] = this.a0;
		this.aArray[1] = this.a1;
		this.aArray[2] = this.a2;
		this.aArray[3] = this.a3;
	}

	public BigInteger Hashing(UInt64 x) {
		// Initialization
		BigInteger y = aArray[3];
		for (short i = 2; i >= 0; i--) {
			BigInteger prod = (BigInteger) (y * x);
			y = prod + aArray[i];
			y = (y & x) + (y >> 89);
		}
		if (y >= this.p) {
			y = y - this.p;
		}
		return y;
	}
}
