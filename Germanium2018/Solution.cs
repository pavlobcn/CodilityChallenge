using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    public int solution(int[] A, int[] B)
    {
        List<Item>[] items = GetItems(A, B);
        int result = GetMaxNotPresent(items);
        return result;
    }

    private int GetMaxNotPresent(List<Item>[] items)
    {
        for (int i = 1; i < items.Length; i++)
        {
            List<Item> list = items[i];
            if (list == null || list.Count == 0)
            {
                return i;
            }

            if (list.Count == 1)
            {
                Item item = list.Single();
                int min = Math.Min(item.A, item.B);
                int max = Math.Max(item.A, item.B);
                if (min == max)
                {
                    continue;
                }

                if (min == i)
                {
                    list.Remove(item);
                    if (max < items.Length)
                    {
                        items[max].Remove(item);
                    }
                    continue;
                }

                list.Remove(item);
                items[min].Remove(item);
                if (items[min].Count == 0)
                {
                    return i;
                }

                RemoveSingleValues(items, min, i);
            }
        }
        return items.Length;
    }

    private void RemoveSingleValues(List<Item>[] items, int removedIndex, int i)
    {
        if (items[removedIndex].Count > 1 || items[removedIndex].Count == 0)
        {
            return;
        }

        Item item = items[removedIndex].Single();
        int min = Math.Min(item.A, item.B);
        int max = Math.Max(item.A, item.B);
        if (min == max)
        {
            return;
        }

        if (min == removedIndex)
        {
            items[min].Remove(item);
            if (max < items.Length)
            {
                items[max].Remove(item);
            }
            if (max < i)
            {
                RemoveSingleValues(items, max, i);
            }
        }
        else if (max == removedIndex)
        {
            items[max].Remove(item);
            items[min].Remove(item);
            RemoveSingleValues(items, min, i);
        }
    }

    private List<Item>[] GetItems(int[] a, int[] b)
    {
        var items = new List<Item>[a.Length + 1];
        for (int i = 0; i < a.Length; i++)
        {
            int valueA = a[i];
            int valueB = b[i];
            var item = new Item(valueA, valueB);
            foreach (int value in new []{valueA, valueB})
            {
                if (value >= items.Length)
                {
                    continue;
                }

                List<Item> list = items[value];
                if (list == null)
                {
                    list = new List<Item>();
                    items[value] = list;
                }
                list.Add(item);
            }
        }
        return items;
    }
}

public partial class Item
{
    public int A { get; }
    public int B { get; }

    public Item(int a, int b)
    {
        A = a;
        B = b;
    }

}
