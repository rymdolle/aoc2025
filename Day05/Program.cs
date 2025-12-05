long part1 = 0;
long part2 = 0;
List<Range> ranges = [];
File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt").ToList().ForEach(line =>
{
    if (line.Length == 0)
        return;
    var parts = line.Split('-');
    if (parts.Length == 2)
    {
        var start = long.Parse(parts[0]);
        var end = long.Parse(parts[1]);
        ranges.Add(new Range(start, end));
        return;
    }
    var id = long.Parse(line);
    if (ranges.Any(r => id >= r.Start && id <= r.End))
        part1++;
});

ranges.Sort((a, b) => a.Start.CompareTo(b.Start));
int i = 1;
while (i < ranges.Count)
{
    var prev = ranges[i - 1];
    var curr = ranges[i];
    if (curr.End < prev.End)
    {
        ranges.RemoveAt(i); // fully contained
    }
    else if (curr.Start <= prev.End)
    {
        ranges[i-1] = new Range(prev.Start, curr.End); // merge
        ranges.RemoveAt(i);
    }
    else
    {
        i++;
    }
}

part2 += ranges.Sum(r => r.End - r.Start + 1);

Console.WriteLine(part1);
Console.WriteLine(part2);

record Range(long Start, long End);
