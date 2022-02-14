using CargaDeMedicamentosAPI.Entities;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CargaDeMedicamentosAPI.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<Eaction> Eactions { get; set; }
        public virtual DbSet<EactionRole> EactionRoles { get; set; }
        public virtual DbSet<Ecarga> Ecargas { get; set; }
        public virtual DbSet<EcargaItemFallido> EcargaItemFallidos { get; set; }
        public virtual DbSet<Efarmacium> Efarmacia { get; set; }
        public virtual DbSet<EhistorialPrecio> EhistorialPrecios { get; set; }
        public virtual DbSet<Emedicamento> Emedicamentos { get; set; }
        public virtual DbSet<EpersonFarmacia> EpersonFarmacias { get; set; }
        public virtual DbSet<EprecioFarmacium> EprecioFarmacia { get; set; }
        public virtual DbSet<LogLogging> LogLoggings { get; set; }
        public virtual DbSet<LogTransaction> LogTransactions { get; set; }
        public virtual DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=172.20.16.55;Initial Catalog=comparador;Persist Security Info=true;User ID=user_comparador;Password=z2CkEbN5;MultipleActiveResultSets=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Eaction>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EAction");

                entity.Property(e => e.Group)
                    .HasMaxLength(100)
                    .HasColumnName("group");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Order).HasColumnName("order");
            });

            modelBuilder.Entity<EactionRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EActionRole");

                entity.Property(e => e.IdRole)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Ecarga>(entity =>
            {
                entity.ToTable("Ecarga");

                entity.HasIndex(e => e.CodFarmacia, "IX_Ecarga_cod_farmacia");

                entity.HasIndex(e => e.PersonId, "IX_Ecarga_personId");

                entity.Property(e => e.CodFarmacia)
                    .HasMaxLength(20)
                    .HasColumnName("cod_farmacia");

                entity.Property(e => e.DirectorioArchivo).HasColumnName("directorio_archivo");

                entity.Property(e => e.EstadoCarga).HasColumnName("estado_carga");

                entity.Property(e => e.FechaCargaFin).HasColumnName("fecha_carga_fin");

                entity.Property(e => e.FechaCargaInicio).HasColumnName("fecha_carga_inicio");

                entity.Property(e => e.FechaCreacion).HasColumnName("fecha_creacion");

                entity.Property(e => e.NombreArchivo).HasColumnName("nombre_archivo");

                entity.Property(e => e.ObservacionesCarga).HasColumnName("observaciones_carga");

                entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.Property(e => e.RutFarmaciaCarga).HasColumnName("rut_farmacia_carga");

                entity.Property(e => e.TipoAccion).HasColumnName("tipo_accion");

                entity.Property(e => e.TotalRegistros).HasColumnName("total_registros");

                entity.Property(e => e.TotalRegistrosCorrectos).HasColumnName("total_registros_correctos");

                entity.Property(e => e.TotalRegistrosErroneos).HasColumnName("total_registros_erroneos");

                entity.HasOne(d => d.CodFarmaciaNavigation)
                    .WithMany(p => p.Ecargas)
                    .HasForeignKey(d => d.CodFarmacia);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Ecargas)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<EcargaItemFallido>(entity =>
            {
                entity.ToTable("ECargaItemFallido");

                entity.HasIndex(e => e.EcargaId, "IX_ECargaItemFallido_EcargaId");

                entity.Property(e => e.CodigoBarra).HasColumnName("codigo_barra");

                entity.Property(e => e.CodigoInterno).HasColumnName("codigo_interno");

                entity.Property(e => e.CodigoTfc).HasColumnName("codigo_tfc");

                entity.Property(e => e.DescError).HasColumnName("desc_error");

                entity.Property(e => e.DescripcionInterna).HasColumnName("descripcion_interna");

                entity.Property(e => e.NuevoPrecio).HasColumnName("nuevo_precio");

                entity.Property(e => e.StockActual).HasColumnName("stock_actual");

                entity.HasOne(d => d.Ecarga)
                    .WithMany(p => p.EcargaItemFallidos)
                    .HasForeignKey(d => d.EcargaId);
            });

            modelBuilder.Entity<Efarmacium>(entity =>
            {
                entity.HasKey(e => e.Codigo);

                entity.ToTable("EFarmacia");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(20)
                    .HasColumnName("codigo");

                entity.Property(e => e.Alopatica)
                    .HasMaxLength(2)
                    .HasColumnName("alopatica");

                entity.Property(e => e.Comuna)
                    .HasMaxLength(50)
                    .HasColumnName("comuna");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(150)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado)
                    .HasMaxLength(2)
                    .HasColumnName("estado");

                entity.Property(e => e.Homeopatica)
                    .HasMaxLength(2)
                    .HasColumnName("homeopatica");

                entity.Property(e => e.Latitud).HasColumnName("latitud");

                entity.Property(e => e.Longitud).HasColumnName("longitud");

                entity.Property(e => e.Movil)
                    .HasMaxLength(2)
                    .HasColumnName("movil");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreFantasia)
                    .HasMaxLength(140)
                    .HasColumnName("nombre_fantasia");

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(150)
                    .HasColumnName("razon_social");

                entity.Property(e => e.Region)
                    .HasMaxLength(50)
                    .HasColumnName("region");

                entity.Property(e => e.Rut)
                    .HasMaxLength(13)
                    .HasColumnName("rut");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .HasColumnName("telefono");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(80)
                    .HasColumnName("tipo");
            });

            modelBuilder.Entity<EhistorialPrecio>(entity =>
            {
                entity.ToTable("EHistorialPrecio");

                entity.Property(e => e.CodFarmacia)
                    .HasMaxLength(20)
                    .HasColumnName("cod_farmacia");

                entity.Property(e => e.CodGtin)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cod_gtin");

                entity.Property(e => e.CodIsp)
                    .HasMaxLength(50)
                    .HasColumnName("cod_isp");

                entity.Property(e => e.CodTfc)
                    .HasMaxLength(16)
                    .HasColumnName("cod_tfc");

                entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion");

                entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.PrecioUnidad)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio_unidad");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.StockMax).HasColumnName("stock_max");

                entity.Property(e => e.StockMin).HasColumnName("stock_min");

                entity.Property(e => e.TipoActualizacion)
                    .HasMaxLength(2)
                    .HasColumnName("tipo_actualizacion");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.EhistorialPrecios)
                    .HasForeignKey(d => d.PersonId);

                entity.HasOne(d => d.Cod)
                    .WithMany(p => p.EhistorialPrecios)
                    .HasForeignKey(d => new { d.CodTfc, d.CodFarmacia });
            });

            modelBuilder.Entity<Emedicamento>(entity =>
            {
                entity.HasKey(e => e.CodTfc);

                entity.ToTable("EMedicamento");

                entity.Property(e => e.CodTfc)
                    .HasMaxLength(16)
                    .HasColumnName("cod_tfc");

                entity.Property(e => e.Bioequivalente)
                    .HasMaxLength(2)
                    .HasColumnName("bioequivalente");

                entity.Property(e => e.CodGtin)
                    .HasMaxLength(50)
                    .HasColumnName("cod_gtin");

                entity.Property(e => e.CodIsp)
                    .HasMaxLength(50)
                    .HasColumnName("cod_isp");

                entity.Property(e => e.Estado)
                    .HasMaxLength(100)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion");

                entity.Property(e => e.Laboratorio)
                    .HasMaxLength(150)
                    .HasColumnName("laboratorio");

                entity.Property(e => e.Marca)
                    .HasMaxLength(150)
                    .HasColumnName("marca");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(150)
                    .HasColumnName("nombre");

                entity.Property(e => e.Presentacion)
                    .HasMaxLength(150)
                    .HasColumnName("presentacion");

                entity.Property(e => e.Unidades).HasColumnName("unidades");
            });

            modelBuilder.Entity<EpersonFarmacia>(entity =>
            {
                entity.HasKey(e => new { e.CodFarmacia, e.PersonId });

                entity.ToTable("EPersonFarmacias");

                entity.HasIndex(e => e.PersonId, "IX_EPersonFarmacias_personId");

                entity.Property(e => e.CodFarmacia)
                    .HasMaxLength(20)
                    .HasColumnName("cod_farmacia");

                entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.HasOne(d => d.CodFarmaciaNavigation)
                    .WithMany(p => p.EpersonFarmacia)
                    .HasForeignKey(d => d.CodFarmacia);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.EpersonFarmacia)
                    .HasForeignKey(d => d.PersonId);
            });

            modelBuilder.Entity<EprecioFarmacium>(entity =>
            {
                entity.HasKey(e => new { e.CodTfc, e.CodFarmacia });

                entity.ToTable("EPrecioFarmacia");

                entity.HasIndex(e => e.CodFarmacia, "IX_EPrecioFarmacia_cod_farmacia");

                entity.Property(e => e.CodTfc)
                    .HasMaxLength(16)
                    .HasColumnName("cod_tfc");

                entity.Property(e => e.CodFarmacia)
                    .HasMaxLength(20)
                    .HasColumnName("cod_farmacia");

                entity.Property(e => e.CodigoBarraFramacia).HasColumnName("codigo_barra_framacia");

                entity.Property(e => e.CodigoInternoFarmacia).HasColumnName("codigo_interno_farmacia");

                entity.Property(e => e.DescripcionInternaFarmacia).HasColumnName("descripcion_interna_farmacia");

                entity.Property(e => e.FechaActualizacion).HasColumnName("fecha_actualizacion");

                entity.Property(e => e.FechaActualizacionStock).HasColumnName("fecha_actualizacion_stock");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("precio");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.StockMax).HasColumnName("stock_max");

                entity.Property(e => e.StockMin).HasColumnName("stock_min");

                entity.HasOne(d => d.CodFarmaciaNavigation)
                    .WithMany(p => p.EprecioFarmacia)
                    .HasForeignKey(d => d.CodFarmacia);

                entity.HasOne(d => d.CodTfcNavigation)
                    .WithMany(p => p.EprecioFarmacia)
                    .HasForeignKey(d => d.CodTfc);
            });

            modelBuilder.Entity<LogLogging>(entity =>
            {
                entity.ToTable("LogLogging");

                entity.Property(e => e.CloseSession).HasColumnName("closeSession");

                entity.Property(e => e.IpAddress).HasColumnName("ipAddress");

                entity.Property(e => e.OpenSession).HasColumnName("openSession");

                entity.Property(e => e.Rut)
                    .HasMaxLength(10)
                    .HasColumnName("rut");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<LogTransaction>(entity =>
            {
                entity.ToTable("LogTransaction");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.IdPk)
                    .HasMaxLength(180)
                    .HasColumnName("idPk");

                entity.Property(e => e.IpAddess)
                    .HasMaxLength(80)
                    .HasColumnName("ipAddess");

                entity.Property(e => e.TableName)
                    .HasMaxLength(80)
                    .HasColumnName("tableName");

                entity.Property(e => e.TypeTransaction)
                    .HasMaxLength(1)
                    .HasColumnName("typeTransaction");

                entity.Property(e => e.UserTransaction)
                    .HasMaxLength(80)
                    .HasColumnName("userTransaction");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.HasIndex(e => e.UserId, "IX_Person_userId");

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("email");

                entity.Property(e => e.IsAdminLocal).HasColumnName("isAdminLocal");

                entity.Property(e => e.IsRepresentantelegal).HasColumnName("isRepresentantelegal");

                entity.Property(e => e.IsSuperAdmin).HasColumnName("isSuperAdmin");

                entity.Property(e => e.IsUsuarioNormal).HasColumnName("isUsuarioNormal");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastName");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Rut)
                    .IsRequired()
                    .HasMaxLength(9)
                    .HasColumnName("rut");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasMaxLength(36)
                    .HasColumnName("uid");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
