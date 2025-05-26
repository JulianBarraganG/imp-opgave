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
	private readonly Int16 q = 89; // Marsenne exponent, 89 for imp
	private readonly BigInteger p;
	private UInt64 m; 

	public FourUniHash() {
		this.p = (BigInteger.One << this.q) - 1;
		this.a0 = this.utils.Gen89BitRnd();
		this.a1 = this.utils.Gen89BitRnd();
		this.a2 = this.utils.Gen89BitRnd();
		this.a3 = this.utils.Gen89BitRnd();
		
		// Assign values to pre-allocated array
		this.aArray[0] = this.a0;
		this.aArray[1] = this.a1;
		this.aArray[3] = this.a3;
	}

	// Private methods
	private (UInt64, Int16) GetHashingFuncs(UInt64 x) {
		// Initialization
		BigInteger gx = this.g(x);
		UInt64 hx = (UInt64)(gx & (this.m - 1));
		Int16 shiftVal = (Int16) (this.q - 1); // 88
		Int16 bx = (Int16) (gx >> shiftVal); // bx is either 0 or 1;
		Int16 sx = (Int16)(1 - 2*bx); // -1 or 1
		return (hx, sx);
	}

	private BigInteger g(UInt64 x) {
		// Initialization
		BigInteger y = aArray[3];
		BigInteger xBig = (BigInteger)x;
		for (short i = 2; i >= 0; i--) {
			BigInteger prod = y * xBig;
			y = prod + aArray[i];
			y = (y & x) + (y >> this.q);
		}
		if (y >= this.p) {
			y = y - this.p;
		}
		return y;
	}

	// Public methods
	public Int32[] GetSketch(Tuple<UInt64, Int32>[] data, UInt16 t) {
		// Assert t in [0, 64]
		if (t < 0 || t > 64) {
			throw new ArgumentOutOfRangeException(nameof(t), "t must be in the range [0, 64]");
		}

		this.m = 1UL << t; // m = 2^t

		// Initialize array
		Int32[] C = new Int32[this.m];
		foreach (var item in data) {
			UInt64 x = item.Item1;
			Int32 d = item.Item2;
			// Get h(x) and s(x)
			(UInt64 hx, Int16 sx) = this.GetHashingFuncs(x);
			C[hx] += sx*d;
		}
		return C;
	}

	public Int32 CountSketch(Int32[] sketch) {
		Int32 sum = 0;
		for (UInt64 i = 0; i < this.m; i++) {
			sum += sketch[i]*sketch[i];
		}
		return sum;
	}
}

