using CaptainSonar.Common.Domain.Map;
using CaptainSonar.Server.Engine;
using Common.Domain.Commands;

namespace CaptainSonar.Tests.Utils;

public static class TestUtils
{
    public static StateExecutionStep CreateStateExecutionStep(List<ICommandBase>? commands)
    {
        var grid = new Grid(GridType.Alpha);
        StateExecutionStep stateExecutionStep = new()
        {
            State = StateHelper.CreateState(grid),
            Commands = commands ?? []
        };
        return stateExecutionStep;
    }
}
