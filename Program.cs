string[] level = {
    "###########",
    "#      #  #",
    "#   #     #",
    "#   #     #",
    "###########"
};

NPC npc = new NPC(5, 3);

int playerX = 1;
int playerY = 1;

Console.CursorVisible = false;

while (true)
{
    Console.Clear();
    DrawLevel();

    ConsoleKey key = Console.ReadKey(true).Key;

    int newX = playerX;
    int newY = playerY;

    switch (key)
    {
        case ConsoleKey.W: newY--; break;
        case ConsoleKey.S: newY++; break;
        case ConsoleKey.A: newX--; break;
        case ConsoleKey.D: newX++; break;
        case ConsoleKey.Escape: return;
    }

    if (level[newY][newX] != '#')   // kod na kolizje
    {
        playerX = newX;
        playerY = newY;
    }


    npc.Move(level);

    if (playerX == npc.X && playerY == npc.Y)
    {
        bool passed = NPC.Interact();
        if (passed)
        {
            Console.WriteLine("Good Job");
            Console.ReadKey();
            return;
        }
        else
        {
            Console.WriteLine("L");
            Console.ReadKey();
        }
    }
}
void DrawLevel()
{
    for (int y = 0; y < level.Length; y++)
    {
        for (int x = 0; x < level[y].Length; x++)
        {
            if (x == playerX && y == playerY && x == npc.X && y == npc.Y)
                Console.Write('&'); // specjalny znak, np. że są na tym samym polu
            else if (x == playerX && y == playerY)
                Console.Write('@');
            else if (x == npc.X && y == npc.Y)
                Console.Write('N');
            else
                Console.Write(level[y][x]);

        }
        Console.WriteLine();
    }

    Console.WriteLine("\nSterowanie: W A S D | ESC = Wyjście");
    Console.WriteLine("& - oznacza gracza oraz NPC na tym samym polu");
}