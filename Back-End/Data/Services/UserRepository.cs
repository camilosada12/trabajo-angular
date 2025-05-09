using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

public class UserRepository : Repository<User>
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAllWithPersonAsync()
    {
        return await _dbSet
            .Include(u => u.person)
            .Where(u => EF.Property<bool>(u, "isdeleted") == false)
            .Select(u => new UserDto
            {
                id = u.id,
                UserName = u.username,
                Email = u.email,
                Password = u.password,
                PersonId = u.personid,
                PersonName = u.person.firstname + " " + u.person.lastname
            })
            .ToListAsync();
    }

    public async Task<User> validacionUser(LoginRequestDto dto)
    {
        bool sucess = false;

        var user = await _context.Set<User>().FirstOrDefaultAsync(u =>
            u.username == dto.UserName &&
            u.password == dto.Password 
        );

        sucess = (user != null) ? true : throw new UnauthorizedAccessException("credenciales Incorrectas");

        return user;
    }
}
