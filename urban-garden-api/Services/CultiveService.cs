using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    public class CultiveService : ICultiveService
    {
        private readonly ICultiveRepository _repository;

        public CultiveService(ICultiveRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Cultive> GetAll()
        {
            return _repository.GetAll();
        }

        public Cultive? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(Cultive cultive)
        {
            _repository.Add(cultive);
        }

        public void Update(Cultive cultive)
        {
            if (_repository.GetById(cultive.ID) == null) throw new Exception("Cultive not found");
            _repository.Update(cultive);
        }

        public void Delete(int id)
        {
            if (_repository.GetById(id) == null) throw new Exception("Cultive not found");
            _repository.Delete(id);
        }
    }
}