long part1 = 0;
long part2 = 0;

var cords = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt")
    .Select(s => s.Split(":"))
    .ToDictionary(s => s[0], s => s[1].Split(" ", StringSplitOptions.RemoveEmptyEntries));

Queue<(string, List<string>)> q = new(cords["you"].Select(s => (s, new List<string>())));
while (q.Count > 0)
{
    var (key, path) = q.Dequeue();
    if (key == "out")
    {
        part1++;
        continue;
    }

    foreach (var next in cords[key])
        q.Enqueue((next, [..path, next]));
}

Console.WriteLine(part1);
Console.WriteLine(part2);
