namespace GIH.Interfaces.Managers;

public interface IAuthenticationService
{
    public string GenerateJwtToken(string username);
}