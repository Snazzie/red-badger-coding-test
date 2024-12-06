using MarsProgram.Data;

namespace MarsProgram.Utils;

public static class CommandHelper
{
    public static void ValidateCommandSequence(string commandSequence)
    {
        for (var i = 0; i < commandSequence.Length; i++)
            if (!Enum.IsDefined(typeof(Command), commandSequence[i].ToString()))
                throw new ArgumentException($"Invalid command at index {i}, value: {commandSequence[i]}");
    }

    public static IEnumerable<Command> ParseCommandSequence(string commandSequence)
    {
        foreach (var command in commandSequence)
        {
            if (!Enum.IsDefined(typeof(Command), command.ToString()))
                throw new ArgumentException($"Invalid command: {command}");

            yield return (Command)Enum.Parse(typeof(Command), command.ToString());
        }
    }
}