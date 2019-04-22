namespace TOTP_Framework.Interfaces
{
    public interface IAlgorithm
    {
        string Generate(string timeCounter, string key);
    }
}
