Part1();
Part2();

void Part1() {
    var nrOfIncreasingMeasurements = 0;
    var previousMeasurement = -1;
    var measurements = File.ReadAllLines("input.txt").Select(l => int.Parse(l));
    foreach (var measurement in measurements) {
        if (previousMeasurement > -1 && (measurement > previousMeasurement)) {
            nrOfIncreasingMeasurements++;
        }
        previousMeasurement = measurement;
    }

    Console.WriteLine("Part 1: {0}", nrOfIncreasingMeasurements);
}

void Part2() {
    var nrOfIncreasingMeasurements = 0;
    var previousSum = -99999;
    var measurements = File.ReadAllLines("input.txt").Select(l => int.Parse(l));
    for (var i = 0; i < measurements.Count(); i ++) {
        var sum = measurements.Skip(i).Take(3).Sum();
        if (previousSum > -99999 && sum > previousSum) {
            nrOfIncreasingMeasurements++;
        }
        previousSum = sum;
    }

    Console.WriteLine("Part 2: {0}", nrOfIncreasingMeasurements);
}