using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;
using GIH.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GIH.WebApi.Controllers;

[Route("api/[controller]"), ApiController]
public class PersonController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    private readonly IAuthenticationService _authService;
    private readonly IPersonValidateService _personValidateService;
    public PersonController(IServiceManager serviceManager,IPersonValidateService personValidateService, 
        IAuthenticationService authService)
    {
        _serviceManager = serviceManager;
        _personValidateService = personValidateService;
        _authService = authService;
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
    //[Authorize] tokenli girilen yerlere koy
    [HttpPost("Login")]
    public IActionResult LoginPerson(PersonForAuthenticationDto personForAutDto)
    {
        try
        {
            if (_personValidateService.ValidatePerson(personForAutDto.Username, personForAutDto.Password))
            {
                var token = _authService.GenerateJwtToken(personForAutDto.Username);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}