long part1 = 0;
long part2 = 0;
Dictionary<(int, int), long> memo = new();

File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt").ToList().ForEach(line =>
{
    memo.Clear();
    part1 += FindJoltage(line, 2);
    part2 += FindJoltage(line, 12);
});

long FindJoltage(string bank, int length, int i = 0)
{
    if (length == 0)
        return 0;
    if (i == bank.Length)
        return long.MinValue;
    if (memo.TryGetValue((i, length), out long val))
        return val;
    long best = FindJoltage(bank, length, i+1);
    if (length > 0) {
        int digit = bank[i] - '0';
        long next = FindJoltage(bank, length-1, i+1);
        long pow = (long)Math.Pow(10, length-1);
        best = Math.Max(best, pow * digit + next);
    }
    memo[(i, length)] = best;
    return best;
}

Console.WriteLine(part1);
Console.WriteLine(part2);
