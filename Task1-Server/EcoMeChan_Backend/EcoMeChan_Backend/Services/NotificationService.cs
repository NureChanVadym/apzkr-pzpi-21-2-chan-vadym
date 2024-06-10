// NotificationService.cs


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
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationService(INotificationRepository notificationRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<NotificationViewModel> CreateAsync(NotificationCreateViewModel notificationCreateViewModel)
        {

            if (notificationCreateViewModel.CreatedAt.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Notification date must be in UTC.");
            }

            var notification = _mapper.Map<Notification>(notificationCreateViewModel);
            var createdNotification = await _notificationRepository.CreateAsync(notification);
            return _mapper.Map<NotificationViewModel>(createdNotification);
        }

        public async Task<NotificationViewModel> GetAsync(int notificationId)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.User && currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var notification = await _notificationRepository.GetAsync(notificationId);
            Validate(notification);
            return _mapper.Map<NotificationViewModel>(notification);
        }

        public async Task<IEnumerable<NotificationViewModel>> GetAllAsync()
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var notifications = await _notificationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<NotificationViewModel>>(notifications);
        }

        public async Task<IEnumerable<NotificationViewModel>> GetByUserIdAsync(int userId)
        {
            //var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            //if (currentUserRole != Role.User && currentUserRole != Role.Admin)
            //{
            //    throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            //}

            var notifications = await _notificationRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<NotificationViewModel>>(notifications);
        }

        public async Task<NotificationViewModel> UpdateAsync(int notificationId, NotificationEditViewModel notificationEditViewModel)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            if (notificationEditViewModel.CreatedAt.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Notification date must be in UTC.");
            }

            var notification = await _notificationRepository.GetAsync(notificationId);
            Validate(notification);

            if (notificationEditViewModel.Id != notificationId)
            {
                throw new ArgumentException("The provided notification ID does not match the notification being updated.");
            }

            _mapper.Map(notificationEditViewModel, notification);
            await _notificationRepository.UpdateAsync(notification);
            return _mapper.Map<NotificationViewModel>(notification);
        }

        public async Task DeleteAsync(int notificationId)
        {
            var currentUserRole = UserAccessUtil.GetCurrentUserRole(_httpContextAccessor);
            if (currentUserRole != Role.Admin && currentUserRole != Role.MunicipalResourceManager)
            {
                throw new UnauthorizedAccessException("Insufficient rights to perform this action.");
            }

            var notification = await _notificationRepository.GetAsync(notificationId);
            Validate(notification);
            await _notificationRepository.DeleteAsync(notificationId);
        }

        private void Validate(Notification notification)
        {
            if (notification == null)
            {
                throw new InvalidOperationException("Notification not found.");
            }
        }
    }
}