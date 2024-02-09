using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class HelperFunctions
{
    public static T GetRandomItem<T>(this List<T> items, Func<T, int> weight, System.Random rand)
    {
        int totalWeight = items.Select(x => weight(x)).Sum(x => x);

        int selected = rand.Next(0, totalWeight);

        int current = 0;
        foreach (var item in items)
        {
            current += weight(item);
            if (selected <= current)
            {
                return item;
            }
        }

        return items.LastOrDefault();
    }

    public static T GetRandomItem<T>(this List<T> items, Func<T, int> weight)
    {
        int totalWeight = items.Select(x => weight(x)).Sum(x => x);

        int selected = UnityEngine.Random.Range(0, totalWeight);

        int current = 0;
        foreach (var item in items)
        {
            current += weight(item);
            if (selected <= current)
            {
                return item;
            }
        }

        return items.LastOrDefault();
    }

    public static int GetCountOfItem<T>(this List<T> items, T item)
    {
        int count = 0;
        foreach (var it in items)
        {
            if (item.Equals(it))
            {
                count++;
            }
        }

        return count;
    }

    public static List<int> GetIndicesOfItem<T>(this List<T> items, T item)
    {
        List<int> indices = new List<int>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals(item))
            {
                indices.Add(i);
            }
        }

        return indices;
    }

    public static Dictionary<T, int> GetCounts<T>(this List<T> items)
    {
        Dictionary<T, int> toReturn = new Dictionary<T, int>();

        foreach (var item in items)
        {
            if (!toReturn.ContainsKey(item))
            {
                toReturn.Add(item, 0);
            }
            toReturn[item]++;
        }

        return toReturn;
    }

    /// <summary>
    /// Returns count of item in dictionary
    /// </summary>
    /// <param name="dict"></param>
    /// <param name="item"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static int GetCount<T>(this Dictionary<T, int> dict, T item)
    {
        if (!dict.ContainsKey(item))
        {
            return 0;
        }

        return dict[item];
    }

    /// <summary>
    /// Returns items in the list at the given indices. No range checking
    /// </summary>
    /// <param name="list"></param>
    /// <param name="indices"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetItemsAtIndices<T>(this List<T> list, List<int> indices)
    {
        List<T> toReturn = new List<T>(indices.Count);

        foreach (var index in indices)
        {
            toReturn.Add(list[index]);
        }

        return toReturn;
    }
}
