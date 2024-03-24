using Lara.Domain.DataTransferObjects;
using Lara.Domain.Entities;

namespace Lara.Domain.Contracts;

public interface IBaseTokenService
{
    public Task<TokenDto> GenerateToken(ApplicationUser user);
}