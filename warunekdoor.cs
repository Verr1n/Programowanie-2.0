if (playerX == drzwi.X && playerY == drzwi.Y)
{
    bool opened = drzwi.TryOpen(key.Collected);

    if (opened)
    {
        NextLevel();
    }
}
