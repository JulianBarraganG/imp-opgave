public class Opg7 {
	public static void Run() {
		Int32 n = 1 << 3;
		Int32 l = 2;
		UInt16 t = 30;

		Tuple<UInt64, Int32>[] data = Stream.CreateStream(n, l).ToArray();
		FourUniHash hash = new FourUniHash();
		Int32[] C = hash.GetSketch(data, t);
		Int32 estimate = hash.CountSketch(C);	
		Console.WriteLine($"Estimate: {estimate}");
	}
}
