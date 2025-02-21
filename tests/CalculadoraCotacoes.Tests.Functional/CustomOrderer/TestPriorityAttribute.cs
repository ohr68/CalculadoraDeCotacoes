namespace CalculadoraCotacoes.Tests.Functional.CustomOrderer;

[AttributeUsage(AttributeTargets.Method)]
public class TestPriorityAttribute(int priority) : Attribute
{
    public int Priority { get; } = priority;
}