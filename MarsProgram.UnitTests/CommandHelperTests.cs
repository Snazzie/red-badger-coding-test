using FluentAssertions;
using MarsProgram;
using MarsProgram.Data;
using MarsProgram.Utils;

namespace MarsProgramTests;

public class CommandHelperTests
{
    [Test]
    public void CommandSequenceIsParsedCorrectly()
    {
        var commandSequence = "RFL";
        var commands = CommandHelper.ParseCommandSequence(commandSequence);
        
        commands.Should().BeEquivalentTo([Command.R, Command.F, Command.L]);
    }
    
}