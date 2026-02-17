using System;

class Player
{
    // Fields
    private int health;

    // Current Room van de speler
    public Room CurrentRoom { get; set; }

    private Inventory backpack;

    // Constructor
    public Player()
    {
        health = 100;
        CurrentRoom = null;
        backpack = new Inventory(25);
    }

    // Take item from the room's chest
    public bool TakeFromChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);

        if (item == null)
        {
            Console.WriteLine("Item is not in this room.");
            return false;
        }

        if (!backpack.Put(itemName, item))
        {
            Console.WriteLine("Item doesn't fit in your inventory.");
            CurrentRoom.Chest.Put(itemName, item);
            return false;
        }

        Console.WriteLine("You picked up the " + itemName + ".");
        return true;
    }


    // Drop item to the room's chest
    public bool DropToChest(string itemName)
    {
        Item item = backpack.Get(itemName);

        if (item == null)
        {
            Console.WriteLine("You don't have that item.");
            return false;
        }

        CurrentRoom.Chest.Put(itemName, item);
        Console.WriteLine("You dropped the " + itemName + ".");
        return true;
    }

    // Show inventory contents
    public string ShowInventory()
    {
        return backpack.Show();
    }



    // Damage speler
    public void Damage(int amount)
    {
        health -= amount;
        if (health < 0)
        {
            health = 0;
        }
    }

    // Speler krijgt health terug
    public void Heal(int amount)
    {
        health += amount;
        if (health > 100)
        {
            health = 100;
        }
    }

    // Check of speler nog leeft
    public bool IsAlive()
    {
        return health > 0;
    }

    // Getter voor health
    public int GetHealth()
    {
        return health;
    }

    public string Use(string itemName)
{
    if (string.IsNullOrEmpty(itemName))
    {
        return "Use what?";
    }

    Item item = backpack.HasItem(itemName);

    if (item == null)
    {
        return "You don't have that item.";
    }

    return "You used the " + itemName + ".";
}





}
