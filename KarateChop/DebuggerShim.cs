using NSpec.Domain;
using System.Reflection;
using NSpec;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class DebuggerShim
{
    [Test]
    public void debug()
    {
        var tagOrClassName = "Describe_KarateChop";

        var invocation = new RunnerInvocation(Assembly.GetExecutingAssembly().Location, tagOrClassName);

        var contexts = invocation.Run();

        //assert that there aren't any failures
        contexts.Failures().Count().should_be(0);
    }
}
