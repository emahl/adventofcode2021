Part1();
Part2();

void Part1()
{
    var heightMap = FetchHeightMap();
    var riskLevel = 0;
    for (var i = 0; i < heightMap.Length; i++)
    {
        for (var j = 0; j < heightMap[0].Length; j++)
        {
            if (IsLowPoint(heightMap, i, j))
            {
                riskLevel += heightMap[i][j] + 1;
            }
        }
    }

    Console.WriteLine("Part 1: {0}", riskLevel);
}

void Part2()
{
    var heightMap = FetchHeightMap();
    var basins = new List<int>();
    for (var i = 0; i < heightMap.Length; i++)
    {
        for (var j = 0; j < heightMap[0].Length; j++)
        {
            if (IsLowPoint(heightMap, i, j))
            {
                basins.Add(CalculateBasinSize(heightMap, i, j, new List<Point>()));
            }
        }
    }

    var top3Basins = basins.OrderByDescending(x => x).Take(3);
    Console.WriteLine("Part 2: {0} = {1}", string.Join(" * ", top3Basins), top3Basins.Aggregate((x, y) => x*y));
}

int CalculateBasinSize(int[][] heightMap, int i, int j, List<Point> pointsToIgnore)
{
    var result = 1;
    pointsToIgnore.Add(new Point(i, j));
    
    void TestValue(int x, int y)
    {
        var test = GetArrayValueSafe(heightMap, x, y);
        if (!pointsToIgnore.Contains(new Point(x, y)) && test < 9)
        {
            result += CalculateBasinSize(heightMap, x, y, pointsToIgnore);
        }
    }

    TestValue(i - 1, j);
    TestValue(i + 1, j);
    TestValue(i, j - 1);
    TestValue(i, j + 1);

    return result;
}

int GetArrayValueSafe(int[][] heightMap, int k, int l)
{
    return (k > -1 && l > -1 && heightMap.Length > k && heightMap[k].Length > l)
        ? heightMap[k][l]
        : int.MaxValue;
}

bool IsLowPoint(int[][] heightMap, int i, int j)
{
    var valueToTest = GetArrayValueSafe(heightMap, i, j);
    return valueToTest < GetArrayValueSafe(heightMap, i - 1, j)
           && valueToTest < GetArrayValueSafe(heightMap, i + 1, j)
           && valueToTest < GetArrayValueSafe(heightMap, i, j - 1)
           && valueToTest < GetArrayValueSafe(heightMap, i, j + 1);
}

string[] FetchInput() => File.ReadAllLines("input.txt");
int[][] FetchHeightMap() {
    var input = FetchInput();
    var heightMap = new int[input.Length][];
    for (var i = 0; i < input.Length; i++)
    {
        heightMap[i] = input[i].ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
    }

    return heightMap;
}

public record Point(int x, int y);