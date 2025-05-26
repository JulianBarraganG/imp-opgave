public class Opg7 {
	public static void Run()
	{
		Int32 n = 1 << 16;
		Int32 l = 15;
		UInt16 t = 12;

		Tuple<ulong, int>[] stream = Stream.CreateStream(n, l).ToArray();
		FourUniHash hash = new FourUniHash();
		Int32[] C = hash.GetSketch(stream, t);
		Int32 estimate = hash.CountSketch(C);
		Console.WriteLine($"Estimate: {estimate}");

		MulShiftHash mulShiftHash = new MulShiftHash((UInt16) l);


		HashTable shifthashTable = Opg3.populateHashTable(mulShiftHash.Hash, stream, l);
		Int64 shiftsum = 0;

		foreach (var list in shifthashTable.table)
		{
			if (list != null)
			{
				foreach (var item in list)
				{
					shiftsum += item.Item2 * item.Item2;
				}
			}
		}
		Console.WriteLine($"True value: {shiftsum}");
	}
}
