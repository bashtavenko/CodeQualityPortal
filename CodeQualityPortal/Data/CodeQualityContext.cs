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
        public DbSet<FactCodeChurn> Churn { get; set; }
        
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
            
            modelBuilder.Entity<DimFile>().HasKey(k => k.FileId);
            modelBuilder.Entity<DimFile>().Property(k => k.FileName).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimFile>().Property(k => k.FileName).IsRequired();
            modelBuilder.Entity<DimFile>().Property(k => k.FileExtension).HasColumnType("varchar").HasMaxLength(255);
            modelBuilder.Entity<DimFile>().Property(k => k.FileExtension).IsRequired();
            modelBuilder.Entity<DimFile>().Property(k => k.Url).IsRequired();
                        
            modelBuilder.Entity<DimDate>().HasKey(k => k.DateId);
            
            modelBuilder.Entity<FactCodeChurn>().HasKey(k => k.CodeChurnId);
        }
    }
}
