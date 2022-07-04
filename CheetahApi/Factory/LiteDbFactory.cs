using LiteDB;
using LiteDB.Async;
using Microsoft.Extensions.Options;

namespace CheetahApi.Factory
{
    public class LiteDbFactory
    {
        private readonly LiteDbServiceOption _options;

        public LiteDbFactory(IOptions<LiteDbServiceOption> options)
        {
            this._options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public LiteDatabaseAsync Create()
        {
            if (string.IsNullOrEmpty(_options.ConnectionString.Filename))
                throw new ArgumentNullException($"LiteDB.Database connection string is invalid.", nameof(ConnectionString.Filename));
            _options.Logger?.LogInformation($"Using database {_options.ConnectionString.Filename}");
            return new LiteDatabaseAsync(_options.ConnectionString, _options.Mapper);
        }
    }
}
