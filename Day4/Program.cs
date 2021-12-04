const int NrOfTileNumbers = 5;

Part1();
Part2();

void Part1()
{
    var numbersDrawn = GetDrawnNumbers();
    var bingoTiles = GetBingoTiles();

    Console.WriteLine("Part 1: {0}", GetFirstWinningTileScore(bingoTiles, numbersDrawn));
}

void Part2()
{
    var numbersDrawn = GetDrawnNumbers();
    var bingoTiles = GetBingoTiles();

    Console.WriteLine("Part 2: {0}", GetLastWinningTileScore(bingoTiles, numbersDrawn));
}

int GetLastWinningTileScore(List<BingoCell[][]> bingoTiles, int[] numbersDrawn)
{
    var bingoTilesLeftToWin = bingoTiles.ToList();
    foreach (var drawnNumber in numbersDrawn)
    {
        bingoTilesLeftToWin = bingoTilesLeftToWin.Where(t => !HasWon(t)).ToList();
        foreach (var bingoTile in bingoTilesLeftToWin)
        {
            MarkNumber(bingoTile, drawnNumber);
        }
        if (bingoTilesLeftToWin.Count == 1 && HasWon(bingoTilesLeftToWin[0]))
        {
            return CalculateUnmarkedScore(bingoTilesLeftToWin[0]) * drawnNumber;
        }
    }

    return -1;
}

int GetFirstWinningTileScore(List<BingoCell[][]> bingoTiles, int[] numbersDrawn)
{
    foreach (var drawnNumber in numbersDrawn)
    {
        foreach (var bingoTile in bingoTiles)
        {
            MarkNumber(bingoTile, drawnNumber);
            if (HasWon(bingoTile))
            {
                return CalculateUnmarkedScore(bingoTile) * drawnNumber;
            }
        }
    }

    return -1;
}

int CalculateUnmarkedScore(BingoCell[][] bingoTile)
{
    var score = 0;
    Iterate((r, c) => score += bingoTile[r][c].Score);
    return score;
}

bool HasWon(BingoCell[][] bingoTile)
{
    // ROWS
    var nrOfMarked = 0;
    for (var i = 0; i < NrOfTileNumbers; i++)
    {
        nrOfMarked = 0;
        for (var j = 0; j < NrOfTileNumbers; j++)
        {
            if (!bingoTile[i][j].Marked)
            {
                break;
            }
            nrOfMarked++;
            if (nrOfMarked == NrOfTileNumbers)
            {
                return true;
            }
        }
    }

    // COLUMNS
    nrOfMarked = 0;
    for (var i = 0; i < NrOfTileNumbers; i++)
    {
        nrOfMarked = 0;
        for (var j = 0; j < NrOfTileNumbers; j++)
        {
            if (!bingoTile[j][i].Marked)
            {
                break;
            }
            nrOfMarked++;
            if (nrOfMarked == NrOfTileNumbers)
            {
                return true;
            }
        }
    }

    return false;
}

void MarkNumber(BingoCell[][] bingoTile, int number)
{
    Iterate((r, c) =>
    {
        if (bingoTile[r][c].Number == number)
        {
            bingoTile[r][c].SetMarked();
        }
    });
}

void Iterate(Action<int, int> doStuff)
{
    for (var i = 0; i < NrOfTileNumbers; i++)
    {
        for (var j = 0; j < NrOfTileNumbers; j++)
        {
            doStuff(i, j);
        }
    }
}

string[] FetchInput() => File.ReadAllLines("input.txt");
int[] GetDrawnNumbers() => FetchInput()[0].Split(",").Select(x => int.Parse(x)).ToArray();
List<BingoCell[][]> GetBingoTiles()
{
    var tiles = new List<BingoCell[][]>();
    var input = FetchInput().Skip(1).ToArray();
    var counter = 0;
    var bingoTile = new BingoCell[NrOfTileNumbers][];
    for (var i = 1; i <= input.Length; i++)
    {
        if (i%6 == 0)
        { 
            tiles.Add(bingoTile);
            bingoTile = new BingoCell[NrOfTileNumbers][];
            counter = 0;
            continue;
        }

        var line = input[i];
        var numbers = line.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).Select(n => new BingoCell(n)).ToArray();
        bingoTile[counter] = numbers;
        counter++;
    }

    return tiles;
}

public record BingoCell(int Number)
{
    public bool Marked { get; private set; }
    public bool SetMarked() => Marked = true;
    public int Score => Marked ? 0 : Number;
}