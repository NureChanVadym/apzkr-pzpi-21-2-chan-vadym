// ConsumptionService.cs


using AutoMapper;
using EcoMeChan.Enums;
using EcoMeChan.Models;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.Services.Strategies;
using EcoMeChan.Utils;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan_Backend.ViewModels;
using Microsoft.AspNetCore.Http;

namespace EcoMeChan.Services
{
    public class ConsumptionService : IConsumptionService
    {
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITariffRepository _tariffRepository;
        private readonly IResourceTypeRepository _resourceTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Dictionary<ResourceTypeEnum, ITariffStrategy> _tariffStrategies;

        public ConsumptionService(
            IConsumptionRepository consumptionRepository,
            IUserRepository userRepository,
            ITariffRepository tariffRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IResourceTypeRepository resourceTypeRepository)
        {
            _consumptionRepository = consumptionRepository;
            _userRepository = userRepository;
            _tariffRepository = tariffRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

            _tariffStrategies = new Dictionary<ResourceTypeEnum, ITariffStrategy>
            {
                { ResourceTypeEnum.Water, new WaterTariffStrategy() },
                { ResourceTypeEnum.Gas, new GasTariffStrategy() },
                { ResourceTypeEnum.Electricity, new ElectricityTariffStrategy() }
            };
            _resourceTypeRepository = resourceTypeRepository;
        }

        public async Task<ConsumptionViewModel> CreateAsync(ConsumptionCreateViewModel consumptionCreateViewModel)
        {

            if (consumptionCreateViewModel.Date.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Consumption date must be in UTC.");
            }

            var user = await _userRepository.GetAsync(consumptionCreateViewModel.UserId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var tariff = await _tariffRepository.GetAsync(consumptionCreateViewModel.TariffId);
            if (tariff == null)
            {
                throw new InvalidOperationException("Tariff not found.");
            }

            var resourceType = await _resourceTypeRepository.GetByIdAsync(tariff.ResourceTypeId);
            if (resourceType == null)
            {
                throw new InvalidOperationException("Resource type not found.");
            }

            var consumption = _mapper.Map<Consumption>(consumptionCreateViewModel);

            ITariffStrategy tariffStrategy;
            if (!_tariffStrategies.TryGetValue(resourceType.Type, out tariffStrategy))
            {
                throw new InvalidOperationException("Unsupported resource type.");
            }

            try
            {
                // calculates the cost of the consumed amount based on the tariff of the specified resource type
                consumption.ConsumedCost = tariffStrategy.CalculateCost(consumption.ConsumedAmount, tariff);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Invalid tariff for the specified resource type: {ex.Message}");
            }

            var createdConsumption = await _consumptionRepository.CreateAsync(consumption);
            return _mapper.Map<ConsumptionViewModel>(createdConsumption);
        }

        public async Task<IEnumerable<ConsumptionViewModel>> GetByUserIdAsync(int userId)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.User && currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var consumptions = await _consumptionRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<ConsumptionViewModel>>(consumptions);
        }

        public async Task<ConsumptionViewModel> GetAsync(int consumptionId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.User && currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var consumption = await _consumptionRepository.GetAsync(consumptionId);
            Validate(consumption);
            return _mapper.Map<ConsumptionViewModel>(consumption);
        }

        public async Task<ConsumptionViewModel> UpdateAsync(int consumptionId, ConsumptionEditViewModel consumptionEditViewModel)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.User && currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            if (consumptionEditViewModel.Date.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Consumption date must be in UTC.");
            }

            var consumption = await _consumptionRepository.GetAsync(consumptionId);
            Validate(consumption);

            if (consumptionEditViewModel.Id != consumptionId)
            {
                throw new ArgumentException("The provided consumption ID does not match the consumption being updated.");
            }

            _mapper.Map(consumptionEditViewModel, consumption);
            await _consumptionRepository.UpdateAsync(consumption);
            return _mapper.Map<ConsumptionViewModel>(consumption);
        }

        public async Task DeleteAsync(int consumptionId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var consumption = await _consumptionRepository.GetAsync(consumptionId);
            Validate(consumption);
            await _consumptionRepository.DeleteAsync(consumptionId);
        }

        private void Validate(Consumption consumption)
        {
            if (consumption == null)
            {
                throw new InvalidOperationException("Consumption not found.");
            }
        }
    }
}