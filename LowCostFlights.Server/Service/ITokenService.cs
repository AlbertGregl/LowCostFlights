namespace LowCostFlights.Server.Service
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
    }
}
