using MarsProgram.Data;

namespace MarsProgram;

public class BotNetwork
{
    GridSize GridSize { get; }

    public List<Robot> Robots { get; } = [];

    public void AddRobots(IEnumerable<Robot> robots) => Robots.AddRange(robots);
    
    public BotNetwork(GridSize gridSize)
    {
        GridSize = gridSize;
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
                robot.Status = Status.Lost;

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
        return coordinates.X <= GridSize.X &&
               coordinates.Y <= GridSize.Y &&
               coordinates is { X: >= 0, Y: >= 0 };
    }
}

public enum Status
{
    Online,
    Lost
}