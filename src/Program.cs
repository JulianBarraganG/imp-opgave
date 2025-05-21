using System.Security.Cryptography;
using System.Numerics;
using System;

public class Program
{
    static public void Main(string[] args)
    {
        MulModPrimeHash hash = new MulModPrimeHash();
        Int64 x = 1234;
        Int64 hashValue = hash.Hash(x);
        Console.WriteLine($"Hash value for {x} is: {hashValue}");


        hashValue = hash.Hash(x);
        Console.WriteLine($"Hash value for {x} is: {hashValue}");


       

    }
}
