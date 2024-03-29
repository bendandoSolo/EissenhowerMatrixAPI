﻿using EissenhowerMatrixBackend.DataBaseConnection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API_Tests.MinimalAPI;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    public CustomWebApplicationFactory()
    {
        ClientOptions.BaseAddress = new Uri("http://localhost/api");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // Add a database context (AppDbContext) using an in-memory database for testing, old method that failed !
            //services.AddDbContext<TodoDb>(options =>
            //{
            //    options.UseInMemoryDatabase("InMemoryTestDatabase");
            //});
            services.AddScoped((_services) => new TodoDb(new DbContextOptionsBuilder<TodoDb>().UseInMemoryDatabase("test").Options));

            // Build the service provider.
            var serviceProvider = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database context (AppDbContext).
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TodoDb>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    SeedData(db);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred while seeding the database with test data. Exception: {ex.Message}");
                }
            }
        });
    }

    private void SeedData(TodoDb db)
    {
        db.Todos.AddRange(
            new Todo { Id = 1, Name = "Task 1", Description = "Description 1", Priority = EissenhowerStatus.Unassigned },
            new Todo { Id = 2, Name = "Task 2", Description = "Description 2", Priority = EissenhowerStatus.Unassigned, CompletionDate = DateTime.Now }
        );

        db.SaveChanges();
    }
}
