using BrandUp.Extensions.Migrations;
namespace ExampleWebSite.Migrations
{
    public class MemoryMigrationStore : IMigrationState
    {
        readonly List<string> names = new List<string>();

        public List<string> Names => names;

        public Task<bool> IsAppliedAsync(IMigrationDefinition migrationDefinition, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(names.Contains(migrationDefinition.Name.ToUpper()));
        }

        public Task SetUpAsync(IMigrationDefinition migrationDefinition, CancellationToken cancellationToken = default)
        {
            names.Add(migrationDefinition.Name.ToUpper());

            return Task.CompletedTask;
        }

        public Task SetDownAsync(IMigrationDefinition migrationDefinition, CancellationToken cancellationToken = default)
        {
            if (!names.Remove(migrationDefinition.Name.ToUpper()))
                throw new InvalidOperationException();

            return Task.CompletedTask;
        }
    }
}
