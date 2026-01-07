using System;

class Player
{
    // Fields
    private int health;

    // Auto property voor currentRoom
    public Room CurrentRoom { get; set; }

    // Constructor
    public Player()
    {
        health = 100;
        CurrentRoom = null;
    }

    // Speler verliest health
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
}
