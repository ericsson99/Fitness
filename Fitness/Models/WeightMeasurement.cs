using System.ComponentModel.DataAnnotations;

namespace Fitness.Models;

public class WeightMeasurement
{
    public int Id { get; set; }
    [Required]
    public DateOnly Date { get; set; }
    [Required]
    public float Weight { get; set; }
}