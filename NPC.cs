using System;

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

    // NPC porusza się losowo (góra/dół/lewo/prawo)
    public void Move(string[] level)
    {
        int newX = X;
        int newY = Y;

        int dir = rand.Next(4); // 0=góra, 1=dół, 2=lewo, 3=prawo
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

    // Interakcja z graczem
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
                Console.WriteLine("❌ Niepoprawny wybór! Wpisz papier, kamień albo nożyce.");
                continue;
            }

            string npc = options[npcRand.Next(3)];
            Console.WriteLine($"NPC wybrał: {npc}");

            if (person == npc)
            {
                Console.WriteLine("Remis!");
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
