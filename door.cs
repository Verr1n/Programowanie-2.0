public class Door
{
    public int X { get; set; }
    public int Y { get; set; }

    public Door(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool TryOpen(bool hasKey)
    {
        if (hasKey)
        {
            Console.WriteLine("Otwierasz drzwi kluczem...");
            return true; // przejście dalej
        }
        else
        {
            Console.WriteLine("Potrzebujesz klucza, aby otworzyć drzwi!");
            Console.WriteLine("Wróć, gdy znajdziesz klucz.");
            Console.WriteLine("Naciśnij dowolny klawisz...");
            Console.ReadKey();
            return false;
        }
    }
}
