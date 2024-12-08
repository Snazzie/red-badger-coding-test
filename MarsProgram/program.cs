using MarsProgram;
using MarsProgram.Data;

// Get the command-line arguments
var commandLineArgs = Environment.GetCommandLineArgs();
if (args.Length > 1 && args[1] == "-input")
{
    if (args.Length == 2)
    {
        var argument = args[2]; // Get the value after -input

        Console.WriteLine(argument);

        var parameters = new ProgramInput(argument);

        var simulation = new Simulation(parameters);
        simulation.Run();

        var result = simulation.GetResultTexts();

        Console.WriteLine(string.Join(Environment.NewLine, result));
    }
    else
    {
        Console.WriteLine("Expected one argument after -input");
    }
}
else
{
    Console.WriteLine("Usage: -input <value>");
}