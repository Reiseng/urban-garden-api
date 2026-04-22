using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    public class CropTypeService : ICropTypeService
    {
        private readonly ICropTypeRepository _repository;

        public CropTypeService(ICropTypeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CropType> GetAll()
        {
            return _repository.GetAll();
        }

        public CropType? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(CropType cropType)
        {
            _repository.Add(cropType);
        }

        public void Update(CropType cropType)
        {
            if (_repository.GetById(cropType.ID) == null) throw new KeyNotFoundException("CropType not found");
            _repository.Update(cropType);
        }

        public void Delete(int id)
        {
            if (_repository.GetById(id) == null) throw new KeyNotFoundException("CropType not found");
            _repository.Delete(id);
        }
    }
}