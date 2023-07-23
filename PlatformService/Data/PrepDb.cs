using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {

            Console.WriteLine("--> SeedData called");

            if(context == null)
            {
                Console.WriteLine("--> context is null!");
                return;
            }

            try
            {
                if(!context.Platforms.Any())
                {
                    Console.WriteLine("--> Seed data...");
                    context.Platforms.AddRange(
                        new Platform() {Name="Dot Net", Publisher="Microsoft", Cost="Free"},
                        new Platform() {Name="Sql Server Express", Publisher="Microsoft", Cost="Free"},
                        new Platform() {Name="Kubernetes", Publisher="Cloud Native Computing Foundation", Cost="Free"}
                    );

                    context.SaveChanges();
                } 
                else 
                {
                    Console.WriteLine("--> We already have data in db");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Error in SeedData: {ex.Message}");
            }
        }
    }
}