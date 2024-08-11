using Microsoft.EntityFrameworkCore;
using IronDomeV2.Models;

namespace IronDomeV2.Data
{
    public class IronDomeV2Context : DbContext
    {
        public IronDomeV2Context (DbContextOptions<IronDomeV2Context> options)
            : base(options)
        {
        }


        public DbSet<IronDomeV2.Models.Attacker> Attacker { get; set; } = default!;
        public DbSet<IronDomeV2.Models.MethodOfAttack> MethodOfAttack { get; set; } = default!;
        public DbSet<IronDomeV2.Models.Volley> Volley { get; set; } = default!;
        public DbSet<IronDomeV2.Models.Defender> Defender { get; set; } = default!;
    }
}
