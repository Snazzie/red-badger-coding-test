using MarsProgram.Utils;

namespace MarsProgram.Data;

public class ProgramInput
{
    public GridSize GridSize { get; }
    public List<Robot> Robots { get; } = [];
    public Dictionary<Robot, string> RobotCommandMaps { get; } = [];

    public ProgramInput(GridSize gridSize, List<Robot> robots, Dictionary<Robot, string> robotCommandMaps)
    {
        GridSize = gridSize;
        Robots = robots;
        RobotCommandMaps = robotCommandMaps;
    }

    public ProgramInput(string input)
    {
        var lines = input.Split(["\r\n", "\r", "\n"],
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var gridConstraint = ParseGridConstraint(lines[0]);
        GridSize = new GridSize(gridConstraint.x, gridConstraint.y);

        for (var i = 1; i < lines.Length; i += 2)
        {
            var robotInstructionGroup = lines[i..(i + 2)];
            var startingPosition = ParseRobotInitialCoordinate(robotInstructionGroup[0]);
            var position = new Coordinates(startingPosition.x, startingPosition.y);

            var commandSequenceString = robotInstructionGroup[1];
            CommandUtil.ValidateCommandSequence(commandSequenceString);

            var robot = new Robot(startingPosition.direction, position);
            Robots.Add(robot);
            RobotCommandMaps[robot] = commandSequenceString;
        }
    }

    private static (int x, int y) ParseGridConstraint(string line)
    {
        var values = line.Split("  ");
        var x = int.Parse(values[0]);
        var y = int.Parse(values[1]);
        return (x, y);
    }

    private static (int x, int y, Direction direction) ParseRobotInitialCoordinate(string line)
    {
        var values = line.Split("  ");
        var x = int.Parse(values[0]);
        var y = int.Parse(values[1]);
        var direction = (Direction)Enum.Parse(typeof(Direction), values[2]);

        return (x, y, direction);
    }
}