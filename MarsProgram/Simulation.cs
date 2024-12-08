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

        BotNetwork = new BotNetwork(programInput.GridBounds);
        BotNetwork.AddRobots(programInput.Robots);
    }

    public void Run()
    {
        foreach (var robot in ProgramInput.Robots)
        {
            var commandSequence = ProgramInput.RobotCommandMaps[robot];
            BotNetwork.ExecuteCommands(CommandHelper.ParseCommandSequence(commandSequence).ToArray(), robot);
        }
    }

    public IEnumerable<string> GetResultTexts()
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