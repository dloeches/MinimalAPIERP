using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ERP;

[Index("OrderId", Name = "IX_OrderId")]
[Index("ProductId", Name = "IX_ProductId")]
[Index("OrderDetailGuid", Name="IX_OrderDetailGuid", IsUnique = true)]
public record OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid OrderDetailGuid { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Count { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderDetails")]
    public virtual Product Product { get; set; } = null!;
}