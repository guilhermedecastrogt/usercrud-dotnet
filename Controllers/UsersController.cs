using Microsoft.AspNetCore.Mvc;
using UserCrud.Models;
using UserCrud.Repositories;

namespace UserCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository)
    {
        _repository = repository;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _repository.GetAllAsync();
        return Ok(users);
    }

    // GET: api/users/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        if (await _repository.EmailExistsAsync(user.Email))
        {
            return Conflict($"Já existe um usuário com o e-mail '{user.Email}'.");
        }

        user.Id = 0;
        user.CreatedAt = DateTime.UtcNow;

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/users/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest("O id da rota não corresponde ao id do usuário informado.");
        }

        if (!await _repository.ExistsAsync(id))
        {
            return NotFound();
        }

        if (await _repository.EmailExistsAsync(user.Email, excludingId: id))
        {
            return Conflict($"Já existe um usuário com o e-mail '{user.Email}'.");
        }

        _repository.Update(user);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/users/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user is null)
        {
            return NotFound();
        }

        _repository.Remove(user);
        await _repository.SaveChangesAsync();

        return NoContent();
    }
}
