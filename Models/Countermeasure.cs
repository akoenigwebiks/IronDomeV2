
using System.ComponentModel.DataAnnotations.Schema;

namespace IronDomeV2.Models;

public class Countermeasure
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Range { get; set; }

    public int DefenderId { get; set; }
    public Defender Defender { get; set; }
    public double Velocity { get; set; } // Velocity of the countermeasure
    public DateTime? LaunchTime { get; set; } // Time when the countermeasure was launched

    // Foreign key for MethodOfAttack
    public int MethodOfAttackId { get; set; }
    public MethodOfAttack MethodOfAttack { get; set; }

    // Computed property to calculate time to intercept
    [NotMapped]
    public double TimeToIntercept
    {
        get
        {
            if (MethodOfAttack == null || LaunchTime == null) return double.MaxValue;
            return Range / Velocity;
        }
    }

    // Logic to determine if the countermeasure can intercept the method of attack
    [NotMapped]
    public bool CanIntercept
    {
        get
        {
            if (MethodOfAttack == null || LaunchTime == null) return false;

            // Time when the MethodOfAttack will reach the countermeasure's range
            double timeUntilInRange = (MethodOfAttack.Distance - Range) / MethodOfAttack.MethodOfAttackTemplate.Velocity;
            double timeAfterLaunch = (DateTime.Now - LaunchTime.Value).TotalSeconds;

            // Countermeasure can intercept if launched before or exactly when the MethodOfAttack enters its range
            return timeAfterLaunch <= timeUntilInRange && timeUntilInRange <= timeAfterLaunch + TimeToIntercept;
        }
    }

    // Logic to determine if the countermeasure has intercepted the method of attack
    [NotMapped]
    public bool HasIntercepted
    {
        get
        {
            return CanIntercept && (DateTime.Now - LaunchTime.Value).TotalSeconds <= TimeToIntercept;
        }
    }
}

