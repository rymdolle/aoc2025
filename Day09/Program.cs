long part1 = 0;
long part2 = 0;
int[][] points = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt")
    .Select(s => s.Split(',')
        .Select(int.Parse)
        .ToArray())
    .ToArray();

for (int i = 0; i < points.Length; i++)
{
    for (int j = 1; i+j < points.Length; j++)
    {
        var a = points[i];
        var b = points[i+j];
        long dx = a[0] - b[0];
        long dy = a[1] - b[1];
        long area = Math.Abs(dx+1) * Math.Abs(dy+1);
        if (area > part1)
            part1 = area;
    }
}

Console.WriteLine(part1);
Console.WriteLine(part2);
