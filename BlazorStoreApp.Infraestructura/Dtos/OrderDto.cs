using System.ComponentModel.DataAnnotations;

namespace BlazorStoreApp.Infraestructura.Dtos
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? State { get; set; }
        [Required]
        public string? PostalCode { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Email { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
    }
}