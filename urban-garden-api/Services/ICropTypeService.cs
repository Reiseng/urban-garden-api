using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Services
{
    public interface ICropTypeService
    {
        IEnumerable<CropType> GetAll();
        CropType? GetById(int id);
        void Add(CropType cropType);
        void Update(CropType cropType);
        void Delete(int id);
    }
}