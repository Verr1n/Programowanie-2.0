
using System;

namespace Bestiarium
{

 public class NPC
 {
    public int X { get; set; }
    public int Y { get; set; }

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

        if (level[newY][newX] != '#')
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

        while (trials > 0)
        {
            Console.Write("\n🫵 Twój wybór: ");
            string person = Console.ReadLine()?.ToLower() ?? "";

        if (Array.IndexOf(options, person) == -1)
            {
                Console.WriteLine("Nie ma takiego wyboru w podstawowej wersji papier kamień nożyce");
                continue;
            }

            string npc = options[new Random().Next(3)];

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

        return winning;
    }
  }
}
