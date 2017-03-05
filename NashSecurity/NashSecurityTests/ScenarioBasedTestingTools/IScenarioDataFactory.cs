namespace NashSecurity.Tests.ScenarioBasedTestingTools
{
    public interface IScenarioDataFactory<T>
    {
        T GetScenarioData();
    }
}