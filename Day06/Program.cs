long part1 = 0;
long part2 = 0;
string[] input = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt");

long Calculate(string operand, IEnumerable<long> numbers)
{
    return operand switch
    {
        "+" => numbers.Sum(),
        "*" => numbers.Aggregate((acc, number) => acc * number),
        _ => throw new InvalidOperationException(),
    };
}

long CalculateColumns(string[] lines)
{
    var operands = lines[^1]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries);

    var numbers = lines[..^1]
        .Select(line => line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray())
        .ToArray();

    long total = 0;
    for (int i = 0; i < numbers[0].Length; i++)
    {
        var column = numbers.Select(s => s[i]).ToArray();
        total += Calculate(operands[i], column);
    }
    return total;
}

long CalculateColumnsRightToLeft(string[] lines)
{
    var operands = lines[^1]
        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
        .Reverse();

    var columns = Enumerable.Range(1, lines[0].Length)
        .Select(column =>
        {
            char[] number = lines[..^1]
                .Select(line => line[^column])
                .Where(c => c != ' ')
                .ToArray();
            return number.Length > 0 ? long.Parse(number) : 0;
        }).Aggregate(new List<List<long>> { new() }, (acc, number) =>
        {
            if (number == 0)
                acc.Add([]);
            else
                acc[^1].Add(number);
            return acc;
        });

    long total = 0;
    foreach (var (operand, column) in operands.Zip(columns))
        total += Calculate(operand, column);

    return total;
}

part1 += CalculateColumns(input);
part2 += CalculateColumnsRightToLeft(input);

Console.WriteLine(part1);
Console.WriteLine(part2);
