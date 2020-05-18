using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SlitherBrain;
using SlitherDatabase;
using SlitherModel.Source;
using SlitherProcessor;
using System.Collections.Generic;

namespace SlitherProcessorApi
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "slither";
        private string sourceDatabaseFolder;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            sourceDatabaseFolder = configuration["SourceDatabaseFolder"];
            GameDatabase.DatabaseFolder = configuration["DatabaseFolder"];
            GameDatabase.LoadGames();

            ActiveGameDatabase.ActiveGames = new List<Game>();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins, builder => builder.WithOrigins("http://slither.io").AllowAnyHeader().AllowAnyMethod());
            });

            var frameProcessor = new FrameProcessor(new OutcomeProcessor(new OutcomeScoreProcessor()), new CollisionMapProcessor(new CollisionMapResolutionProcessor(new CollisionSliceProcessor(new FoodSliceProcessor(new CollisionService()), new BadCollisionSliceProcessor(new CollisionService()), new SelfSliceProcessor(new CollisionService()))), new SlitherFrameNormalizer()));
            var gameManager = new GameManager(new GameProcessor(frameProcessor), sourceDatabaseFolder);
            var slitherPlayer = new SlitherPlayer(frameProcessor);
            services.AddSingleton(gameManager);
            services.AddSingleton(slitherPlayer);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
