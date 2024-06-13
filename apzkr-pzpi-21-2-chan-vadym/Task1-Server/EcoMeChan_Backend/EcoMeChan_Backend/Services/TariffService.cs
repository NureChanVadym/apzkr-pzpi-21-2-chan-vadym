// TariffService.cs


using AutoMapper;
using EcoMeChan.Models;
using EcoMeChan.Enums;
using EcoMeChan.Repositories.Interfaces;
using EcoMeChan.Services.Interfaces;
using EcoMeChan.ViewModels;
using EcoMeChan.ViewModels.Create;
using EcoMeChan.ViewModels.Edit;
using EcoMeChan.Utils;
using Microsoft.AspNetCore.Http;
using System.Security.AccessControl;

namespace EcoMeChan.Services
{
    public class TariffService : ITariffService
    {
        private readonly ITariffRepository _tariffRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TariffService(ITariffRepository tariffRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _tariffRepository = tariffRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TariffViewModel> CreateAsync(TariffCreateViewModel tariffCreateViewModel)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            Validate(_mapper.Map<TariffViewModel>(tariffCreateViewModel));

            var tariff = _mapper.Map<Tariff>(tariffCreateViewModel);
            var createdTariff = await _tariffRepository.CreateAsync(tariff);
            return _mapper.Map<TariffViewModel>(createdTariff);
        }

        public async Task<TariffViewModel> GetAsync(int tariffId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager && currentUserRole != Role.User)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var tariff = await _tariffRepository.GetAsync(tariffId);
            Validate(tariff);
            return _mapper.Map<TariffViewModel>(tariff);
        }

        public async Task<IEnumerable<TariffViewModel>> GetAllAsync()
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager && currentUserRole != Role.User)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var tariffs = await _tariffRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TariffViewModel>>(tariffs);
        }

        public async Task<IEnumerable<TariffViewModel>> GetByResourceTypeIdAsync(int resourceTypeId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager && currentUserRole != Role.User)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var tariffs = await _tariffRepository.GetByResourceTypeIdAsync(resourceTypeId);
            return _mapper.Map<IEnumerable<TariffViewModel>>(tariffs);
        }

        public async Task<TariffViewModel> UpdateAsync(int tariffId, TariffEditViewModel tariffEditViewModel)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            Validate(_mapper.Map<TariffViewModel>(tariffEditViewModel));

            var tariff = await _tariffRepository.GetAsync(tariffId);
            Validate(tariff);

            if (tariffEditViewModel.Id != tariffId)
            {
                throw new ArgumentException("The provided tariff ID does not match the tariff being updated.");
            }

            _mapper.Map(tariffEditViewModel, tariff);
            await _tariffRepository.UpdateAsync(tariff);
            return _mapper.Map<TariffViewModel>(tariff);
        }

        public async Task DeleteAsync(int tariffId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var tariff = await _tariffRepository.GetAsync(tariffId);
            Validate(tariff);
            await _tariffRepository.DeleteAsync(tariffId);
        }

        private void Validate(Tariff tariff)
        {
            if (tariff == null)
            {
                throw new InvalidOperationException("Tariff not found.");
            }
        }

        private void Validate(TariffViewModel viewModel)
        {
            var validationErrors = new List<string>();

            if (viewModel == null)
            {
                validationErrors.Add("Tariff data cannot be null.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(viewModel.Name))
                {
                    validationErrors.Add("Tariff name is required.");
                }

                if (viewModel.StartDate.Kind != DateTimeKind.Utc || viewModel.EndDate.Kind != DateTimeKind.Utc)
                {
                    validationErrors.Add("Tariff start and end dates must be in UTC.");
                }

                if (viewModel.StartDate >= viewModel.EndDate)
                {
                    validationErrors.Add("Tariff start date must be before the end date.");
                }

                if (viewModel.PricePerUnit <= 0)
                {
                    validationErrors.Add("Price per unit must be greater than zero.");
                }
            }

            if (validationErrors.Any())
            {
                var errorMessage = string.Join(Environment.NewLine, validationErrors);
                throw new ArgumentException(errorMessage);
            }
        }
    }
}