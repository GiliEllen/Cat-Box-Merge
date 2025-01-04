using System;
using System.Collections.Generic;

public static class ListExtensions
{
    private static Random random = new Random();
    public static T GetRandomItem<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new InvalidOperationException("cannot select a random item from an empty or null list.");
        }

        int index = random.Next(list.Count);
        return list[index];
    }
}
