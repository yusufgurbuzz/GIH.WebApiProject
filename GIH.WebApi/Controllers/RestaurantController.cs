using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Services;
using Microsoft.AspNetCore.Mvc;

namespace GIH.WebApi.Controllers;

[Route("api/[controller]"), ApiController]
public class RestaurantController : Controller
{
    private readonly IServiceManager _serviceManager;
  
    public RestaurantController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public IActionResult GetPerson()
    {
        var restaurant = _serviceManager.RestaurantService.GetRestaurant();
        return Ok(restaurant);
    }
    [HttpGet("{id:int}")]
    public IActionResult GetRestorantById(int id)
    {
        var restorant = _serviceManager.RestaurantService.GetRestaurantById(id);
        
        if (restorant is null)
        {
            return NotFound();
        }
        return Ok(restorant);
    }
    [HttpGet("adres")]
    public IActionResult GetRestorantByAdress(string adress)
    {
        var restorant = _serviceManager.RestaurantService.GetRestaurantByAdress(adress);
        
        if (restorant is null)
        {
            return NotFound();
        }
        return Ok(restorant);
    }
    [HttpPost]
    public IActionResult CreateRestaurant([FromBody] Restaurant restaurant)
    {
        try
        {
            if (restaurant is null)
            {
                return BadRequest();
            }
            _serviceManager.RestaurantService.CreateRestaurant(restaurant);
            return Ok(restaurant);
        }
        catch 
        {
            return BadRequest("This e-mail address or username is used" );
        }
    }
    [HttpPut ("{id:int}")]
    public IActionResult UpdateRestaurantById(int id,RestaurantDto restaurantDto)
    {
        try
        {
            if (restaurantDto is null)
            {
                return BadRequest();
            }
            _serviceManager.RestaurantService.UpdateRestaurantById(id,restaurantDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut("UpdatePassword")]
    public IActionResult UpdateRestaurantByEmail(string email,string currentPassword,string newPassword)
    {
        try
        {
            bool passwordUpdated = _serviceManager.RestaurantService.UpdatePassword(email,currentPassword, newPassword);

            if (passwordUpdated)
            {
                return Ok("Password updated successfully");
            }
            else
            {
                return BadRequest("The current password was not verified or the user was not found.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Something went wrong: {ex.Message}");
        }
    }
    [HttpDelete ("{id:int}")]
    public IActionResult DeleteRestaurantById(int id)
    {
        try
        {
            _serviceManager.RestaurantService.DeleteRestaurantById(id);
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
        var userLogin = _serviceManager.RestaurantService.GetRestaurantByNickName(nickName);
        
        if (userLogin is not null && PasswordHasher.VerifyPassword(password, userLogin.restaurantPassword, userLogin.PasswordSalt))
        {
            return Ok(new { Message = "Login successful" });
        }
        return BadRequest();
    }
    
}