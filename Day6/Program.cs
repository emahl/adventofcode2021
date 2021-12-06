const int NewFish = 8;

Part1();
Part2();

void Part1()
{
    var nextState = FetchInitialLanternFishState();
    for (var i = 0; i < 80; i++)
    {
        nextState = CalculateNextStateSlow(nextState);
    }
    Console.WriteLine("Part 1: {0}", nextState.Count);
}

void Part2()
{
    var state = GetCompressedStateList();
    for (var i = 0; i < 256; i++)
    {
        CalculateNextStateFast(state);
    }

    Console.WriteLine("Part 2: {0}", state.Sum());
}

List<long> GetCompressedStateList()
{
   return Enumerable.Range(0, 9).Select(i => (long)FetchInitialLanternFishState().Where(x => x == i).Count()).ToList();
}

void CalculateNextStateFast(List<long> state)
{
    var newFish = state[0];
    state.RemoveAt(0);
    state.Add(0);
    state[6] += newFish;
    state[8] += newFish;
}

List<int> CalculateNextStateSlow(List<int> state)
{
    var nextState = new List<int>();

    foreach (var fish in state)
    {
        var nextFish = fish;
        if (nextFish == 0)
        {
            nextFish = 7;
            nextState.Add(NewFish);
        }

        nextState.Add(nextFish - 1);
    }

    return nextState;
}

string[] FetchInput() => File.ReadAllLines("input.txt");
List<int> FetchInitialLanternFishState() => FetchInput()[0].Split(",").Select(x => int.Parse(x)).ToList();