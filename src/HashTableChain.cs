
public class HashTable
{
    private readonly Func<Int64, Int64> h;
    private readonly int l;
    private readonly List<(Int64, Int64)>[] table;

    public HashTable(Func<Int64, Int64> h, int l)
    {
        this.h = h;
        this.l = l;
        Int64 size = 1 << l; // 2^l
        this.table = new List<(Int64, Int64)>[size];

    }

    public Int64 get(Int64 x)
    {
        Int64 index = h(x);

        if (table[index] == null)
        {
            return 0;
        }

        else
        {
            foreach (var item in table[index])
            {
                if (item.Item1 == x)
                {
                    return item.Item2;
                }
            }
        }
        return 0;
    }


    public void set(Int64 x, Int64 v)
    {
        Int64 index = h(x);
        if (table[index] == null)
        {
            table[index] = new List<(Int64, Int64)>();
            table[index].Add((x, v));
        }
        else
        {
            for (int i = 0; i < table[index].Count; i++)
            {
                var item = table[index][i];
                {
                    if (item.Item1 == x)
                    {
                        table[index][i] = (x, v);
                        return;
                    }
                }
            }
            table[index].Add((x, v));
        }
    }

    public void increment(Int64 x, Int64 d)
     {
        Int64 index = h(x);
        if (table[index] == null)
        {
            table[index] = new List<(Int64, Int64)>();
            table[index].Add((x, d));
        }
        else
        {
            for (int i = 0; i < table[index].Count; i++)
            {
                var item = table[index][i];
                {
                    if (item.Item1 == x)
                    {
                        table[index][i] = (x, item.Item2 + d);
                        return;
                    }
                }
            }
            table[index].Add((x, d));
        }
    }


}