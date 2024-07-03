using System.ComponentModel.DataAnnotations;

namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
    }
}