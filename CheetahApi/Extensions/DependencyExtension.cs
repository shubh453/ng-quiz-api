using CheetahApi.Factory;
using CheetahApi.Infrastructure;
using CheetahApi.Services;
using LiteDB;
using LiteDB.Async;
using LiteDB.Identity.Async.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CheetahApi.Extensions
{
    public static class DependencyExtension
    {
        public const string LiteDatabaseConnectionStringKey = "LiteDatabase";
        public const string LiteDatabaseLoggerCategory = "LiteDB.LiteDatabase";

        public static IServiceCollection AddClassDependencies(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSingleton<IQuizService, QuizService>();
            services.AddSingleton<IQuizRepository, QuizRepository>();
            services.AddSingleton<IQuestionnaireService, QuestionnaireService>();
            services.AddSingleton<IQuestionnaireRepository, QuestionnaireRepository>();

            return services;
        }

        public static IServiceCollection AddLiteDb(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var connectionString = configuration.GetConnectionString(LiteDatabaseConnectionStringKey);

            services.AddOptions();
            services.AddOptions<LiteDbServiceOption>()
                .Configure<IServiceProvider>((options, provider) =>
                {
                    var factory = provider.GetService<ILoggerFactory>();
                    options.Logger = factory?.CreateLogger(LiteDatabaseLoggerCategory);

                    if (connectionString != null)
                    {
                        options.ConnectionString = new ConnectionString(connectionString);
                    }

                });

            services.TryAddSingleton<LiteDbFactory>();

            services.TryAddSingleton<ILiteDatabaseAsync>(provider =>
            {
                var factory = provider.GetRequiredService<LiteDbFactory>();
                return factory.Create();
            });

            services.AddLiteDbIdentityAsync(connectionString).AddDefaultTokenProviders();
            return services;
        }


    }
}
