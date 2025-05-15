using Data.Services;
using Email;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;


public class UserRepository : Repository<User>
{
    private readonly ApplicationDbContext _context;
    private readonly IMensajeCorreo _mensaje;
    private readonly IMensajeTelegram _mensajeTelegram;

    public UserRepository(ApplicationDbContext context, IMensajeCorreo mensaje, IMensajeTelegram mensajeTelegram) : base(context)
    {
        _context = context;
        _mensaje = mensaje;
        _mensajeTelegram = mensajeTelegram;
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

    public async Task<User?> getByEmail(string email)
    {
        return await _context.user.FirstOrDefaultAsync(u => u.email == email);
    }

    public async Task NotificarPorCorreo(string email)
    {
        string asunto = "Notificación importante";
        string contenido = "<h1>Hola, esto es un correo de prueba</h1>";

        await _mensaje.EnviarAsync(email, asunto, contenido);
    }

    public async Task NotificarPorTelegram(string texto)
    {
        await _mensajeTelegram.EnviarTelegram(texto);
    }

}
