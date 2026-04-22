using UrbanGarden.Api.Models.Entities;
public interface ICropTypeRepository
{
    IEnumerable<CropType> GetAll();
    CropType? GetById(int id);
    void Add(CropType cropType);
    void Update(CropType cropType);
    void Delete(int id);
}