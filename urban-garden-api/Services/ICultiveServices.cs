using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    public interface ICultiveService
    {
        IEnumerable<Cultive> GetAll();
        Cultive? GetById(int id);
        void Add(Cultive cultive);
        void Update(Cultive cultive);
        void Delete(int id);
    }
}