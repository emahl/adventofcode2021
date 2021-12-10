Part1();
Part2();

void Part1()
{
    var totalScore = 0;
    var input = FetchInput();

    foreach (var line in input)
    {
        var tag = GetFirstIncorrectClosingTag(line);
        if (tag != null)
        {
            totalScore += GetClosedTagErrorScore(tag.Value);
        }
    }

    Console.WriteLine("Part 1: {0}", totalScore);
}

void Part2()
{
    var input = FetchInput();

    var incompleteLines = input.Where(l => !IsLineCorrupted(l)).ToList();
    var completionScores = new long[incompleteLines.Count];
    
    for (var i = 0; i < incompleteLines.Count; i++)
    {
        var remainingOpenTags = new Stack<char>();
        foreach (var tag in incompleteLines[i])
        {
            if (IsValidOpenTag(tag))
            {
                remainingOpenTags.Push(tag);
            }
            else
            {
                remainingOpenTags.Pop();
            }
        }

        foreach (var openTag in remainingOpenTags)
        {
            completionScores[i] *= 5;
            completionScores[i] += GetOpenTagCompletionScore(openTag);
        }
    }

    // From assignment description: There will always be an odd number of scores to consider
    Console.WriteLine("Part 2: {0}", completionScores.OrderBy(x => x).ToArray()[(completionScores.Length - 1) / 2]);
}

bool IsLineCorrupted(string line) => GetFirstIncorrectClosingTag(line) != null;

char? GetFirstIncorrectClosingTag(string line)
{
    var openTags = new Stack<char>();
    foreach (var tag in line)
    {
        if (IsValidOpenTag(tag))
        {
            openTags.Push(tag);
            continue;
        }

        var latestOpenTag = openTags.Pop();
        var openTag = GetOpenTag(tag);
        if (latestOpenTag != openTag)
        {
            return tag;
        }
    }

    return null;
}

bool IsValidOpenTag(char tag) => tag switch
{
    '(' => true,
    '[' => true,
    '{' => true,
    '<' => true,
    _ => false
};

char GetOpenTag(char tag) => tag switch
{
    ')' => '(',
    ']' => '[',
    '}' => '{',
    '>' => '<',
    _ => throw new ArgumentOutOfRangeException($"Unexpected tag {tag}")
};

int GetClosedTagErrorScore(char closedTag) => closedTag switch
{
    ')' => 3,
    ']' => 57,
    '}' => 1197,
    '>' => 25137,
    _ => throw new ArgumentOutOfRangeException($"Unexpected closed tag {closedTag}")
};

int GetOpenTagCompletionScore(char openTag) => openTag switch
{
    '(' => 1,
    '[' => 2,
    '{' => 3,
    '<' => 4,
    _ => throw new ArgumentOutOfRangeException($"Unexpected open tag {openTag}")
};

string[] FetchInput() => File.ReadAllLines("input.txt");