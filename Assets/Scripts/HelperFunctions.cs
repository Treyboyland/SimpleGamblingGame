using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class HelperFunctions
{
    public static void Shuffle<T>(this List<T> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            int chosenIndex = UnityEngine.Random.Range(i, items.Count);
            var temp = items[chosenIndex];
            items[chosenIndex] = items[i];
            items[i] = temp;
        }
    }

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

    /// <summary>
    /// Converts a value in pennies to a value in dollars
    /// </summary>
    /// <param name="pennies"></param>
    public static string ToCash(this int pennies)
    {
        return string.Format("{0:C}", pennies / 100.0m);
    }

    /// <summary>
    /// For each item in the list, returns the indices of the longest consecutive portion
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Dictionary<T, List<int>> GetMaxConsecutiveIndices<T>(this List<T> list, List<T> wildItems, bool startLeft)
    {
        Dictionary<T, List<int>> count = new Dictionary<T, List<int>>();
        if (list.Count == 0)
        {
            return count;
        }

        var uniqueitems = list.Distinct().ToList();

        //TODO: This is inefficient, but it reads easier to me...
        foreach (var item in uniqueitems)
        {
            if (wildItems.Contains(item))
            {
                //NOTE: We don't count wilds individually, which may be a problem
                continue;
            }
            int itemCount = 0;
            int max = int.MinValue;
            List<int> indices = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(item) || wildItems.Contains(list[i]))
                {
                    indices.Add(i);
                    itemCount++;
                }
                else
                {
                    if (itemCount > max)
                    {
                        max = itemCount;
                        if (!count.ContainsKey(item))
                        {
                            count.Add(item, indices);
                        }
                        else
                        {
                            count[item] = indices;
                        }
                    }
                    itemCount = 0;
                    indices = new List<int>();
                    if (startLeft)
                    {
                        return count;
                    }
                }
            }
            if (itemCount > max)
            {
                if (!count.ContainsKey(item))
                {
                    count.Add(item, indices);
                }
                else
                {
                    count[item] = indices;
                }
            }
        }


        /*
        T last = default(T);
        List<int> indices = new List<int>();
        for (int i = 0; i < list.Count; i++)
        {
            //If first item, add it
            if (i == 0)
            {
                last = list[i];
                indices.Add(i);
                if (!count.ContainsKey(last))
                {
                    count.Add(last, indices);
                }
            }
            //If item matches last, add to list
            else if (list[i].Equals(last))
            {
                indices.Add(i);
                if (!count.ContainsKey(last))
                {
                    count.Add(last, indices);
                }
                if (count[last].Count < indices.Count)
                {
                    count[last] = indices;
                }
            }
            //If the last item is wild, set last to this non-wild value, and add to list
            else if (wildItems.Contains(last))
            {
                last = list[i];
                indices.Add(i);
                if (!count.ContainsKey(last))
                {
                    count.Add(last, indices);
                }
                else if (count[last].Count < indices.Count)
                {
                    count[last] = indices;
                }
            }
            //If this item is wild, previous is not and should remain last
            else if (wildItems.Contains(list[i]))
            {
                indices.Add(i);
                if (!count.ContainsKey(last))
                {
                    count.Add(last, indices);
                }
                else if (count[last].Count < indices.Count)
                {
                    count[last] = indices;
                }
            }
            else
            {
                indices = new List<int>();
                indices.Add(i);
                last = list[i];
                if (!count.ContainsKey(last))
                {
                    count.Add(last, indices);
                }
            }
        }*/

        return count;
    }
}
