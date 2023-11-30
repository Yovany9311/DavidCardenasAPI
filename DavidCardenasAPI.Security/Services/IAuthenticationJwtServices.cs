namespace DavidCardenasAPI.Security.Services
{
    public interface IAuthenticationJwtServices
    {
        Task<string> GetToken();
    }
}
