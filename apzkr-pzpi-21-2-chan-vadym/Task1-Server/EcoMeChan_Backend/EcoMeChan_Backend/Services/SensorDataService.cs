// SensorDataService.cs


using AutoMapper;
using EcoMeChan.Enums;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.Utils;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using Microsoft.AspNetCore.Http;

namespace EcoMeChan.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly ISensorDataRepository _sensorDataRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SensorDataService(ISensorDataRepository sensorDataRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _sensorDataRepository = sensorDataRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SensorDataViewModel> CreateAsync(SensorDataCreateViewModel sensorDataCreateViewModel)
        {

            if (sensorDataCreateViewModel.Timestamp.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Sensor date must be in UTC.");
            }

            var sensorData = _mapper.Map<SensorData>(sensorDataCreateViewModel);
            var createdSensorData = await _sensorDataRepository.CreateAsync(sensorData);
            return _mapper.Map<SensorDataViewModel>(createdSensorData);
        }

        public async Task<SensorDataViewModel> GetAsync(int sensorDataId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var sensorData = await _sensorDataRepository.GetAsync(sensorDataId);
            Validate(sensorData);
            return _mapper.Map<SensorDataViewModel>(sensorData);
        }

        public async Task<IEnumerable<SensorDataViewModel>> GetAllAsync()
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var sensorDataList = await _sensorDataRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SensorDataViewModel>>(sensorDataList);
        }

        public async Task<IEnumerable<SensorDataViewModel>> GetByDeviceIdAsync(int deviceId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var sensorDataList = await _sensorDataRepository.GetByDeviceIdAsync(deviceId);
            return _mapper.Map<IEnumerable<SensorDataViewModel>>(sensorDataList);
        }

        public async Task<SensorDataViewModel> UpdateAsync(int sensorDataId, SensorDataEditViewModel sensorDataEditViewModel)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            if (sensorDataEditViewModel.Timestamp.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Sensor date must be in UTC.");
            }

            var sensorData = await _sensorDataRepository.GetAsync(sensorDataId);
            Validate(sensorData);

            if (sensorDataEditViewModel.Id != sensorDataId)
            {
                throw new ArgumentException("The provided sensor data ID does not match the sensor data being updated.");
            }

            _mapper.Map(sensorDataEditViewModel, sensorData);
            await _sensorDataRepository.UpdateAsync(sensorData);
            return _mapper.Map<SensorDataViewModel>(sensorData);
        }

        public async Task DeleteAsync(int sensorDataId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var sensorData = await _sensorDataRepository.GetAsync(sensorDataId);
            Validate(sensorData);
            await _sensorDataRepository.DeleteAsync(sensorDataId);
        }

        private void Validate(SensorData sensorData)
        {
            if (sensorData == null)
            {
                throw new InvalidOperationException("Sensor data not found.");
            }
        }
    }
}