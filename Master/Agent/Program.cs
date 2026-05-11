using System.IO.Pipes;

class Program
{
    static async Task Main(string[] args)
    {
        string name = args[0];

        using NamedPipeClientStream pipe =
            new (
                ".",
                "mypipe",
                PipeDirection.InOut);

        await pipe.ConnectAsync();

        using StreamReader reader =
            new (pipe);

        using StreamWriter writer =
            new (pipe);

        writer.AutoFlush = true;

        // gaunam skaiciu
        string? line = await reader.ReadLineAsync();

        if (line == null)
        {
            Console.WriteLine("No data received.");
            return;
        }

        int secret = int.Parse(line);

        Random rnd = new();

        while (true)
        {
            int guess = rnd.Next(1, 101);

            if (guess == secret)
            {
                string msg =
                    $"Agentas {name} atspejo {secret}";

                await writer.WriteLineAsync(msg);

                break;
            }
        }
    }
}