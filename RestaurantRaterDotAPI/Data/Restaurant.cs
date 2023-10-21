using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRaterDotAPI.Data;

public class Restaurant 
{
    [Key] // Primary Key
    public int Id { get; set; }

    [Required] // NOT NULL
    [MaxLength(100)] // NVARCHAR(100)
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)] //Attributes can go in the same brackets
    public string Location { get; set; } = string.Empty;
}

public class Rating
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Range(0, 5)]
    public double Score { get; set; }

    [Required]
    [ForeignKey("Restaurant")]
    public int RestaurantId { get; set; }
}