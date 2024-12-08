using MarsProgram.Data;
using MarsProgram.Utils;

namespace MarsProgram;

public class Simulation
{
    RobotCommander RobotCommander { get; }
    ProgramInput ProgramInput { get; }

    public Simulation(ProgramInput programInput)
    {
        ProgramInput = programInput;

        RobotCommander = new RobotCommander(programInput.GridBounds);
        RobotCommander.AddRobots(programInput.Robots);
    }

    public void Run()
    {
        foreach (var robot in ProgramInput.Robots)
        {
            var commandSequence = ProgramInput.RobotCommandMaps[robot];
            RobotCommander.ExecuteCommands(CommandHelper.ParseCommandSequence(commandSequence).ToArray(), robot);
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