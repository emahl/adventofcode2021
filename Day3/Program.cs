Part1();
Part2();

void Part1()
{
    double gammaRate = 0;
    double epsilonRate = 0;
    var input = FetchInput();
    var nrOfRows = input.Length;
    var nrOfBits = input[0].Length;

    for (var c = 0; c < nrOfBits; c++)
    {
        var nrOfOnes = 0;
        for (var r = 0; r < nrOfRows; r++)
        {
            nrOfOnes += input[r][c];
        }

        var rate = 1 << (nrOfBits - 1 - c);
        if (nrOfOnes > (nrOfRows / 2))
            gammaRate += rate;
        else
            epsilonRate += rate;
    }

    Console.WriteLine("Part 1: [gamma: {0}] * [epsilon: {1}] = {2}", gammaRate, epsilonRate, gammaRate * epsilonRate);
}

void Part2()
{
    double oxygen = 0;
    double co2 = 0;
    var input = FetchInput();
    var nrOfBits = input[0].Length;
    var remainingLineOxygen = input.ToArray();
    var remainingLinesCO2 = input.ToArray();

    for (var c = 0; c < nrOfBits; c++)
    {
        if (oxygen == 0)
        {
            var mostCommon = GetMostCommonValue(remainingLineOxygen, c);
            remainingLineOxygen = remainingLineOxygen.Where(x => x[c] == mostCommon).ToArray();
            if (remainingLineOxygen.Length == 1)
            {
                oxygen = CalculateValue(remainingLineOxygen[0]);
            }
        }
        
        if (co2 == 0)
        {
            var leastCommon = GetMostCommonValue(remainingLinesCO2, c) == 1 ? 0 : 1;
            remainingLinesCO2 = remainingLinesCO2.Where(x => x[c] == leastCommon).ToArray();
            if (remainingLinesCO2.Length == 1)
            {
                co2 = CalculateValue(remainingLinesCO2[0]);
            }
        }
        
        if (oxygen != 0 && co2 != 0)
        {
            break;
        }
    }

    Console.WriteLine("Part 2: [oxygen: {0}] * [co2: {1}] = {2}", oxygen, co2, oxygen * co2);
}

int GetMostCommonValue(int[][] numbers, int c)
{
    var nrOfRows = numbers.Length;
    var nrOfOnes = 0;
    for (var r = 0; r < nrOfRows; r++)
    {
        nrOfOnes += numbers[r][c];
    }

    return nrOfOnes >= (nrOfRows / 2) ? 1 : 0;
}

int CalculateValue(int[] bits)
{
    var value = 0;
    for (var i = 0; i < bits.Length; i++)
    {
        value += bits[i] << (bits.Length - 1 - i);
    }
    return value;
}

int[][] FetchInput() => File.ReadAllLines("input.txt").Select(s => s.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();