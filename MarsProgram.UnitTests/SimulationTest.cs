using FluentAssertions;
using MarsProgram;
using MarsProgram.Data;

namespace MarsProgramTests;

public class SimulationTest
{
    [Test]
    public void SimulationShouldRunCorrectly()
    {
        const string inputString = """
                                   5  3 
                                   1  1  E 
                                   RFRFRFRF 
                                   
                                   3  2  N 
                                   FRRFLLFFRRFLL 
                                   
                                   0  3  W 
                                   LLFFFLFLFL
                                   """;

        var expectedOutput = new List<string>
        {
            "1  1  E",
            "3  3  N  LOST",
            "2  3  S",
        };

        var input = new ProgramInput(inputString);
        var sim = new Simulation(input);
        
        sim.Run();

        sim.GetResultTexts().Should().BeEquivalentTo(expectedOutput);
    }

    [Test]
    public void IfNewCoordinateContainsLostBot_RobotShouldIgnoreCommand
        ()
    {
        var robot1 = new Robot(Direction.N, new Coordinates(0, 1));
        var robot2 = new Robot(Direction.N, new Coordinates(0, 0));
        var commands = new Dictionary<Robot, string>
        {
            [robot1] = "FFFF",
            [robot2] = "FFFFRF"
        };

        var input = new ProgramInput(new GridBounds(3, 3), [robot1, robot2], commands);

        var sim = new Simulation(input);
        sim.Run();

        sim.GetResultTexts().Should().BeEquivalentTo([
            "0  3  N  LOST",
            "1  3  E"
        ]);
    }

    [Test]
    public void RobotShouldBeSetLost_WhenItReachesTheEdgeOfTheGrid_Y()
    {
        var robot = new Robot(Direction.N, new Coordinates(0, 0));
        var commands = new Dictionary<Robot, string>
        {
            [robot] = "FFFF"
        };

        var input = new ProgramInput(new GridBounds(3, 3), [robot], commands);

        var sim = new Simulation(input);
        sim.Run();

        sim.GetResultTexts().Should().BeEquivalentTo([
            "0  3  N  LOST"
        ]);
    }
    
    [Test]
    public void RobotShouldBeSetLost_WhenItReachesTheEdgeOfTheGrid_X()
    {
        var robot = new Robot(Direction.E, new Coordinates(3, 3));
        var commands = new Dictionary<Robot, string>
        {
            [robot] = "F"
        };

        var input = new ProgramInput(new GridBounds(3, 3), [robot], commands);

        var sim = new Simulation(input);
        sim.Run();

        sim.GetResultTexts().Should().BeEquivalentTo([
            "3  3  E  LOST"
        ]);
    }
}