using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static string[] level1 = {
        "###########",
        "#     K   #",
        "#   #     #",
        "#   #   D #",
        "###########"
    };

    static string[] level2 = {
        "###########",
        "#         #",
        "#   N     #",
        "#     D   #",
        "###########"
    };

    static string[] currentLevel;
    static int playerX = 1;
    static int playerY = 1;
    static Inventory playerInventory = new Inventory();
    static Item key;
    static NPC npc;

    static void Main()
    {
        Console.CursorVisible = false;
        LoadLevel(level1);

        while (true)
        {
            DrawLevel();
            HandleInput();
        }
    }

    static void LoadLevel(string[] level)
    {
        currentLevel = level;

        if (level == level1)
        {
            playerX = 1;
            playerY = 1;
            key = new Item("klucz", level[0].Length, level.Length, currentLevel);
            key.X = 6;
            key.Y = 1;
        }
        else if (level == level2)
        {
            playerX = 1;
            playerY = 1;
            npc = new NPC(5, 2);
        }
    }

    static void DrawLevel()
    {
        Console.Clear();
        for (int y = 0; y < currentLevel.Length; y++)
        {
            for (int x = 0; x < currentLevel[y].Length; x++)
            {
                if (x == playerX && y == playerY)
                    Console.Write('P');
                else if (key != null && x == key.X && y == key.Y)
                    Console.Write('K');
                else if (npc != null && x == npc.X && y == npc.Y)
                    Console.Write('N');
                else
                    Console.Write(currentLevel[y][x]);
            }
            Console.WriteLine();
        }
    }

    static void HandleInput()
    {
        ConsoleKey keyInput = Console.ReadKey(true).Key;
        int newX = playerX;
        int newY = playerY;

        switch (keyInput)
        {
            case ConsoleKey.UpArrow: newY--; break;
            case ConsoleKey.DownArrow: newY++; break;
            case ConsoleKey.LeftArrow: newX--; break;
            case ConsoleKey.RightArrow: newX++; break;
        }

        if (currentLevel[newY][newX] != '#')
        {
            playerX = newX;
            playerY = newY;
        }

        if (key != null && playerX == key.X && playerY == key.Y)
        {
            playerInventory.AddItem(key);
            key = null;
        }

        if (currentLevel[playerY][playerX] == 'D')
        {
            if (currentLevel == level1)
            {
                if (playerInventory.HasItem("klucz"))
                {
                    Console.WriteLine("Otwierasz drzwi i przechodzisz dalej...");
                    playerInventory.RemoveItem("klucz");
                    Console.ReadKey();
                    LoadLevel(level2);
                }
                else
                {
                    Console.WriteLine("Drzwi są zamknięte. Potrzebujesz klucza!");
                    Console.ReadKey();
                }
            }
            else if (currentLevel == level2)
            {
                if (playerX == npc.X && playerY == npc.Y)
                {
                    if (NPC.Interact())
                    {
                        Console.WriteLine("Wygrałeś! Przechodzisz dalej...");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("Gratulacje! Wygrałeś grę!");
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("Przegrałeś. Spróbuj jeszcze raz.");
                        Console.ReadKey();
                        LoadLevel(level1);
                    }
                }
                else
                {
                    Console.WriteLine("Te drzwi się nie otwierają, zanim nie pokonasz NPC!");
                    Console.ReadKey();
                }
            }
        }

        if (npc != null)
        {
            npc.Move(currentLevel);
        }
    }
}

public class Inventory
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"odniesiono przedmiot: {item.Name}");
    }

    public bool HasItem(string itemName)
    {
        foreach (var item in items)
        {
            if (item.Name.ToLower() == itemName.ToLower())
                return true;
        }
        return false;
    }

    public void RemoveItem(string itemName)
    {
        items.RemoveAll(item => item.Name.ToLower() == itemName.ToLower());
    }
}

public class Item
{
    public string Name { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }
    private static Random rand = new Random();

    public Item(string name, int maxWidth, int maxHeight, string[] level)
    {
        Name = name;
        RandomizePosition(maxWidth, maxHeight, level);
    }

    public void RandomizePosition(int maxWidth, int maxHeight, string[] level)
    {
        do
        {
            X = rand.Next(1, maxWidth - 1);
            Y = rand.Next(1, maxHeight - 1);
        }
        while (level[Y][X] == '#' || level[Y][X] == 'D');
    }
}

public class NPC
{
    public int X { get; private set; }
    public int Y { get; private set; }
    private Random rand = new Random();

    public NPC(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(string[] level)
    {
        int newX = X;
        int newY = Y;
        int dir = rand.Next(4);
        switch (dir)
        {
            case 0: newY--; break;
            case 1: newY++; break;
            case 2: newX--; break;
            case 3: newX++; break;
        }
        if (newY >= 0 && newY < level.Length &&
            newX >= 0 && newX < level[0].Length &&
            level[newY][newX] != '#')
        {
            X = newX;
            Y = newY;
        }
    }

    public static bool Interact()
    {
        Console.Clear();
        Console.WriteLine("NPC: Chcesz się zmierzyć w papier-kamień-nożyce?");
        Console.WriteLine("Masz 3 próby. Wygraj przynajmniej raz, by przejść dalej!");
        Console.WriteLine("Wpisz: papier / kamień / nożyce");

        string[] options = { "papier", "kamień", "nożyce" };
        int trials = 3;
        bool winning = false;
        Random npcRand = new Random();

        while (trials > 0)
        {
            Console.Write("\n🫵 Twój wybór: ");
            string person = Console.ReadLine()?.ToLower()?.Trim() ?? "";

            if (Array.IndexOf(options, person) == -1)
            {
                Console.WriteLine("Niepoprawny wybór! Wpisz papier, kamień albo nożyce.");
                continue;
            }

            string npc = options[npcRand.Next(3)];
            Console.WriteLine($"NPC wybrał: {npc}");

            if (person == npc)
            {
                Console.WriteLine("🔁 Remis!");
                continue;
            }

            if ((person == "papier" && npc == "kamień") ||
                (person == "kamień" && npc == "nożyce") ||
                (person == "nożyce" && npc == "papier"))
            {
                Console.WriteLine("Wygrałeś!");
                winning = true;
                break;
            }
            else
            {
                Console.WriteLine("Przegrałeś tę rundę.");
                trials--;
            }
        }

        if (!winning)
        {
            Console.WriteLine("Przegrałeś wszystkie próby.");
        }

        Console.WriteLine("Naciśnij dowolny klawisz, by kontynuować...");
        Console.ReadKey();
        return winning;
    }
}
