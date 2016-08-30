<Query Kind="Program" />

static string _fps = "29.97";

static string _startOffsetInFrames = "19";

// These needs to be on the format "00:00.00" (minutes:seconds.hundreds)
static string _loadOneStart = "0:17.06";
static string _loadOneEnd = "0:23.68";

static string _loadTwoStart = "5:44.33";
static string _loadTwoEnd = "5:50.79";

static string _loadThreeStart = "11:50.12";
static string _loadThreeEnd = "11:58.70";

static string _end = "17:29.01";

static void Main()
{
    decimal total = 0m;

    total += StartOffset();

    total += ParseToSeconds(_end);

	Console.WriteLine($"With loads: {AsMinutesAndSeconds(total)}");
	Console.WriteLine($"With loads seconds: {total}");

    total -= CalculateLoadTime(_loadOneStart, _loadOneEnd);
    total -= CalculateLoadTime(_loadTwoStart, _loadTwoEnd);
    total -= CalculateLoadTime(_loadThreeStart, _loadThreeEnd);

    Console.WriteLine($"LoadLess: {AsMinutesAndSeconds(total)}");
	Console.WriteLine($"LoadLess seconds: {total}");
}

static decimal StartOffset()
{
    var fps = decimal.Parse(_fps);
    var startOffsetInFrames = decimal.Parse(_startOffsetInFrames);

    return startOffsetInFrames * (1m / fps);
}

static decimal CalculateLoadTime(string loadStart, string loadEnd)
{
    decimal start = ParseToSeconds(loadStart);
    decimal end = ParseToSeconds(loadEnd);

    return end - start;
}

static string AsMinutesAndSeconds(decimal seconds)
{
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(Math.Floor(seconds / 60)).Append(':');
	if (seconds % 60 < 10)
	{
		stringBuilder.Append('0');
	}
    stringBuilder.Append(seconds % 60);
    return stringBuilder.ToString();
}

static decimal ParseToSeconds(string time)
{
    var splitValues = time.Split(':', '.');

    decimal seconds = 0m;
    seconds += 60m * decimal.Parse(splitValues[0]);
    seconds += decimal.Parse(splitValues[1]);
    seconds += 0.01m * decimal.Parse(splitValues[2]);

    return seconds;
}