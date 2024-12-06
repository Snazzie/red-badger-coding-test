using FluentAssertions;
using MarsProgram.Data;

namespace MarsProgramTests;

public class InputTests
{
    const string InputString = """
                               5  3 
                               1  1  E 
                               RFRFRFRF 
                               3  2  N 
                               FRRFLLFFRRFLL 
                               0  3  W 
                               LLFFFLFLFL
                               """;

    [Test]
    public void GridBoundIsParsedCorrectly()
    {
        var input = new ProgramInput(InputString);

        input.GridBounds.X.Should().Be(5);
        input.GridBounds.Y.Should().Be(3);
    }

    [Test]
    public void RobotInstructionsIsParsedCorrectly()
    {
        var input = new ProgramInput(InputString);

        input.Robots.Count.Should().Be(3);

        input.Robots[0].Coordinates.X.Should().Be(1);
        input.Robots[0].Coordinates.Y.Should().Be(1);
        input.Robots[0].Direction.Should().Be(Direction.E);

        input.Robots[1].Coordinates.X.Should().Be(3);
        input.Robots[1].Coordinates.Y.Should().Be(2);
        input.Robots[1].Direction.Should().Be(Direction.N);


        input.Robots[2].Coordinates.X.Should().Be(0);
        input.Robots[2].Coordinates.Y.Should().Be(3);
        input.Robots[2].Direction.Should().Be(Direction.W);
    }

    [Test]
    public void ShouldSetRobotCommands()
    {
        var input = new ProgramInput(InputString);

        input.RobotCommandMaps.Should().ContainKey(input.Robots[0]);
        input.RobotCommandMaps[input.Robots[0]].Should().Be("RFRFRFRF");

        input.RobotCommandMaps.Should().ContainKey(input.Robots[1]);
        input.RobotCommandMaps[input.Robots[1]].Should().Be("FRRFLLFFRRFLL");

        input.RobotCommandMaps.Should().ContainKey(input.Robots[2]);
        input.RobotCommandMaps[input.Robots[2]].Should().Be("LLFFFLFLFL");
    }

    [TestCase(-1, 0)]
    [TestCase(0, -1)]
    [TestCase(51, 0)]
    [TestCase(0, 51)]
    public void IfBoundIsOutOfRange_ThrowOutOfRange(int x, int y)
    {
        var action = new Action(() =>
            new ProgramInput(new GridBounds(x, y), new List<Robot>(), new Dictionary<Robot, string>()));

        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestCase(1, 0)]
    [TestCase(0, 1)]
    [TestCase(50, 0)]
    [TestCase(0, 50)]
    public void IfBoundIsWithinRange_ShouldNotThrow(int x, int y)
    {
        var action = new Action(() =>
            new ProgramInput(new GridBounds(x, y), new List<Robot>(), new Dictionary<Robot, string>()));

        action.Should().NotThrow();
    }
}