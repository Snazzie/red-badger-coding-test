using MarsProgram.Utils;

namespace MarsProgram.Data;

public class ProgramInput
{
    public GridBounds GridBounds { get; }
    public List<Robot> Robots { get; } = [];
    public Dictionary<Robot, string> RobotCommandMaps { get; } = [];

    public ProgramInput(GridBounds gridBounds, List<Robot> robots, Dictionary<Robot, string> robotCommandMaps)
    {
        GridBounds = gridBounds;
        Robots = robots;
        RobotCommandMaps = robotCommandMaps;
    }

    public ProgramInput(string input)
    {
        var lines = input.Split(["\r\n", "\r", "\n"],
            StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var gridConstraint = ParseGridConstraint(lines[0]);
        GridBounds = new GridBounds(gridConstraint.x, gridConstraint.y);

        for (var i = 1; i < lines.Length; i += 2)
        {
            var robotInstructionGroup = lines[i..(i + 2)];
            var startingPosition = ParseRobotInitialCoordinate(robotInstructionGroup[0]);
            var coordinates= new Coordinates(startingPosition.x, startingPosition.y);

            var commandSequenceString = robotInstructionGroup[1];
            CommandHelper.ValidateCommandSequence(commandSequenceString);

            var robot = new Robot(startingPosition.direction, coordinates);
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
        var direction = Enum.Parse<Direction>(values[2]);

        return (x, y, direction);
    }
}