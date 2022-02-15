// See https://aka.ms/new-console-template for more information
var httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:5113") };

await Task.Delay(5000);
Console.WriteLine("Starting");


await Parallel.ForEachAsync(
    Enumerable.Range(0, 10000),
    new ParallelOptions { MaxDegreeOfParallelism = 5 },
    async (number, cancellationToken) =>
{
    var result = await httpClient.GetAsync("/weatherforecast");
    await Task.Delay(100);
    Console.WriteLine(result.StatusCode);
});

Console.ReadKey();