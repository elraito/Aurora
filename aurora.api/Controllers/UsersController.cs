using Aurora.Api.Dtos.User.Requests;
using Aurora.Api.Entities;
using Aurora.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAll() =>
        Ok(await _userService.GetAll());

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id) =>
        Ok(await _userService.GetById(id));

    [HttpPost]
    public async Task<ActionResult<User>> Create([FromBody] CreateRequest dto) =>
        Ok(await _userService.Create(dto));

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Update([FromRoute] int id, [FromBody] UpdateRequest dto) =>
        Ok(await _userService.Update(id, dto));

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete([FromRoute] int id) =>
        Ok(await _userService.Delete(id));
}