long part1 = 0;
long part2 = 0;
File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt").ToList().ForEach(line =>
{
    line.Split(',').ToList().ForEach(range => {
        var parts = range.Split('-');
        long start = long.Parse(parts[0]);
        long end = long.Parse(parts[1]);
        for (long i = start; i <= end; i++)
        {
            string id = i.ToString();

            // part 1
            if (id.Length % 2 == 0)
            {
                int j = id.Length / 2;
                string a = id[..j];
                string b = id[j..];
                if (a == b)
                    part1 += i;
            }

            // part 2
            for (int j = 1; j < id.Length; j++)
            {
                string a = id[..j];
                string b = id[j..];
                if (FindRepeat(a, b))
                {
                    part2 += i;
                    break;
                }
            }
        }
        return;
    });
});

bool FindRepeat(string a, string b)
{
    if (a.Length == 0)
        return b.Length == 0;
    if (b.Length % a.Length != 0)
        return false;
    for (int i = 0; i < b.Length; i += a.Length)
    {
        if (a != b.Substring(i, a.Length))
            return false;
    }
    return true;
}

Console.WriteLine(part1);
Console.WriteLine(part2);
