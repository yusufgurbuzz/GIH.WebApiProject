using GIH.Entities;
using GIH.Entities.DTOs;
using GIH.Interfaces.Managers;
using GIH.Interfaces.Services;
using GIH.Services;
using Microsoft.AspNetCore.Mvc;

namespace GIH.WebApi.Controllers;

[Route("api/[controller]"), ApiController]
public class RestaurantController : Controller
{
    private readonly IServiceManager _serviceManager;
    private readonly IAuthenticationService _authService;
    private readonly IRestaurantValidateService _personValidateService;
    public RestaurantController(IServiceManager serviceManager,IRestaurantValidateService personValidateService, 
        IAuthenticationService authService)
    {
        _serviceManager = serviceManager;
        _personValidateService = personValidateService;
        _authService = authService;
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
    public IActionResult LoginRestaurant(RestaurantForAuthentication restaurantForAut)
    {
        if (_personValidateService.ValidateRestaurant(restaurantForAut.Username, restaurantForAut.Password))
        {
            // Kullanıcı doğrulama başarılı, JWT token oluşturulur.
            var token = _authService.GenerateJwtToken(restaurantForAut.Username);
            return Ok(new { token });
        }
        return Unauthorized();
    }

    [HttpGet ("Advert")]
    public IActionResult GetAdvert()
    {
        var adverts = _serviceManager.AdvertService.GetAdvert();
        return Ok(adverts);
    }
    [HttpGet("AdvertId")]
    public IActionResult GetAdvertById(int id)
    {
        var advert = _serviceManager.AdvertService.GetAdvertById(id);
        
        if (advert is null)
        {
            return NotFound();
        }
        return Ok(advert);
    }
    [HttpGet("AdvertAdress")]
    public IActionResult GetAdvertByAdress(string adress)
    {
        var advert = _serviceManager.AdvertService.GetAdvertByAdress(adress);
        
        if (advert is null)
        {
            return NotFound();
        }
        return Ok(advert);
    }
    [HttpPost("AdvertAdd")]
    public IActionResult CreateAdvert([FromBody] Advert advert)
    {
            if (advert is null)
            {
                return BadRequest();
            }
            _serviceManager.AdvertService.CreateAdvert(advert);
            return Ok(advert);
        
        
    }
    [HttpPut ("AdvertUpdate")]
    public IActionResult UpdateAdvertById(int id,AdvertDto advertDto)
    {
        try
        {
            if (advertDto is null)
            {
                return BadRequest();
            }
            _serviceManager.AdvertService.UpdateAdvertById(id,advertDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

  [HttpDelete ("AdvertDelete")]
    public IActionResult DeleteAdvertById(int id)
    {
        try
        {
            _serviceManager.AdvertService.DeleteAdvertById(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    
}