// ViewModels/ResourceTypeViewModel.cs
using EcoMeChan.Models;

namespace EcoMeChan.ViewModels
{
    public class ResourceTypeViewModel
    {
        public int Id { get; set; }
        public ResourceTypeEnum Type { get; set; }
        public string Unit { get; set; }
    }
}