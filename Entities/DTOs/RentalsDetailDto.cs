using Core.Entities;

namespace Entities.DTOs
{
    public class RentalsDetailDto : IDto
    {
        public string BrandName { get; set; }
        public string FullName { get; set; }
    }
}
