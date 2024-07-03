using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ERP;

[Index("CategoryGuid", Name = "IX_CategoryGuid", IsUnique = true)]
public record Category
{
    [Key]
    public int CategoryId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CategoryGuid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}