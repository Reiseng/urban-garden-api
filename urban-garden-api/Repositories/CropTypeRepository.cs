using UrbanGarden.Api.Data;
using UrbanGarden.Api.Models.Entities;

namespace UrbanGarden.Api.Repositories
{
    public class CropTypeRepository : ICropTypeRepository
    {
        private readonly UrbanGardenDbContext _context;

        public CropTypeRepository(UrbanGardenDbContext context)
        {
            _context = context;
        }


        public IEnumerable<CropType> GetAll()
        {
            return _context.CropTypes.ToList();
        }


        public CropType? GetById(int id)
        {
            return _context.CropTypes
                .FirstOrDefault(c => c.ID == id);
        }


        public void Add(CropType cropType)
        {
            _context.CropTypes.Add(cropType);
            _context.SaveChanges();
        }


        public void Update(CropType cropType)
        {
            var existing = GetById(cropType.ID);

            if(existing == null)
                return;


            existing.Name = cropType.Name;
            existing.Season = cropType.Season;
            existing.IsPerennial = cropType.IsPerennial;
            existing.Disponible = cropType.Disponible;

            _context.SaveChanges();
        }


        public void Delete(int id)
        {
            var cropType = GetById(id);

            if(cropType == null)
                return;


            _context.CropTypes.Remove(cropType);
            _context.SaveChanges();
        }
    }
}