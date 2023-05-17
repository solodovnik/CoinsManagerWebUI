using System.Threading.Tasks;

namespace CoinsManagerWebUI.Authentication
{
    public interface IAuthenticator
    {
        Task<string> GetToken();
    }
}
