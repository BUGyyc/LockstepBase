// using FixMath.NET;
using System;


/// <summary>
/// TODO: 待完善
/// </summary>
public class RandomUtil
{
    public static int Seed = 10000;
    public const int Base = 2147483647;
    public const int Offset = 13;
    public static int Random()
    {
        //Fix64 result = default(Fix64);
        int result = (Base - Seed) % Seed + Offset;
        Seed = result;
        return result;
    }

    public static bool RandomCompare(int compare)
    {
        Seed = Math.Abs((48271 * Seed + Offset) % 2147483647);

        var min = compare - 100;
        var max = compare + 100;
        Seed = min + (int)(Seed % (max - min + 1));
        var mid = (max + min) / 2;

        return Seed > mid;
    }
}

