// ViewModels/Edit/ResourceTypeEditViewModel.cs
using EcoMeChan.Models;

namespace EcoMeChan.ViewModels.Edit
{
    public class ResourceTypeEditViewModel
    {
        public int Id { get; set; }
        public ResourceTypeEnum Type { get; set; }
        public string Unit { get; set; }
    }
}