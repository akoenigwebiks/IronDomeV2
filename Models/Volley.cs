namespace IronDomeV2.Models;

public class Volley
{
    public int Id { get; set; }

    // Foreign key
    public int AttackerId { get; set; }
    public Attacker Attacker { get; set; }
    public DateTime? launchDate { get; set; }
    // Navigation property
    public IEnumerable<MethodOfAttack> MethodsOfAttacks { get; set; }
}
