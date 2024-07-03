using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ERP;

[Index("ProductId", Name = "IX_ProductId")]
[Index("StoreId", Name = "IX_StoreId")]
[Index("RaincheckGuid", Name = "IX_RaincheckGuid", IsUnique = true)]
public record Raincheck
{
    [Key]
    public int RaincheckId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid RaincheckGuid { get; set; }
    public string? Name { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    public double SalePrice { get; set; }

    public int StoreId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Rainchecks")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("StoreId")]
    [InverseProperty("Rainchecks")]
    public virtual Store Store { get; set; } = null!;
}