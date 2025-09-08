using AADP.Domain.Model;

namespace AADP.Application.Port.In
{
    public interface ILogInServices
    {
        Task<Token> GenerateTokenAsync(UserCredential userCredential);
    }
}
