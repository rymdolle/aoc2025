int dial = 50;
int part1 = 0;
int part2 = 0;
File.ReadAllLines(args.Length > 0 ? args[0] : "input.txt").ToList().ForEach(line =>
{
    int turns = int.Parse(line[1..]);
    int direction = line[0] == 'R' ? 1 : -1;

    for (int i = 0; i < turns; i++)
    {
        dial += direction;
        if (dial < 0)
            dial += 100;
        if (dial >= 100)
            dial -= 100;
        if (dial == 0)
            part2++;
    }

    if (dial == 0)
        part1++;
});

Console.WriteLine(part1);
Console.WriteLine(part2);
