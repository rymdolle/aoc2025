long part1 = 0;
long part2 = 0;

var cords = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt")
    .Select(s => s.Split(":"))
    .ToDictionary(s => s[0], s => s[1].Split(" ", StringSplitOptions.RemoveEmptyEntries));

Queue<string> q = new(cords["you"]);
while (q.Count > 0)
{
    var key = q.Dequeue();
    if (key == "out")
    {
        part1++;
        continue;
    }

    foreach (var next in cords[key])
        q.Enqueue(next);
}

Dictionary<(string, bool, bool), long> memo = new();
long FindNodeViaDacFft(string start, bool dac = false, bool fft = false)
{
    if (start == "out")
        return dac && fft ? 1 : 0;
    if (memo.TryGetValue((start, dac, fft), out var value))
        return value;
    return memo[(start, dac, fft)] = cords[start]
        .Sum(s => FindNodeViaDacFft(s, dac || s == "dac", fft || s == "fft"));
}

part2 += FindNodeViaDacFft("svr");

Console.WriteLine(part1);
Console.WriteLine(part2);
