using System.Diagnostics;
using System.IO.Pipes;

class Program
{
    static async Task Main()
    {
        int secret = new Random().Next(1, 101);

        Console.WriteLine($"Skaicius: {secret}");

        Stopwatch sw = Stopwatch.StartNew();

        // paleidziam agentus
        Process.Start("Agent.exe", "1");
        Process.Start("Agent.exe", "2");

        // laukiam 2 agentu
        for (int i = 0; i < 2; i++)
        {
            _ = HandleAgent(secret, sw);
        }

        Console.ReadLine();
    }

    static async Task HandleAgent(
        int secret,
        Stopwatch sw)
    {
        using NamedPipeServerStream pipe =
            new (
                "mypipe",
                PipeDirection.InOut,
                2,
                PipeTransmissionMode.Byte,
                PipeOptions.Asynchronous);

        await pipe.WaitForConnectionAsync();

        using StreamReader reader =
            new (pipe);

        using StreamWriter writer =
            new (pipe);

        writer.AutoFlush = true;

        // issiunciam skaiciu
        await writer.WriteLineAsync(secret.ToString());

        // gaunam atsakyma
        string? msg =
            await reader.ReadLineAsync();

        Console.WriteLine(msg);

        Console.WriteLine(
            $"Laikas: {sw.ElapsedMilliseconds} ms");
    }
}