using FluentAssertions;
using MarsProgram;
using MarsProgram.Data;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace MarsProgramTests;

public class RobotTests
{
    [TestCase(Direction.N, Command.R, Direction.E)]
    [TestCase(Direction.E, Command.R, Direction.S)]
    [TestCase(Direction.S, Command.R, Direction.W)]
    [TestCase(Direction.W, Command.R, Direction.N)]
    [TestCase(Direction.N, Command.L, Direction.W)]
    [TestCase(Direction.W, Command.L, Direction.S)]
    [TestCase(Direction.S, Command.L, Direction.E)]
    [TestCase(Direction.E, Command.L, Direction.N)]
    public void RobotShouldBeTurnedCorrectly(Direction startingDirection, Command command,
        Direction expectedEndDirection)
    {
        var robot = new Robot(startingDirection, new Coordinates(1, 1));

        robot.ExecuteCommand(command);

        robot.Direction.Should().Be(expectedEndDirection);
    }

    [Test, TestCaseSource(nameof(MoveTestCases))]
    public void RobotShouldMoveForwardCorrectly(Direction startingDirection,
        Coordinates expectedEndPosition)
    {
        var robot = new Robot(startingDirection, new Coordinates(0, 0));

        robot.ExecuteCommand(Command.F);

        robot.Coordinates.Should().Be(expectedEndPosition);
    }

    static IEnumerable<TestCaseData> MoveTestCases()
    {
        yield return new TestCaseData(Direction.N, new Coordinates(0, 1)).SetName("Move North");
        yield return new TestCaseData(Direction.E, new Coordinates(1, 0)).SetName("Move East");
        yield return new TestCaseData(Direction.S, new Coordinates(0, -1)).SetName("Move South");
        yield return new TestCaseData(Direction.W, new Coordinates(-1, 0)).SetName("Move West");
    }

}