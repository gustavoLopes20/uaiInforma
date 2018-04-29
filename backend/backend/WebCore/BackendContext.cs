using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.WebCore
{
    public class BackendContext : DbContext
    {
        public BackendContext(DbContextOptions<BackendContext> options) : base(options)
        { }

        public DbSet<Pessoas> Pessoas { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<SessoesUsuarios> SessoesUsuarios { get; set; }
        public DbSet<PermissoesUsuarios> PermissoesUsuarios { get; set; }

        public DbSet<Unidades> Unidades { get; set; }
        public DbSet<Setores> Setores { get; set; }
        public DbSet<Medicos> Medicos { get; set; }
        public DbSet<Plantoes> Plantoes { get; set; }
        public DbSet<Especialidades> Especialidades { get; set; }
        public DbSet<MedicosUnidades> MedicosUnidades { get; set; }
        public DbSet<SetoresUnidades> SetoresUnidades { get; set; }
        public DbSet<EspecialidadesUnidades> EspecialidadesUnidades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoas>()
             .HasAlternateKey(c => c.RID)
             .HasName("RID");

            modelBuilder.Entity<Usuarios>()
             .HasAlternateKey(c => c.RID)
             .HasName("RID");

            modelBuilder.Entity<SessoesUsuarios>()
             .HasAlternateKey(c => c.RID)
             .HasName("RID");

            modelBuilder.Entity<PermissoesUsuarios>()
             .HasAlternateKey(c => c.RID)
             .HasName("RID");

            modelBuilder.Entity<Unidades>()
               .HasAlternateKey(c => c.RID)
               .HasName("RID");

            modelBuilder.Entity<Setores>()
                .HasAlternateKey(c => c.RID)
                .HasName("RID");

            modelBuilder.Entity<Medicos>()
                .HasAlternateKey(c => c.RID)
                .HasName("RID");

            modelBuilder.Entity<Plantoes>()
                .HasAlternateKey(c => c.RID)
                .HasName("RID");

            modelBuilder.Entity<Especialidades>()
                .HasAlternateKey(c => c.RID)
                .HasName("RID");

            modelBuilder.Entity<MedicosUnidades>()
                .HasAlternateKey(c => c.RID)
                .HasName("RID");

            modelBuilder.Entity<EspecialidadesUnidades>()
                .HasAlternateKey(c => c.RID)
                .HasName("RID");

            modelBuilder.Entity<SetoresUnidades>()
                .HasAlternateKey(c => c.RID)
                .HasName("RID");

        }
    }
}
