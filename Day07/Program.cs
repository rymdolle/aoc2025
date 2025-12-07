long part1 = 0;
long part2 = 0;
string[] grid = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt");
HashSet<Vector2> visited = [];
Dictionary<Vector2, long> memo = [];

long TransportManifold(Vector2 start, bool quantum)
{
    if (start.Y >= grid.Length || start.X < 0 || start.X >= grid[start.Y].Length)
        return quantum ? 1 : 0;
    if (!quantum && !visited.Add(start))
        return 0;
    if (grid[start.Y][start.X] != '^')
        return TransportManifold(start + new Vector2(0, 1), quantum);
    if (quantum && memo.TryGetValue(start, out var manifold))
        return manifold;
    var path = TransportManifold(start+new Vector2(-1, 1), quantum) +
               TransportManifold(start+new Vector2(1, 1), quantum);
    memo[start] = path;
    return quantum ? path : path + 1;
}

Vector2 start = new Vector2(grid[0].IndexOf('S'), 0);
part2 += TransportManifold(start, quantum: true);
part1 += TransportManifold(start, quantum: false);

Console.WriteLine(part1);
Console.WriteLine(part2);

record Vector2(int X, int Y)
{
    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
}
