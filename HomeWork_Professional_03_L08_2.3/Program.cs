using System.Diagnostics;

Console.WriteLine("Home Work Professional 3 (2, 3)\n");

string dirName = @"TextFiles";
InfoNumberOfSpacesInTextFiles(dirName);

void InfoNumberOfSpacesInTextFiles(string path)
{
    if (Directory.Exists(path))
    {
        Task[]tasksArray = new Task[1];
        var stopWatch = new Stopwatch();
        string[] files = Directory.GetFiles(path, "*.txt");
        Array.Resize(ref tasksArray, files.Length);

        Console.WriteLine("Sequential file reading...");
        stopWatch.Start();

        foreach (var file in files)
            GetNumberOfSpacesInTextFile(file);

        stopWatch.Stop();
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Time: {stopWatch.Elapsed.TotalMilliseconds} ms\n");


        Console.WriteLine("Parallel file reading...");
        stopWatch.Restart();

        for (int i = 0; i < tasksArray.Length; i++)
        {
            int j = i;
            tasksArray[j] = Task.Run(() => GetNumberOfSpacesInTextFile(files[j]));
        }               
        Task.WaitAll(tasksArray);
        stopWatch.Stop();
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Time: {stopWatch.Elapsed.TotalMilliseconds} ms\n");
    }
}

void GetNumberOfSpacesInTextFile(string path)
{
    int count = 0;
    string text;

    using (var reader = new StreamReader(path))
    {
        text = reader.ReadToEnd();
    }
    foreach (var ch in text)
        if (ch == ' ') count++;
    Console.WriteLine($"{path}\t - {count} spaces");    
}