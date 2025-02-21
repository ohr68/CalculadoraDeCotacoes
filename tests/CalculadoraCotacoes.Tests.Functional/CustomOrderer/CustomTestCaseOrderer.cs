using Xunit.Abstractions;
using Xunit.Sdk;

namespace CalculadoraCotacoes.Tests.Functional.CustomOrderer;

public class CustomTestCaseOrderer: ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        // Order the tests based on the "Order" property or other logic
        return testCases.OrderBy(tc => tc.TestMethod.Method.GetCustomAttributes(typeof(TestPriorityAttribute))
            .FirstOrDefault()?.GetNamedArgument<int>("Priority") ?? 0);
    }
}
