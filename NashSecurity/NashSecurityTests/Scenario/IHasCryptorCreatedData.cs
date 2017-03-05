namespace NashSecurity.Tests.Scenario
{
    public interface IHasCryptorCreatedData : IHasEnteredData
    {
        ICryptor Cryptor { get; set; }
    }
}