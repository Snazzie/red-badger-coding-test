using FluentAssertions;
using MarsProgram;
using MarsProgram.Data;
using MarsProgram.Utils;

namespace MarsProgramTests;

public class CommandUtilTests
{
    
    [Test]
    public void ShouldParseCommandSequenceCorrectly()
    {
        var commandSequence = "RFL";
        var commands = CommandUtil.ParseCommandSequence(commandSequence);
        
        commands.Should().BeEquivalentTo([Command.R, Command.F, Command.L]);
    }
    
}