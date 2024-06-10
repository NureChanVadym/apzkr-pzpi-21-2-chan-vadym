// UserController.cs

using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan.ViewModels.Extended;
using EcoMeChan_Backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateViewModel userCreateViewModel)
    {
        try
        {
            var createdUser = await _userService.CreateAsync(userCreateViewModel);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
    {
        try
        {
            var user = await _userService.AuthenticateAsync(loginViewModel.Login, loginViewModel.Password);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserExtendedViewModel>> Get(int id)
    {
        try
        {
            var user = await _userService.GetAsync(id);
            return Ok(user);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    //[HttpGet("login/{login}")]
    //public async Task<ActionResult<UserExtendedViewModel>> GetByLogin(string login)
    //{
    //    try
    //    {
    //        var user = await _userService.GetByLoginAsync(login);
    //        return Ok(user);
    //    }
    //    catch (InvalidOperationException ex)
    //    {
    //        return NotFound(ex.Message);
    //    }
    //    catch (Exception ex)
    //    {
    //        return StatusCode(500, ex.Message);
    //    }
    //}

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAll()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UserEditViewModel userEditViewModel)
    {
        try
        {
            await _userService.UpdateAsync(id, userEditViewModel);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            _userService.Logout();
            return Ok("Logged out successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("hash-password")]
    public IActionResult HashPassword([FromBody] string intputPassword)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(intputPassword))
            {
                return BadRequest("Password is required.");
            }

            var hashedPassword = _userService.HashPassword(intputPassword);
            return Ok(new { HashedPassword = hashedPassword });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}