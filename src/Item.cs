class Item
{
    // Fields
    public int Weight { get; set; }
    public string Description { get; }

    // Constructor
    public Item(int weight, string description)
    {
        Weight = weight;
        Description = description;
    }
}