using IronDomeV2.Models;

namespace IronDomeV2.ViewModels.Volleys
{
    public class VMCreateVolley
    {
        public Attacker Attacker { get; set; }
        public Volley Volley { get; set; }
        public IEnumerable<MethodOfAttack> MethodOfAttacks { get; set; }
        public IEnumerable<VMCreateMethodsOfAttack>? Methods { get; set; }
    }
}
