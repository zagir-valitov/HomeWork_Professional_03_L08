using System.Diagnostics;

Console.WriteLine("Home Work Professional 3 (1, 3)\n");

string dirName = @"TextFiles";
var stopWatch = new Stopwatch();

if (Directory.Exists(dirName))
{
    string[] files = Directory.GetFiles(dirName, "*.txt");
    if (files.Length >= 3)
    {
        Console.WriteLine("Sequential file reading...");
        stopWatch.Start();

        var count1 = GetNumberOfSpacesInTxtFile(files[0]);
        var count2 = GetNumberOfSpacesInTxtFile(files[1]);
        var count3 = GetNumberOfSpacesInTxtFile(files[2]);

        stopWatch.Stop();
        Console.WriteLine($"Time: {stopWatch.Elapsed.TotalMilliseconds} ms\n");

        Console.WriteLine("Parallel file reading...");
        stopWatch.Restart();

        Task<int>[] tasks =
        [
            Task.Run(() => GetNumberOfSpacesInTxtFile(files[0])),
            Task.Run(() => GetNumberOfSpacesInTxtFile(files[1])),
            Task.Run(() => GetNumberOfSpacesInTxtFile(files[2]))
        ];
        Task.WaitAll(tasks);

        stopWatch.Stop();
        Console.WriteLine($"Time: {stopWatch.Elapsed.TotalMilliseconds} ms\n");
    }
}

int GetNumberOfSpacesInTxtFile(string path)
{
    int count = 0;
    string text;

    using (var reader = new StreamReader(path))
    {
        text = reader.ReadToEnd();
    }

    foreach (var ch in text)
        if (ch == ' ') count++;

    return count;
}


