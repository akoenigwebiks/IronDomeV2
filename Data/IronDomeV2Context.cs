using Microsoft.EntityFrameworkCore;
using IronDomeV2.Models;
using System;

namespace IronDomeV2.Data
{
    public class IronDomeV2Context : DbContext
    {
        public IronDomeV2Context (DbContextOptions<IronDomeV2Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MethodOfAttackTemplate to MethodOfAttack relationship
            modelBuilder.Entity<MethodOfAttackTemplate>()
                .HasMany(moat => moat.MethodOfAttacks)
                .WithOne(moa => moa.MethodOfAttackTemplate)
                .HasForeignKey(moa => moa.MethodOfAttackTemplateId)
                .OnDelete(DeleteBehavior.NoAction); // Prevents cycles or multiple cascade paths

            // Volley to MethodOfAttack relationship
            modelBuilder.Entity<Volley>()
                .HasMany(v => v.MethodsOfAttacks)
                .WithOne(moa => moa.Volley)
                .HasForeignKey(moa => moa.VolleyId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure that deleting a Volley will cascade delete its MethodOfAttacks
        }


        public DbSet<IronDomeV2.Models.Attacker> Attacker { get; set; } = default!;
        public DbSet<IronDomeV2.Models.MethodOfAttack> MethodOfAttack { get; set; } = default!;
        public DbSet<IronDomeV2.Models.Volley> Volley { get; set; } = default!;
        public DbSet<IronDomeV2.Models.Defender> Defender { get; set; } = default!;
    }
}
