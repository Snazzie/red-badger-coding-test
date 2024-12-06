using MarsProgram.Data;
using MarsProgram.Utils;

namespace MarsProgram;

public class Simulation
{
    BotNetwork BotNetwork { get; }
    ProgramInput ProgramInput { get; }

    public Simulation(ProgramInput programInput)
    {
        ProgramInput = programInput;

        BotNetwork = new BotNetwork(programInput.GridSize);
        BotNetwork.AddRobots(programInput.Robots);
    }

    public void Run()
    {
        foreach (var robot in ProgramInput.Robots)
        {
            var commandSequence = ProgramInput.RobotCommandMaps[robot];
            BotNetwork.ExecuteCommands(CommandUtil.ParseCommandSequence(commandSequence).ToArray(), robot);
        }

        var result = GetResult();

        Console.WriteLine(string.Join("\n", result));
    }

    public IEnumerable<string> GetResult()
    {
        foreach (var robot in ProgramInput.Robots)
        {
            if (robot.Status == Status.Lost)
            {
                yield return $"{robot.LastCoordinates.X}  {robot.LastCoordinates.Y}  {robot.Direction}  LOST";
            }
            else
            {
                yield return $"{robot.Coordinates.X}  {robot.Coordinates.Y}  {robot.Direction}";
            }
        }
    }
}