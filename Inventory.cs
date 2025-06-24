public class Inventory
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"Zebrałeś przedmiot: {item.Name}");
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