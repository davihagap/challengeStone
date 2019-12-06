using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using api.Data;
using api.Domain.Models;

namespace api.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup: class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Add DataContext using an in-memory database for testing.
                services.AddDbContext<DataContext>(options =>
                {
                    options.UseInMemoryDatabase("bancoteste");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (DataContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<DataContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with test data.
                        InitializeDbForTests(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                            "database with test messages. Error: {Message}", ex.Message);
                    }
                }
            });
        }

         private void InitializeDbForTests(DataContext db)
        {
            db.Contas.AddRange(new List<Conta> {
                new Conta(5564, "joao", 100),
                new Conta(8897, "maria", 100),
                new Conta(1223, "jose", 100)
            });
            db.Transacoes.AddRange(new List<Transacao> {
                new Transacao(new Conta(1330, "adolfo", 130), "saque", "SAQ", DateTime.Now, 100, 50, 50),
                new Transacao(new Conta(1330, "sampaio", 50), "deposito", "DEP", DateTime.Now, 50, 400, 350)
            }); 
            db.SaveChanges();
        }
    }
}