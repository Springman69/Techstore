using TechStore.Models;
using System.Collections.Generic;

namespace TechStore.Repositories
{
    public interface IOpiniaRepository
    {
        Opinia GetOpinia(int id);
        IEnumerable<Opinia> GetAllOpinie();
        Task<Opinia> AddAsync(Opinia opinia);
        Opinia Update(Opinia opiniaChanges);
        void Delete(int id);
    }
}
