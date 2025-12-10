long part1 = 0;
long part2 = 0;
List<Point> points = File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt")
    .Select(s => s.Split(',').Select(int.Parse).ToArray())
    .Select(s => new Point(s[0], s[1]))
    .ToList();

List<Rectangle> rects = points
    .Select((a, i) => points.Skip(i+1).Select(b => Rectangle.FromPoint(a, b)))
    .SelectMany(s => s)
    .OrderByDescending(s => s.Area)
    .ToList();

List<Rectangle> loop = points
    .Zip([..points.Skip(1), points.First()], Rectangle.FromPoint)
    .ToList();

part1 += rects.First().Area;
part2 += rects.First(s =>
{
    var inner = s.Inner();
    return loop.All(r => !r.Overlap(inner));
}).Area;

Console.WriteLine(part1);
Console.WriteLine(part2);

public record Point(int X, int Y);

public record Rectangle(Range X, Range Y)
{
    public long Area =>
        X.Size() * Y.Size();
    public static Rectangle FromPoint(Point a, Point b) =>
        new(Math.Min(a.X, b.X)..Math.Max(a.X, b.X),
            Math.Min(a.Y, b.Y)..Math.Max(a.Y, b.Y)
        );

    public Rectangle Inner() =>
        new ((X.Start.Value+1)..(X.End.Value-1),
            (Y.Start.Value+1)..(Y.End.Value-1)
        );

    public bool Overlap(Rectangle other) =>
        X.Overlap(other.X) && Y.Overlap(other.Y);
}

public static class RangeExtensions
{
    extension(Range source)
    {
        public long Size() =>
            source.End.Value - source.Start.Value + 1;

        public bool Overlap(Range other) =>
            Math.Max(source.Start.Value, other.Start.Value) <=
            Math.Min(source.End.Value, other.End.Value);
    }
}
