namespace NashSecurity.Tests.StateAbstractions
{
    public interface IHasCryptorCreatedData : IHasEnteredData
    {
        ICryptor Cryptor { get; set; }
    }
}