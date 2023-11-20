using System.Net.Mail;

namespace GIH.Entities;

public class Person
{
    public int PersonId { get; set; }
    public string PersonName { get; set; }
    public string PersonSurname { get; set; } 
    public string PersonEmail { get; set; }
    public string PersonPassword { get; set; }
    public string PersonNickName { get; set; }
    public string PersonPhoneNumber { get; set; }
    public int RoleId { get; set; }
    public string PasswordSalt { get; set; }
}