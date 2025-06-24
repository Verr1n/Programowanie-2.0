using System;

public class NPC
{
    public int X { get; private set; }
    public int Y { get; private set; }

    private static Random rand = new Random();

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

        // Sprawdzenie czy nowe pole nie jest ścianą
        if (level[newY][newX] != '#')
        {
            X = newX;
            Y = newY;
        }
    }

    // Interakcja: gra w papier-kamień-nożyce
    public static bool Interact()
    {
        Console.Clear();
        Console.WriteLine("🧍 NPC: Chcesz się zmierzyć w papier-kamień-nożyce?");
        Console.WriteLine("Masz 3 próby. Wygraj przynajmniej raz, by przejść dalej!");
        Console.WriteLine("Wpisz: papier / kamień / nożyce");

        string[] options = { "papier", "kamień", "nożyce" };
        int trials = 3;
        bool won = false;

        while (trials > 0)
        {
            Console.Write("\n🫵 Twój wybór: ");
            string player = Console.ReadLine()?.ToLower().Trim() ?? "";

            if (Array.IndexOf(options, player) == -1)
            {
                Console.WriteLine("Niepoprawny wybór! Wpisz: papier / kamień / nożyce.");
                continue;
            }

            string npc = options[rand.Next(3)];
            Console.WriteLine($"NPC wybrał: {npc}");

            if (player == npc)
            {
                Console.WriteLine("Remis!");
                continue;
            }

            if ((player == "papier" && npc == "kamień") ||
                (player == "kamień" && npc == "nożyce") ||
                (player == "nożyce" && npc == "papier"))
            {
                Console.WriteLine("Wygrałeś pojedynek!");
                won = true;
                break;
            }
            else
            {
                Console.WriteLine("Przegrałeś tę rundę.");
                trials--;
            }
        }

        if (!won)
        {
            Console.WriteLine("Przegrałeś wszystkie próby. Spróbuj ponownie później.");
        }

        Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();

        return won;
    }
}
