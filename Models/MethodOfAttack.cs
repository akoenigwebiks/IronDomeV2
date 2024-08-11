using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronDomeV2.Models;

[Index(nameof(Name), IsUnique = true)]
public class MethodOfAttack
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Distance { get; set; } // Distance covered mid flight
    public MethodOfAttackTemplate MethodOfAttackTemplate { get; set; }
    public int MethodOfAttackTemplateId { get; set; }
    public DateTime? TimeOfLaunch { get; set; } // Time when the method was launched

    // Foreign key
    public int VolleyId { get; set; }
    public Volley Volley { get; set; }

    // Navigation property for countermeasure
    public Countermeasure? Countermeasure { get; set; }

    // Computed property to calculate time to destination
    [NotMapped]
    public double TimeToDestination
    {
        get
        {
            return Distance / MethodOfAttackTemplate.Velocity;
        }
    }

    [NotMapped]
    public bool HasReachedDestination
    {
        get
        {
            if (TimeOfLaunch == null) return false;
            return (DateTime.Now - TimeOfLaunch.Value).TotalSeconds >= TimeToDestination;
        }
    }

    [NotMapped]
    public bool WasAborted { get; set; } // Allows for the attack to be aborted manually

    [NotMapped]
    public bool WasIntercepted
    {
        get
        {
            return Countermeasure != null && Countermeasure.HasIntercepted;
        }
    }
}
