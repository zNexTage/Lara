using Lara.Domain.DataTransferObjects;

namespace Lara.Domain.Contracts;

public interface IBaseTokenService
{
    public TokenDto GenerateToken(string id);
}