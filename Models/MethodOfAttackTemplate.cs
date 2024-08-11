namespace IronDomeV2.Models
{
    public class MethodOfAttackTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int Range { get; set; }
        public int Velocity { get; set; }

        public Attacker Attacker { get; set; }
        public int AttackerId { get; set; }
        public IEnumerable<MethodOfAttack> MethodOfAttacks { get; set; }
    }
}
