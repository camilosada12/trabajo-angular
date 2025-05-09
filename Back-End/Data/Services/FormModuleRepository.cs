using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity.Context;
using Entity.DTOs;
using Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class FormModuleRepository : Repository<FormModule>
    {
        private readonly ApplicationDbContext _context;
        public FormModuleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<FormModuleDto>> GetAllJoinAsync()
        {
            return await _dbSet
                .Include(x => x.Form)
                .Include(x => x.Module)
                .Where(e => EF.Property<bool>(e, "isdeleted") == false)
                .Select(ru => new FormModuleDto
                {
                    id = ru.id,
                    formid = ru.formid,
                    moduleid = ru.moduleid,
                    formname = ru.Form.name,
                    modulename = ru.Module.name
                })
                .ToListAsync();
        }

    }
}
