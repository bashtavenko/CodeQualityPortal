using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CodeQualityPortal.Data
{
    public class CodeQualityContext : DbContext
    {
        public CodeQualityContext(string databaseName, IDatabaseInitializer<CodeQualityContext> initializer)
            : base(databaseName)
        {
            Database.SetInitializer(initializer);
        }

        public CodeQualityContext(string connectionString)
            : base(nameOrConnectionString: connectionString)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CodeQualityContext>());
        }

        public CodeQualityContext() : base("CodeQuality")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CodeQualityContext>());                        
        }

        public DbSet<DimRepo> Repos { get; set; }
        public DbSet<DimCommit> Commits { get; set; }
        public DbSet<DimFile> Files { get; set; }
        public DbSet<DimDate> Dates { get; set; }
        public DbSet<DimTarget> Targets { get; set; }
        public DbSet<DimModule> Modules { get; set; }
        public DbSet<DimNamespace> Namespaces { get; set; }
        public DbSet<DimType> Types { get; set; }
        public DbSet<DimMember> Members { get; set; }
        public DbSet<FactCodeChurn> Churn { get; set; }
        public DbSet<FactMetrics> Metrics { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<DimRepo>().HasKey(k => k.RepoId);
            modelBuilder.Entity<DimRepo>().Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimRepo>().Property(k => k.Name).IsRequired();

            modelBuilder.Entity<DimCommit>().HasKey(k => k.CommitId);
            modelBuilder.Entity<DimCommit>().Property(k => k.Sha).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimCommit>().Property(k => k.Sha).IsRequired();
            modelBuilder.Entity<DimCommit>().Property(k => k.Url).IsRequired();
            modelBuilder.Entity<DimCommit>().HasMany(c => c.Files)
                .WithMany(f => f.Commits)
                .Map(c =>
                {
                    c.ToTable("DimCommitFile");
                    c.MapLeftKey("CommitId");
                    c.MapRightKey("FileId");
                });
            
            modelBuilder.Entity<DimFile>().HasKey(k => k.FileId);
            modelBuilder.Entity<DimFile>().Property(k => k.FileName).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimFile>().Property(k => k.FileName).IsRequired();
            modelBuilder.Entity<DimFile>().Property(k => k.FileExtension).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimFile>().Property(k => k.FileExtension).IsRequired();
            modelBuilder.Entity<DimFile>().Property(k => k.Url).IsOptional(); // Bitbucket doesn't provide file urls
                        
            modelBuilder.Entity<DimDate>().HasKey(k => k.DateId);
            
            modelBuilder.Entity<FactCodeChurn>().HasKey(k => k.CodeChurnId);            

            modelBuilder.Entity<DimTarget>().HasKey(k => k.TargetId);
            modelBuilder.Entity<DimTarget>().Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimTarget>().Property(k => k.Tag).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimTarget>().Property(k => k.FileName).HasColumnType("varchar").HasMaxLength(255);

            modelBuilder.Entity<DimModule>().HasKey(k => k.ModuleId);
            modelBuilder.Entity<DimModule>().Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);
            
            modelBuilder.Entity<DimNamespace>().HasKey(k => k.NamespaceId);
            modelBuilder.Entity<DimNamespace>().Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);

            modelBuilder.Entity<DimType>().HasKey(k => k.TypeId);
            modelBuilder.Entity<DimType>().Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);

            modelBuilder.Entity<DimMember>().HasKey(k => k.MemberId);
            modelBuilder.Entity<DimMember>().Property(k => k.Name).HasColumnType("varchar").HasMaxLength(255);            

            modelBuilder.Entity<FactMetrics>().HasKey(k => k.MetricsId);
        }
    }
}
