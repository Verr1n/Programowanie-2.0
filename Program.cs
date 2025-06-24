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



