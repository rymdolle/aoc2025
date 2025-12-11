long part1 = 0;
long part2 = 0;
var machines = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt")
    .Select(line => line.Split(' '))
    .Select(parts =>
    {
        var state = parts[0][1..^1]
            .Reverse()
            .Aggregate(0, (acc, b) => (acc << 1) | (b == '#' ? 1 : 0));
        var buttons = parts[1..^1]
            .Select(s => s[1..^1]
                .Split(',')
                .Select(int.Parse)
                .Aggregate(0, (acc, b) => acc | (1 << b)))
            .ToArray();
        var joltage = parts[^1][1..^1]
            .Split(',')
            .Select(int.Parse)
            .ToList();
        return (state, buttons, joltage);
    }).ToList();

foreach (var machine in machines)
{
    int min = machine.buttons.Length;
    for (int i = 0; i < (1 << machine.buttons.Length); i++)
    {
        int state = machine.state;
        int count = 0;
        for (int j = 0; j < machine.buttons.Length; j++)
        {
            if ((i & (1 << j)) > 0)
            {
                state ^= machine.buttons[j];
                count++;
            }
        }

        if (state == 0)
            min = Math.Min(min, count);
    }

    part1 += min;
}

Console.WriteLine(part1);
Console.WriteLine(part2);
