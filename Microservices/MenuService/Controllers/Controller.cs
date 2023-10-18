using Microsoft.AspNetCore.Mvc;
using MenuService.Models;

namespace MenuService.Controllers;

[ApiController]
[Route("[controller]")]
public class Controller : ControllerBase
{
    private readonly ILogger<Controller> _logger;

    public Controller(ILogger<Controller> logger)
    {
        _logger = logger;
    }


    //  CREATE
    [HttpPost(Name = "Post")]
    public ServiceResponse<string> Post()
    {
        return new ServiceResponse<string>() {
            Data = null,
            Success = true,
            Message = "This route does not have a POST request method, only GET. Try again."
        };
    }


    //  READ
    [HttpGet(Name = "Get")]
    public ServiceResponse<List<string>> Get()
    {
        return new ServiceResponse<List<string>>() {
            Data = new List<string>() {
            "Hello, World!",
            "The Jinx Coffeehouse MenuService API is LIVE!",
            "Version 1.0.0"
            },
            Success = true,
            Message = ""
        };
    }


    //  UPDATE
    [HttpPut(Name = "Put")]
    public ServiceResponse<string> Put()
    {
        return new ServiceResponse<string>() {
            Data = null,
            Success = true,
            Message = "This route does not have a PUT request method, only GET. Try again."
        };
    }


    //  DELETE
    [HttpDelete(Name = "Delete")]
    public ServiceResponse<string> Delete()
    {
        return new ServiceResponse<string>() {
            Data = null,
            Success = true,
            Message = "This route does not have a DELETE request method, only GET. Try again."
        };
    }
}
