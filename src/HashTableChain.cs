
public class HashTable
{
    public Func<Int64, UInt64> h;
    public int l;
    public List<(Int64, Int64)>[] table;

    public HashTable(Func<Int64, UInt64> h, int l)
    {
        this.h = h;
        this.l = l;
        
        UInt64 size = (UInt64) 1 << l; // 2^l
        this.table = new List<(Int64, Int64)>[size];
        

    }

    public Int64 get(Int64 x)
    {
        UInt64 index = h(x);
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
        
        UInt64 index = h(x);
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
        Int64 v = get(x);
        set(x, v + d);
    }


}