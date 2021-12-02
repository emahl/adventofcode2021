Part1();
Part2();

void Part1() {
    var position = 0;
    var depth = 0;

    void PerformCommand(string cmdStr) {
        var cmdSplit = cmdStr.Split(' ');
        var cmd = cmdSplit[0];
        var units = int.Parse(cmdSplit[1]);

        switch (cmd)
        {
            case "forward":
                position += units;
                break;
            case "up":
                depth -= units;
                break;
            case "down":
                depth += units;
                break;
            default:
                throw new ArgumentOutOfRangeException($"Unrecognized command {cmd}");
        }
    }

    foreach (var command in File.ReadAllLines("input.txt"))
    {
        PerformCommand(command);
    }

    Console.WriteLine("Part 1: [position: {0}] * [depth: {1}] = {2}", position, depth, position*depth);
}

void Part2() {
    var position = 0;
    var depth = 0;
    var aim = 0;

    void PerformCommand(string cmdStr) {
        var cmdSplit = cmdStr.Split(' ');
        var cmd = cmdSplit[0];
        var units = int.Parse(cmdSplit[1]);

        switch (cmd)
        {
            case "forward":
                position += units;
                depth += units*aim;
                break;
            case "up":
                aim -= units;
                break;
            case "down":
                aim += units;
                break;
            default:
                throw new ArgumentOutOfRangeException($"Unrecognized command {cmd}");
        }
    }

    foreach (var command in File.ReadAllLines("input.txt"))
    {
        PerformCommand(command);
    }

    Console.WriteLine("Part 2: [position: {0}] * [depth: {1}] = {2}", position, depth, position*depth);
}