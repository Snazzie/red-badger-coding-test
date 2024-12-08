using MarsProgram.Data;

namespace MarsProgram;

public class RobotCommander
{
    GridBounds GridBounds { get; }

    public List<Robot> Robots { get; } = [];

    public void AddRobots(IEnumerable<Robot> robots) => Robots.AddRange(robots);
    
    public RobotCommander(GridBounds gridBounds)
    {
        GridBounds = gridBounds;
    }


    public void ExecuteCommands(Command[] commandSequence, Robot robot)
    {
        for (int i = 0; i < commandSequence.Length; i++)
        {
            var plannedCoordinates = robot.GetNewPosition(commandSequence[i]);

            if (CoordinateReportedLost(plannedCoordinates.coordinates))
                continue;

            robot.ExecuteCommand(commandSequence[i]);

            if (!PositionIsInGrid(robot.Coordinates))
            {
                robot.SetStatus(Status.Lost);

                return;
            }
        }
    }

    bool CoordinateReportedLost(Coordinates coordinates)
    {
        return Robots.Any(e => e.Status == Status.Lost && e.Coordinates == coordinates);
    }

    bool PositionIsInGrid(Coordinates coordinates)
    {
        return coordinates.X <= GridBounds.X &&
               coordinates.Y <= GridBounds.Y &&
               coordinates is { X: >= 0, Y: >= 0 };
    }
}

public enum Status
{
    Online,
    Lost
}