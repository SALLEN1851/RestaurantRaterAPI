using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantRaterDotAPI.Data;

public class Restaurant 
{
    [Key] 
    public int Id { get; set; }

    [Required] 
    [MaxLength(100)] 
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(100)] 
    public string Location { get; set; } = string.Empty;

    public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
    public double AverageRating
    {
        get
        {
            if (Ratings.Count == 0)
            {
                return 0;
            }
            double total = 0.0;
            foreach (Rating rating in Ratings)
            {
                total += rating.Score;
            }
            return total / Ratings.Count;
        }
    }
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