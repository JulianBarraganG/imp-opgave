using System.Diagnostics;
public static class Opg1c
{
    public static void Run(int n = 1280000, int l = 32)
    {
        MulModPrimeHash modHash = new MulModPrimeHash();
        MulShiftHash shiftHash = new MulShiftHash();

        Tuple<ulong, int>[] stream = Stream.CreateStream(n, l).ToArray();

        Int64 modSum = 0;
        Int64 shiftSum = 0;

        Stopwatch sw = new Stopwatch();
        sw.Start();

        foreach (var item in stream)
        {
            modSum += modHash.Hash((Int64)item.Item1 * item.Item2);
        }
        sw.Stop();
        Console.WriteLine($"Mod Hash Time: {sw.ElapsedMilliseconds} ms");

        sw.Restart();

        foreach (var item in stream)
        {
            shiftSum += shiftHash.Hash((Int64)item.Item1 * item.Item2);
        }
        sw.Stop();
        Console.WriteLine($"Shift Hash Time: {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"Shift Hash: {shiftSum}");
        Console.WriteLine($"Mod Hash: {modSum}");
    }
}
