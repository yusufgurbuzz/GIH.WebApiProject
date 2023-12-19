namespace GIH.Interfaces.Services;

public interface IPersonValidateService
{
     bool ValidatePerson(string nickname, string password);
     
}