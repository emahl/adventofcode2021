Part1();
Part2();

void Part1()
{
    var positions = FetchCrabPositions();
    var fuel = FindCheapestFuelConsumption(positions, true);
    Console.WriteLine("Part 1: {0}", fuel);
}

void Part2()
{
    var positions = FetchCrabPositions();
    var fuel = FindCheapestFuelConsumption(positions, false);
    Console.WriteLine("Part 2: {0}", fuel);
}

int FindCheapestFuelConsumption(List<int> positions, bool isFuelConsumptionRateConstant)
{
    var minPos = positions.Min();
    var maxPos = positions.Max();
    var bestFuel = int.MaxValue;

    for (var testPos = minPos; testPos <= maxPos; testPos++)
    {
        var fuel = 0;
        foreach(var position in positions)
        {
            var diff = Math.Abs(position - testPos);
            if (diff == 0)
            {
                // No fuel cost
                continue;
            }

            fuel += isFuelConsumptionRateConstant ? diff : Enumerable.Range(1, diff).Aggregate((p, item) => p + item);
        }

        if (fuel < bestFuel) {
            bestFuel = fuel;
        }
    }

    return bestFuel;
}

string[] FetchInput() => File.ReadAllLines("input.txt");
List<int> FetchCrabPositions() => FetchInput()[0].Split(",").Select(x => int.Parse(x)).ToList();