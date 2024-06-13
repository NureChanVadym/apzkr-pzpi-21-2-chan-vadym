// ViewModels/MonitoringViewModel.cs
using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Resources.Languages;
using EcoMeChan_Mobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

namespace EcoMeChan_Mobile.ViewModels
{
    public class MonitoringViewModel : BaseViewModel
    {
        private readonly NotificationService _notificationService;

        public ObservableCollection<Notification> Notifications { get; }
        public ICommand LoadNotificationsCommand { get; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string DateTimeFormat { get; set; }

        public MonitoringViewModel()
        {
            _notificationService = new NotificationService();
            Notifications = new ObservableCollection<Notification>();
            LoadNotificationsCommand = new Command(async () => await LoadNotifications());
            SortColumn = null;
            SortOrder = "asc";
            DateTimeFormat = GetDateTimeFormat();
        }

        private async Task LoadNotifications()
        {
            var notifications = await _notificationService.GetAllNotifications();
            Notifications.Clear();
            foreach (var notification in notifications)
            {
                notification.FormattedCreatedAt = FormatDateTime(notification.CreatedAt);
                Notifications.Add(notification);
            }
        }

        public string GetNotificationType(NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Normal:
                    return AppResources.NotificationTypeNormal;
                case NotificationType.Warning:
                    return AppResources.NotificationTypeWarning;
                case NotificationType.Critical:
                    return AppResources.NotificationTypeCritical;
                default:
                    return AppResources.NotificationTypeUnknown;
            }
        }

        public void SortNotifications(string column)
        {
            if (column == SortColumn)
            {
                SortOrder = SortOrder == "asc" ? "desc" : "asc";
            }
            else
            {
                SortColumn = column;
                SortOrder = "asc";
            }

            var sortedNotifications = SortOrder == "asc"
                ? Notifications.OrderBy(n => GetPropertyValue(n, column))
                : Notifications.OrderByDescending(n => GetPropertyValue(n, column));

            Notifications.Clear();
            foreach (var notification in sortedNotifications)
            {
                Notifications.Add(notification);
            }
        }

        private object GetPropertyValue(Notification notification, string propertyName)
        {
            var property = typeof(Notification).GetProperty(propertyName);
            return property?.GetValue(notification);
        }

        private string FormatDateTime(DateTime dateTime)
        {
            return dateTime.ToString(DateTimeFormat);
        }

        private string GetDateTimeFormat()
        {
            var culture = new CultureInfo(AppResources.Culture.Name);
            return culture.Name == "uk" ? "dd/MM/yyyy HH:mm:ss" : "MM/dd/yyyy h:mm:ss tt";
        }

        public void UpdateDateTimeFormat()
        {
            DateTimeFormat = GetDateTimeFormat();
            foreach (var notification in Notifications)
            {
                notification.FormattedCreatedAt = FormatDateTime(notification.CreatedAt);
            }
        }
    }
}