// IoTDeviceService.cs


using AutoMapper;
using EcoMeChan.Enums;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.Utils;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan.ViewModels.Extended;
using Microsoft.AspNetCore.Http;

namespace EcoMeChan.Services
{
    public class IoTDeviceService : IIoTDeviceService
    {
        private readonly IIoTDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IoTDeviceService(IIoTDeviceRepository deviceRepository, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IoTDeviceViewModel> CreateAsync(IoTDeviceCreateViewModel deviceCreateViewModel)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var device = _mapper.Map<IoTDevice>(deviceCreateViewModel);
            var createdDevice = await _deviceRepository.CreateAsync(device);
            return _mapper.Map<IoTDeviceViewModel>(createdDevice);
        }

        public async Task<IoTDeviceExtendedViewModel> GetAsync(int deviceId)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var device = await _deviceRepository.GetAsync(deviceId);
            Validate(device);
            return _mapper.Map<IoTDeviceExtendedViewModel>(device);
        }

        public async Task<IEnumerable<IoTDeviceViewModel>> GetByUserIdAsync(int userId)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager && currentUserRole != Role.User)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var devices = await _deviceRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<IoTDeviceViewModel>>(devices);
        }

        public async Task<IEnumerable<IoTDeviceViewModel>> GetAllAsync()
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var devices = await _deviceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<IoTDeviceViewModel>>(devices);
        }

        public async Task<IoTDeviceViewModel> UpdateAsync(int deviceId, IoTDeviceEditViewModel deviceEditViewModel)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var device = await _deviceRepository.GetAsync(deviceId);
            Validate(device);

            if (deviceEditViewModel.Id != deviceId)
            {
                throw new ArgumentException("The provided device ID does not match the device being updated.");
            }

            _mapper.Map(deviceEditViewModel, device);
            await _deviceRepository.UpdateAsync(device);
            return _mapper.Map<IoTDeviceViewModel>(device);
        }

        public async Task DeleteAsync(int deviceId)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var device = await _deviceRepository.GetAsync(deviceId);
            Validate(device);
            await _deviceRepository.DeleteAsync(deviceId);
        }

        private void Validate(IoTDevice device)
        {
            if (device == null)
            {
                throw new InvalidOperationException("IoT device not found.");
            }
        }
    }
}