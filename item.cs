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