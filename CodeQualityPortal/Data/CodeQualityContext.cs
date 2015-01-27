using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using CodeQualityPortal.Data.Maps;

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
            
            modelBuilder.Configurations.Add(new DimRepoConfiguration());
            modelBuilder.Configurations.Add(new DimCommitConfiguration());
            modelBuilder.Configurations.Add(new DimFileConfiguration());
            modelBuilder.Configurations.Add(new DimDateConfiguration());
            modelBuilder.Configurations.Add(new FactCodeChurnConfiguration());
            modelBuilder.Configurations.Add(new DimTargetConfiguration());
            modelBuilder.Configurations.Add(new DimModuleConfiguration());
            modelBuilder.Configurations.Add(new DimNamespaceConfiguration());
            modelBuilder.Configurations.Add(new DimTypeConfiguration());
            modelBuilder.Configurations.Add(new DimMemberConfiguration());
            modelBuilder.Configurations.Add(new FactMetricsConfiguration());
        }
    }
}
