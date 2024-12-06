namespace MarsProgram.Data;

public class Robot
{
    public Coordinates LastCoordinates { get; private set; }
    public Coordinates Coordinates { get; private set; }
    public Direction Direction { get; private set; }
    public Status Status { get; set; } = Status.Online;

    public Robot(Direction direction, Coordinates coordinates)
    {
        LastCoordinates = coordinates;
        Coordinates = coordinates;
        Direction = direction;
    }

    public (Coordinates coordinates, Direction direction) GetNewPosition(Command command)
    {
        switch (command)
        {
            case Command.R:
            case Command.L:
                var newDirection = Turn(command);
                return (Coordinates, newDirection);
            case Command.F:
                var newPosition = Forward();
                return (newPosition, Direction);
            default:
                throw new ArgumentOutOfRangeException(nameof(command), command, null);
        }
    }

    public void ExecuteCommand(Command command)
    {
        LastCoordinates = Coordinates;
        var plan = GetNewPosition(command);

        Coordinates = plan.coordinates;
        Direction = plan.direction;
    }

    Coordinates Forward()
    {
        return Direction switch
        {
            Direction.N => Coordinates with { Y = Coordinates.Y + 1 },
            Direction.S => Coordinates with { Y = Coordinates.Y - 1 },
            Direction.E => Coordinates with { X = Coordinates.X + 1 },
            Direction.W => Coordinates with { X = Coordinates.X - 1 },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    Direction Turn(Command turnCommand)
    {
        switch (turnCommand)
        {
            case Command.L:
            {
                var newDirection = (int)Direction - 90;
                if (newDirection < 0)
                    newDirection += 360;
                return (Direction)newDirection;
            }
            case Command.R:
            {
                var newDirection = (int)Direction + 90;
                if (newDirection >= 360)
                    newDirection -= 360;
                return (Direction)newDirection;
            }
            default:
                throw new ArgumentOutOfRangeException(nameof(turnCommand), turnCommand, null);
        }
    }
}