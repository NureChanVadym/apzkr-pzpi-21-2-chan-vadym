// ViewModels/IoTDevicesViewModel.cs
using System.Collections.ObjectModel;
using System.Windows.Input;
using EcoMeChan_Mobile.Models;
using EcoMeChan_Mobile.Services;

namespace EcoMeChan_Mobile.ViewModels
{
    public class IoTDevicesViewModel : BaseViewModel
    {
        private readonly IoTDeviceService _ioTDeviceService;
        private AuthService authService;


        public ObservableCollection<IoTDevice> IoTDevices { get; }
        public ICommand LoadIoTDevicesCommand { get; }

        public IoTDevicesViewModel()
        {
            _ioTDeviceService = new IoTDeviceService();
            IoTDevices = new ObservableCollection<IoTDevice>();
            LoadIoTDevicesCommand = new Command(async () => await LoadIoTDevices());
            authService = new AuthService();
        }

        private async Task LoadIoTDevices()
        {
            var user = authService.GetUser();
            if (user != null)
            {
                var devices = await _ioTDeviceService.GetUserIoTDevices(user.Id);
                IoTDevices.Clear();
                foreach (var device in devices)
                {
                    IoTDevices.Add(device);
                }
            }
        }
    }
}