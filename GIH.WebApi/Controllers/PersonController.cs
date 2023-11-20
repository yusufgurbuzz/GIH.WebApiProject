using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Services;
using Microsoft.AspNetCore.Mvc;

namespace GIH.WebApi.Controllers;

[Route("api/[controller]"), ApiController]
public class PersonController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
   
    public PersonController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public IActionResult GetPerson()
    {
        var products = _serviceManager.PersonService.GetPerson();
        return Ok(products);
    }
    [HttpGet("{id:int}")]
    public IActionResult GetPersonById(int id)
    {
       
        var products = _serviceManager.PersonService.GetPersonById(id);
        if (products is null)
        {
            return NotFound();
        }
        return Ok(products);
    }
    [HttpPost]
    public IActionResult CreatePerson([FromBody] Person person)
    {
        try
        {
            if (person is null)
            {
                return BadRequest();
            }
            _serviceManager.PersonService.CreatePerson(person);
            return Ok(person);
        }
        catch (Exception ex)
        {
            return BadRequest("Bu mail adresi veya kullanıcı adı kullanılmaktadır" );
        }
    }
    [HttpPut ("{id:int}")]
    public IActionResult UpdatePersonById(int id,PersonDto person)
    {
        try
        {
            if (person is null)
            {
                return BadRequest();
            }
            _serviceManager.PersonService.UpdatePersonById(id,person);
            return NoContent();
        }
        catch (Exception ex)
        {
           return BadRequest(ex.Message);
        }
    }
    [HttpPut("UpdatePassword")]
    public IActionResult UpdatePersonByEmail(string email,string currentPassword,string newPassword)
    {
        try
        {
            // Kullanıcı şifresini güncelle
            bool passwordUpdated = _serviceManager.PersonService.UpdatePassword(email,currentPassword, newPassword);

            if (passwordUpdated)
            {
                return Ok("Şifre başarıyla güncellendi.");
            }
            else
            {
                return BadRequest("Mevcut şifre doğrulanamadı veya kullanıcı bulunamadı.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Bir hata oluştu: {ex.Message}");
        }
    }
    [HttpDelete ("{id:int}")]
    public IActionResult DeletePersonById(int id)
    {
        try
        {
            _serviceManager.PersonService.DeletePersonById(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("Login")]
    public IActionResult LoginPerson(string nickName, string password)
    {
        var userLogin =  _serviceManager.PersonService.GetPersonByNickName(nickName);
        
        if (userLogin is not null && PasswordHasher.VerifyPassword(password, userLogin.PersonPassword, userLogin.PasswordSalt))
        {
            return Ok(new { Message = "Login successful" });
        }
        return BadRequest();
    }
}