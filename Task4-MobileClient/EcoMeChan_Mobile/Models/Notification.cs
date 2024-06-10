// Models/Notification.cs
using EcoMeChan_Mobile.Enums;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EcoMeChan_Mobile.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int IoTDeviceId { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        private string _formattedCreatedAt;
        public string FormattedCreatedAt
        {
            get => _formattedCreatedAt;
            set
            {
                if (_formattedCreatedAt != value)
                {
                    _formattedCreatedAt = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}