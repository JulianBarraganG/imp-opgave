// Creates a stream of random numbers with 
// a given length andnumber of bits.


public static class Stream
{
    // This function creates a stream of random numbers.
    // The stream is generated by creating a random number
    // and then shifting it to the left by 30 bits.
    // The function returns a tuple containing the random number
    // and an integer indicating the sign of the number.
    //
    // n: The length of the stream.
    // l: The number of bits in the random number.
    //
    // Returns: An IEnumerable of tuples containing the random number
    // and an integer indicating the sign of the number.
    public static IEnumerable<Tuple<ulong, int>> CreateStream(int n, int l)
    {
        // We generate a random uint64 number.
        Random rnd = new System.Random();
        ulong a = 0UL;
        Byte[] b = new Byte[8];
        rnd.NextBytes(b);
        for (int i = 0; i < 8; ++i)
        {
            a = (a << 8) + (ulong)b[i];
        }
        // We demand that our random number has 30 zeros on 
        // the least significant bits and then a one.
        a = (a | ((1UL << 31) - 1UL)) ^ ((1UL << 30) - 1UL);
        ulong x = 0UL;
        for (int i = 0; i < n / 3; ++i)
        {
            x = x + a;
            yield return Tuple.Create(x & (((1UL << l) - 1UL) << 30), 1);
        }
        for (int i = 0; i < (n + 1) / 3; ++i)
        {
            x = x + a;
            yield return Tuple.Create(x & (((1UL << l) - 1UL) << 30), -1);
        }
        for (int i = 0; i < (n + 2) / 3; ++i)
        {
            x = x + a;
            yield return Tuple.Create(x & (((1UL << l) - 1UL) << 30), 1);
        }
    }
}