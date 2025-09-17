using TechStore.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TechStore.Data;

namespace TechStore.Repositories
{
    public class OpiniaRepository : IOpiniaRepository
    {
        private readonly ApplicationDbContext _context;

        public OpiniaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Opinia GetOpinia(int id)
        {
            return _context.Opinie.Find(id);
        }

        public IEnumerable<Opinia> GetAllOpinie()
        {
            return _context.Opinie.ToList();
        }

        public async Task<Opinia> AddAsync(Opinia opinia)
        {
            await _context.Opinie.AddAsync(opinia);
            await _context.SaveChangesAsync();
            return opinia;
        }

        public Opinia Update(Opinia opiniaChanges)
        {
            var opinia = _context.Opinie.Attach(opiniaChanges);
            opinia.State = EntityState.Modified;
            _context.SaveChanges();
            return opiniaChanges;
        }

        public void Delete(int id)
        {
            Opinia opinia = _context.Opinie.Find(id);
            if (opinia != null)
            {
                _context.Opinie.Remove(opinia);
                _context.SaveChanges();
            }
        }
    }
}
