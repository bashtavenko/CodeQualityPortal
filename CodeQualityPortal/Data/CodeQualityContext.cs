using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using CodeQualityPortal.Data.Maps;

namespace CodeQualityPortal.Data
{
    public class CodeQualityContext : DbContext
    {
        private const string DefaultConnectionStringKey = "CodeQuality";

        public CodeQualityContext()
            : this(DefaultConnectionStringKey, null)
        {
        }

        public CodeQualityContext(IDatabaseInitializer<CodeQualityContext> initializer) : this (DefaultConnectionStringKey, initializer)
        {
        }

        private CodeQualityContext(string connectionString, IDatabaseInitializer<CodeQualityContext> initializer)
            : base(nameOrConnectionString: connectionString)
        {
            Database.SetInitializer(initializer ?? new CodeQualityCreateDatabaseIfNotExists());
        }

        public DbSet<DimRepo> Repos { get; set; }
        public DbSet<DimCommit> Commits { get; set; }
        public DbSet<DimFile> Files { get; set; }
        public DbSet<DimDate> Dates { get; set; }
        public DbSet<DimSystem> Systems { get; set; }
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
            modelBuilder.Configurations.Add(new DimSystemConfiguration());
            modelBuilder.Configurations.Add(new DimModuleConfiguration());
            modelBuilder.Configurations.Add(new DimNamespaceConfiguration());
            modelBuilder.Configurations.Add(new DimTypeConfiguration());
            modelBuilder.Configurations.Add(new DimMemberConfiguration());
            modelBuilder.Configurations.Add(new FactMetricsConfiguration());
        }
    }
}
