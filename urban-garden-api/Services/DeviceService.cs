using UrbanGarden.Api.Models.Entities;
using UrbanGarden.Api.Models.Dtos;
using UrbanGarden.Api.Repositories;
using UrbanGarden.Api.Infrastructure;

namespace UrbanGarden.Api.Services
{
    /// <summary>
    /// Implementación del servicio de dispositivos.
    /// </summary>
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;

        public DeviceService(IDeviceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Device> GetAll()
        {
            return _repository.GetAll();
        }

        public Device? GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public Device? GetByMacAddress(string macAddress)
        {
            return _repository.GetByMacAddress(macAddress);
        }

        public CreateDeviceDto Add(RegisterDeviceDto device)
        {
            if (string.IsNullOrWhiteSpace(device.MacAddress) || string.IsNullOrWhiteSpace(device.Name) || string.IsNullOrWhiteSpace(device.RegistrationKey))
            {
                throw new ArgumentException("Todos los campos son obligatorios");
            }
            if (device.RegistrationKey != "SECRET_KEY")
            {
                throw new UnauthorizedAccessException("Clave de registro inválida");
            }
            var existingDevice = _repository.GetByMacAddress(device.MacAddress);
            if (existingDevice != null)
            {
                return new CreateDeviceDto { ApiKey = existingDevice.ApiKey, ID = existingDevice.ID };
            }
            var newDevice = new Device
            {
                Name = device.Name,
                MacAddress = device.MacAddress,
                ApiKey = ApiKeyGenerator.Generate()
            };
            var addedDevice = _repository.Add(newDevice);
            return new CreateDeviceDto { ApiKey = addedDevice?.ApiKey ?? string.Empty, ID = addedDevice?.ID ?? Guid.Empty };
        }
        public void Update(Guid id, UpdateDeviceDto dto)
        {
            var existing = _repository.GetById(id);
            if (existing == null) return;

            existing.Name = dto.Name;

            _repository.Update(existing);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}