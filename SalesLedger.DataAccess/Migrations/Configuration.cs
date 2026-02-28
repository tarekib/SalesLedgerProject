using System.Data.Entity.Migrations;

namespace SalesLedger.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesLedger.DataAccess.SalesLedgerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SalesLedgerDbContext context)
        {
            // Seeding is handled by SalesLedgerDbInitializer.
            // This configuration is kept for manual migrations via Package Manager Console.
        }
    }
}
