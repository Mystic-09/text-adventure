using System.Collections.Generic;

class Inventory
{
    private int maxWeight;
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        items = new Dictionary<string, Item>();
    }

    public bool Put(string itemName, Item item)
    {
        if (item.Weight > FreeWeight())
        {
            return false;
        }

        items[itemName] = item;
        return true;
    }

    public Item Get(string itemName)
    {
        if (!items.ContainsKey(itemName))
        {
            return null;
        }

        Item item = items[itemName];
        items.Remove(itemName);
        return item;
    }

    public int TotalWeight()
    {
        int total = 0;
        foreach (Item item in items.Values)
        {
            total += item.Weight;
        }
        return total;
    }

    public int FreeWeight()
    {
        return maxWeight - TotalWeight();
    }

    public string Show()
    {
        if (items.Count == 0)
        {
            return "none";
        }

        string result = "";
        foreach (Item item in items.Values)
        {
            result += item.Description + " ";
        }
        return result.Trim();
    }
}
