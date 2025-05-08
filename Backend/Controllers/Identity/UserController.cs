using ALBackend.Services.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Identity;

[ApiController,Route("/users")]
public class UserController(IUsersFetch fetcher) : ControllerBase
{
    [HttpGet, Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN,IT,CASHIER")]
    public JsonResult GetUsers()
    {
        var users = fetcher.Users();
        return new(users);
    }

    [HttpGet, Route("list"),Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN,IT,CASHIER")]
    public JsonResult GetListUsers()
    {
        var users = fetcher.ListUsers();
        var dtos = users.Select(user => user.ToListDto()).ToList();
        return new(dtos);
    }
    
}