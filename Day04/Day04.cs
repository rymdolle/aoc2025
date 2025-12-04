long part1 = 0;
long part2 = 0;
char[][] rolls = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt")
    .Select(line => line.ToCharArray())
    .ToArray();

int ReachableRolls(bool remove = false)
{
    int count = 0;
    for (int y = 0; y < rolls.Length; y++)
    {
        for (int x = 0; x < rolls[y].Length; x++)
        {
            if (rolls[y][x] != '@')
                continue;

            int access = AccessCount(x, y);
            if (access >= 4)
                continue;

            count++;
            if (remove)
                rolls[y][x] = 'x';
        }
    }
    return count;
}

int AccessCount(int x, int y)
{
    int count = 0;
    if (x < 0 || y < 0 || y >= rolls.Length || x >= rolls[y].Length)
        return count;
    for (int i = 0; i < 9; i++)
    {
        int nx = x + (i % 3) - 1;
        int ny = y + (i / 3) - 1;
        if (nx == x && ny == y)
            continue;
        if (nx < 0 || ny < 0 || ny >= rolls.Length || nx >= rolls[ny].Length)
            continue;
        if (rolls[ny][nx] == '@')
            count++;
    }
    return count;
}

part1 = ReachableRolls();

int removed;
do {
    removed = ReachableRolls(remove: true);
    part2 += removed;
} while (removed > 0);

Console.WriteLine(part1);
Console.WriteLine(part2);
