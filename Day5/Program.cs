Part1();
Part2();

void Part1()
{
    var lines = FetchLines().Where(l => l.IsHorizontal || l.IsVertical).ToList();

    var lineCoordinates = lines.SelectMany(l => l.GetAllLineCoordinates()).ToList();
    var coordCount = lineCoordinates.GroupBy(coord => coord).Select(g => (g.Key, NrOfDuplicates: g.Count())).ToList();

    var nrOfMaxCoordCount = coordCount.Where(g => g.NrOfDuplicates >= 2).Count();

    Console.WriteLine("Part 1: {0}", nrOfMaxCoordCount);
}

void Part2()
{
    var lines = FetchLines().ToList();

    var lineCoordinates = lines.SelectMany(l => l.GetAllLineCoordinates()).ToList();
    var coordCount = lineCoordinates.GroupBy(coord => coord).Select(g => (g.Key, NrOfDuplicates: g.Count())).ToList();

    var nrOfMaxCoordCount = coordCount.Where(g => g.NrOfDuplicates >= 2).Count();

    Console.WriteLine("Part 2: {0}", nrOfMaxCoordCount);
}

string[] FetchInput() => File.ReadAllLines("input.txt");
List<Line> FetchLines()
{
    var input = FetchInput();
    return input.Select(x =>
    {
        var split = x.Split(" -> ");
        var c1 = split[0].Split(",").Select(x => int.Parse(x)).ToArray();
        var c2 = split[1].Split(",").Select(x => int.Parse(x)).ToArray();
        return new Line(new Coordinate(c1[0], c1[1]), new Coordinate(c2[0], c2[1]));
    }).ToList();
}

public record Line(Coordinate Start, Coordinate End)
{
    public bool IsHorizontal => Start.X == End.X;
    public bool IsVertical => Start.Y == End.Y;

    int MinX => Math.Min(Start.X, End.X);
    int MaxX => Math.Max(Start.X, End.X);
    int MinY => Math.Min(Start.Y, End.Y);
    int MaxY => Math.Max(Start.Y, End.Y);

    public List<Coordinate> GetAllLineCoordinates()
    {
        if (IsHorizontal || IsVertical)
        {
            var allX = Enumerable.Range(MinX, MaxX - MinX + 1);
            var allY = Enumerable.Range(MinY, MaxY - MinY + 1);

            return allX.SelectMany(x => allY.Select(y => new Coordinate(x, y))).ToList();
        }

        var coordinates = new List<Coordinate>();
        var start = Start.Copy();
        while (start != End)
        {
            coordinates.Add(start.Copy());
            start.MoveTowards(End);
        }
        coordinates.Add(End.Copy());
        return coordinates;
    }
}

public record Coordinate(int X, int Y)
{
    public int X { get; private set;  } = X;
    public int Y { get; private set; } = Y;

    public void MoveTowards(Coordinate goTo)
    {
        X += X < goTo.X ? 1 : -1;
        Y += Y < goTo.Y ? 1 : -1;
    }

    public Coordinate Copy()
    {
        return new Coordinate(X, Y);
    }
}