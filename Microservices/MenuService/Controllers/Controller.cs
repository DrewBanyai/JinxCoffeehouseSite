using Microsoft.AspNetCore.Mvc;

namespace REST_API.Controllers;

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
    public string Post()
    {
        return "This route does not have a POST request method, only GET. Try again.";
    }


    //  READ
    [HttpGet(Name = "Get")]
    public List<string> Get()
    {
        return new List<string>() {
            "Hello, World!",
            "The Jinx Coffeehouse API is LIVE!",
            "Version 1.0.0"
        };
    }


    //  UPDATE
    [HttpPut(Name = "Put")]
    public string Put()
    {
        return "This route does not have a PUT request method, only GET. Try again.";
    }


    //  DELETE
    [HttpDelete(Name = "Delete")]
    public string Delete()
    {
        return "This route does not have a DELETE request method, only GET. Try again.";
    }
}
