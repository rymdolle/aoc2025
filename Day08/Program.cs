using System.Numerics;

long part1 = 0;
long part2 = 0;
List<Vector3> points = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt")
    .Select(s => s
        .Split(',')
        .Select(int.Parse)
        .ToArray())
    .Select(s => new Vector3(s[0], s[1], s[2]))
    .ToList();

var junctions = new List<(int, int, float)>();

// calculate distances
for (var i = 0; i < points.Count; i++)
    for (int j = 0; j < i; j++)
        junctions.Add((i, j, Vector3.Distance(points[i], points[j])));

junctions = junctions.OrderBy(c => c.Item3).ToList();
var circuits = Enumerable.Range(0, points.Count).ToDictionary(s => s);
int FindConnection(int point)
{
    if (circuits[point] == point)
        return point;
    circuits[point] = FindConnection(circuits[point]);
    return circuits[point];
}

int connections = 0;
// combine connections
foreach (var (junction, i) in junctions.Select((c, i) => (c, i)))
{
    if (i == 1000)
    {
        part1 += Enumerable.Range(0, points.Count)
            .Aggregate(new Dictionary<int, int>(), (acc, c) =>
            {
                int index = FindConnection(c);
                acc[index] = acc.GetValueOrDefault(index, 0) + 1;
                return acc;
            })
            .Values
            .Order()
            .TakeLast(3)
            .Aggregate((acc, c) => acc * c);
    }

    int c1 = FindConnection(junction.Item1);
    int c2 = FindConnection(junction.Item2);
    if (c1 != c2)
    {
        connections++;
        if (connections == points.Count-1)
        {
            part2 += (long)points[junction.Item1].X * (long)points[junction.Item2].X;
            break;
        }

        // connect
        circuits[c1] = c2;
    }
}

Console.WriteLine(part1);
Console.WriteLine(part2);
