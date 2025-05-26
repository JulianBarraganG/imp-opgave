using System.Diagnostics;
using System.Numerics;
public static class Opg1c
{
    public static void Run(int n = 1280000, UInt16 l = 32)
    {
        
        MulModPrimeHash modHash = new MulModPrimeHash(l);
        MulShiftHash shiftHash = new MulShiftHash(l);

        Tuple<ulong, int>[] stream = Stream.CreateStream(n, l).ToArray();

        UInt64 modSum = 0;
        UInt64 shiftSum = 0;

        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (var item in stream)
        {
            modSum += modHash.Hash((UInt64)item.Item1);
        }
        sw.Stop();
        Console.WriteLine($"Mod Hash Time: {sw.ElapsedMilliseconds} ms");

        sw.Restart();

        foreach (var item in stream)
        {
            shiftSum += shiftHash.Hash((UInt64)item.Item1);
        }
        sw.Stop();
        Console.WriteLine($"Shift Hash Time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"Shift Hash: {shiftSum}");
        Console.WriteLine($"Mod Hash: {modSum}");
    }
}
