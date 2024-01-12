using Microsoft.EntityFrameworkCore;

namespace L2DataParser.L2Models
{
    public class L2DataContext : DbContext
    {
        /*
        public L2DataContext()
        : base("L2WebDataDatabase")
        {
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=192.168.1.44;Initial Catalog=L2Data;Persist Security Info=True;User ID=L2Web;Password=Prdel006;TrustServerCertificate=True ");
        }

        public DbSet<ItemData> ItemDatas { get; set; }
        public DbSet<ItemName> ItemNames { get; set; }
        public DbSet<NpcData> NpcDatas { get; set; }
        public DbSet<NpcName> NpcNames { get; set; }
        /*public DbSet<DropInformation> DropInformations { get; set; }
        public DbSet<SpoilInformation> SpoilInformations { get; set; }
        public DbSet<RecipeData> RecipeDatas { get; set; }
        public DbSet<NpcTerritory> NpcTerritories { get; set; }
        public DbSet<NpcTerritoryPoint> NpcTerritoryPoints { get; set; }
        public DbSet<NpcMaker> NpcMakers { get; set; }
        public DbSet<NpcSpawn> NpcSpawns { get; set; }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // <... other configurations ...>
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // Configure Decimal to always have a precision of 18 and a scale of 4
           /* modelBuilder.Entity<DropInformation>()
                .Property(p => p.Percents)
                .HasPrecision(12, 4);

            modelBuilder.Entity<SpoilInformation>()
                .Property(p => p.Percents)
                .HasPrecision(12, 4);*/

            base.OnModelCreating(modelBuilder);
        }
    }
}
