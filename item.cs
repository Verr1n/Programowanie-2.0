using System;

public class Item
{
    public string Name { get; private set; } = "Klucz";
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool Collected { get; private set; } = false;

    private Random rand = new Random();

    public Item(string name)
    {
        Name = name;
    }

    public void PlaceRandomly(string[] level)
    {
        while (true)
        {
            int x = rand.Next(1, level[0].Length - 1);
            int y = rand.Next(1, level.Length - 1);

            if (level[y][x] == ' ')
            {
                X = x;
                Y = y;
                break;
            }
        }
    }

    public bool TryPickup(int playerX, int playerY)
    {
        if (!Collected && playerX == X && playerY == Y)
        {
            Collected = true;
            Console.WriteLine($"Znalazłeś przedmiot: {Name}!");
            return true;
        }
        return false;
    }

    public bool UseOnDoor()
    {
        if (Collected)
        {
            Console.WriteLine($"Używasz {Name} do otwarcia drzwi.");
            return true;
        }

        Console.WriteLine("Nie masz odpowiedniego przedmiotu.");
        return false;
    }
}
