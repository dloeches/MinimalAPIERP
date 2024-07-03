using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ERP;

[Index("StoreGuid", Name = "IX_StoreGuid", IsUnique = true)]
public record Store
{
    [Key]
    public int StoreId { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid StoreGuid { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Store")]
    public virtual ICollection<Raincheck> Rainchecks { get; set; } = new List<Raincheck>();
}
