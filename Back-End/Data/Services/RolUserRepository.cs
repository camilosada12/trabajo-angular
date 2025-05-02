using Data.Services;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

public class RolUserRepository : Repository<RolUser>
{
    public RolUserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<RolUserDto>> GetAllJoinAsync()
    {
        return await _dbSet
            .Include(x => x.User)
            .Include(x => x.Rol)
            .Where(e => EF.Property<bool>(e, "isdeleted") == false)
            .Select(ru => new RolUserDto
            {
                Id = ru.id,
                UserId = ru.userid,
                RolId = ru.rolid,
                UserName = ru.User.username,
                RolName = ru.Rol.name
            })
            .ToListAsync();
    }

    // ✅ Método para obtener el nombre del rol de un usuario
    public async Task<string?> GetRolNameByUserId(int userId)
    {
        var rolUser = await _dbSet
            .Include(ru => ru.Rol)
            .FirstOrDefaultAsync(ru => ru.userid == userId && !ru.isdeleted);

        return rolUser?.Rol?.name;
    }

    public async Task AsignarRol(int userId, int rolId)
    {
        var nuevaRelacion = new RolUser
        {
            userid = userId,
            rolid = rolId
        };

        _context.roluser.Add(nuevaRelacion); // Usa tu contexto real
        await _context.SaveChangesAsync();
    }
}

