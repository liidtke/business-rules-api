using BRules.Domain.RuleAggregate;
using BRules.Domain.SharedKernel;
using BRules.Infrastructure;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRules.Web
{
    public static class DependencyInjection
    {
        internal static void ConfigureService(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DatabaseSettings>(
               configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                 sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetRequiredService<IDatabaseSettings>().ConnectionString));
            services.AddScoped<MongoContext>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<CreateRuleService>();
            services.AddScoped<AlterRuleService>();

            var config = new TypeAdapterConfig();
            services.AddSingleton(config);
            IMapper mapper = new Mapper(config);
            services.AddSingleton<IMapper>(mapper);
        }
    }
}
