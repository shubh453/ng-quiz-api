using LiteDB;

namespace CheetahApi.Factory
{
    public class LiteDbServiceOption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbServiceOption"/> class.
        /// </summary>
        public LiteDbServiceOption() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiteDbServiceOption"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public LiteDbServiceOption(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            this.ConnectionString = new ConnectionString(connectionString);
        }

        /// <summary>
        /// The <see cref="ConnectionString"/> connection string for the LiteDb database
        /// </summary>
        public ConnectionString ConnectionString { get; set; } = new ConnectionString();

        /// <summary>
        /// The <see cref="BsonMapper"/> class used to convert entities to and from BsonDocument
        /// </summary>
        public BsonMapper Mapper { get; set; } = BsonMapper.Global;

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        public ILogger? Logger { get; set; }
    }
}
