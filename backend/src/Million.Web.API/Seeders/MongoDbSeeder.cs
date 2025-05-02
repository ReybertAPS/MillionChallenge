using Million.Domain.Entities;
using Million.Infrastructure.Persistence.Mongo;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Million.Web.API.Seeders;

public static class MongoDbSeeder
{
    public static async Task SeedAsync(MongoDbContext context)
    {
        var propertyCollection = context.Properties;

        var existingCount = await propertyCollection.CountDocumentsAsync(FilterDefinition<Property>.Empty);
        if (existingCount > 0)
        {
            Console.WriteLine("Seeder: ya existen propiedades en la base de datos. No se insertarán datos de prueba.");
            return;
        }

        var baseUrl = "https://raw.githubusercontent.com/ReybertAPS/million-assets/main/images/";

        var properties = new List<Property>
        {
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER001",
                Name = "Apartamento Norte",
                Address = "Cra 45 #10-22",
                Price = 250000000,
                CodeInternal = "APT1",
                Year = 2021,
                Images = new() {
                    new() { File = $"{baseUrl}APT1-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}APT1-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER002",
                Name = "Apartamento Centro",
                Address = "Calle 12 #8-55",
                Price = 180000000,
                CodeInternal = "APT2",
                Year = 2020,
                Images = new() {
                    new() { File = $"{baseUrl}APT2-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}APT2-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER003",
                Name = "Apartamento Sur",
                Address = "Av. Sur #30-15",
                Price = 210000000,
                CodeInternal = "APT3",
                Year = 2022,
                Images = new() {
                    new() { File = $"{baseUrl}APT3-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}APT3-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER004",
                Name = "Apartamento Vista",
                Address = "Calle 100 #25-60",
                Price = 320000000,
                CodeInternal = "APT4",
                Year = 2023,
                Images = new() {
                    new() { File = $"{baseUrl}APT4-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}APT4-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER005",
                Name = "Casa Campestre",
                Address = "Vereda El Retiro",
                Price = 550000000,
                CodeInternal = "CAS1",
                Year = 2019,
                Images = new() {
                    new() { File = $"{baseUrl}CAS1-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}CAS1-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER006",
                Name = "Casa Familiar",
                Address = "Calle 80 #32-20",
                Price = 470000000,
                CodeInternal = "CAS2",
                Year = 2018,
                Images = new() {
                    new() { File = $"{baseUrl}CAS2-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}CAS2-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER007",
                Name = "Apartamento Urbano",
                Address = "Cra 10 #15-40",
                Price = 270000000,
                CodeInternal = "APT5",
                Year = 2021,
                Images = new() {
                    new() { File = $"{baseUrl}APT5-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}APT5-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER008",
                Name = "Apartamento Moderno",
                Address = "Calle 60 #20-30",
                Price = 290000000,
                CodeInternal = "APT6",
                Year = 2022,
                Images = new() {
                    new() { File = $"{baseUrl}APT6-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}APT6-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER009",
                Name = "Casa con Piscina",
                Address = "Calle 110 #12-05",
                Price = 680000000,
                CodeInternal = "CAS3",
                Year = 2020,
                Images = new() {
                    new() { File = $"{baseUrl}CAS3-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}CAS3-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER010",
                Name = "Casa de Lujo",
                Address = "Av. Las Palmas #33-22",
                Price = 950000000,
                CodeInternal = "CAS4",
                Year = 2023,
                Images = new() {
                    new() { File = $"{baseUrl}CAS4-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}CAS4-1.jpg", Enabled = true, IsMain = false }
                }
            },
            new() {
                Id = ObjectId.GenerateNewId().ToString(),
                IdOwner = "OWNER011",
                Name = "Casa Contemporánea",
                Address = "Km 4 vía al mar",
                Price = 730000000,
                CodeInternal = "CAS5",
                Year = 2021,
                Images = new() {
                    new() { File = $"{baseUrl}CAS5-0.jpg", Enabled = true, IsMain = true },
                    new() { File = $"{baseUrl}CAS5-1.jpg", Enabled = true, IsMain = false }
                }
            }
        };

        await propertyCollection.InsertManyAsync(properties);

        Console.WriteLine("Seeder: se insertaron propiedades con imágenes embebidas (1 principal y 1 secundaria por propiedad).");
    }
}
