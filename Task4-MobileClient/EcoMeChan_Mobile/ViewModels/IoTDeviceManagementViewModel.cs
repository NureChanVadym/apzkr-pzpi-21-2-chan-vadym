// ViewModels/IoTDeviceManagementViewModel.cs
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Services;

namespace EcoMeChan_Mobile.ViewModels
{
    public class IoTDeviceManagementViewModel : INotifyPropertyChanged
    {
        private readonly IoTDeviceService _iotDeviceService;
        public ObservableCollection<IoTDevice> IoTDevices { get; set; }

        public IoTDeviceManagementViewModel()
        {
            _iotDeviceService = new IoTDeviceService();
            IoTDevices = new ObservableCollection<IoTDevice>();
            LoadIoTDevices();
        }

        private async void LoadIoTDevices()
        {
            var devices = await _iotDeviceService.GetAllIoTDevicesAsync();
            if (devices != null)
            {
                IoTDevices.Clear();
                foreach (var device in devices)
                {
                    IoTDevices.Add(device);
                }
            }
        }

        public ICommand UpdateIoTDeviceCommand => new Command<IoTDevice>(async (device) =>
        {
            var isUpdateSuccessful = await _iotDeviceService.UpdateIoTDeviceAsync(device.Id, device);
            if (!isUpdateSuccessful)
            {
                // Handle update error
            }
        });

        public ICommand DeleteIoTDeviceCommand => new Command<IoTDevice>(async (device) =>
        {
            var isDeleteSuccessful = await _iotDeviceService.DeleteIoTDeviceAsync(device.Id);
            if (isDeleteSuccessful)
            {
                IoTDevices.Remove(device);
            }
            else
            {
                // Handle delete error
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}