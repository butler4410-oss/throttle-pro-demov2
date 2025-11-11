using Microsoft.EntityFrameworkCore;
using ThrottlePro.Shared.Entities;
using ThrottlePro.Shared.Enums;

namespace ThrottlePro.Server.Data;

/// <summary>
/// Seeds the database with sample data for development
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with parent companies, stores, customers, and sample data
    /// </summary>
    public static async Task SeedAsync(ThrottleProDbContext context, ILogger logger)
    {
        try
        {
            // Check if data already exists
            if (await context.Parents.AnyAsync())
            {
                logger.LogInformation("Database already seeded. Skipping seed data.");
                return;
            }

            logger.LogInformation("Starting database seed...");

            // Create Parent Companies
            var greaseMonkey = new Parent
            {
                Id = Guid.NewGuid(),
                Name = "Grease Monkey International",
                BrandName = "Grease Monkey",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            };

            var jiffyLube = new Parent
            {
                Id = Guid.NewGuid(),
                Name = "Jiffy Lube Franchise Group",
                BrandName = "Jiffy Lube",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            };

            context.Parents.AddRange(greaseMonkey, jiffyLube);
            await context.SaveChangesAsync();

            logger.LogInformation("Created 2 parent companies");

            // Create Stores for Grease Monkey
            var gmStores = new[]
            {
                CreateStore(greaseMonkey.Id, "Grease Monkey Denver Downtown", "001", "1234 Main St", "Denver", "CO", "80202"),
                CreateStore(greaseMonkey.Id, "Grease Monkey Aurora", "002", "5678 E Colfax Ave", "Aurora", "CO", "80010"),
                CreateStore(greaseMonkey.Id, "Grease Monkey Colorado Springs", "003", "9012 N Academy Blvd", "Colorado Springs", "CO", "80920"),
                CreateStore(greaseMonkey.Id, "Grease Monkey Fort Collins", "004", "3456 College Ave", "Fort Collins", "CO", "80524"),
                CreateStore(greaseMonkey.Id, "Grease Monkey Boulder", "005", "7890 Pearl St", "Boulder", "CO", "80302")
            };

            // Create Stores for Jiffy Lube
            var jlStores = new[]
            {
                CreateStore(jiffyLube.Id, "Jiffy Lube Phoenix Central", "101", "2345 Central Ave", "Phoenix", "AZ", "85004"),
                CreateStore(jiffyLube.Id, "Jiffy Lube Scottsdale", "102", "6789 Scottsdale Rd", "Scottsdale", "AZ", "85251"),
                CreateStore(jiffyLube.Id, "Jiffy Lube Tempe", "103", "1234 S Mill Ave", "Tempe", "AZ", "85281"),
                CreateStore(jiffyLube.Id, "Jiffy Lube Mesa", "104", "5678 E Main St", "Mesa", "AZ", "85201"),
                CreateStore(jiffyLube.Id, "Jiffy Lube Tucson", "105", "9012 N Oracle Rd", "Tucson", "AZ", "85704")
            };

            context.Stores.AddRange(gmStores.Concat(jlStores));
            await context.SaveChangesAsync();

            logger.LogInformation("Created 10 stores");

            // Create Customers for Grease Monkey
            var gmCustomers = CreateCustomers(greaseMonkey.Id, gmStores, 100);
            context.Customers.AddRange(gmCustomers);

            // Create Customers for Jiffy Lube
            var jlCustomers = CreateCustomers(jiffyLube.Id, jlStores, 100);
            context.Customers.AddRange(jlCustomers);

            await context.SaveChangesAsync();

            logger.LogInformation("Created 200 customers");

            // Create Vehicles for Customers
            var allCustomers = gmCustomers.Concat(jlCustomers).ToList();
            var vehicles = CreateVehicles(allCustomers);
            context.Vehicles.AddRange(vehicles);
            await context.SaveChangesAsync();

            logger.LogInformation("Created {VehicleCount} vehicles", vehicles.Count);

            // Create Visits
            var allStores = gmStores.Concat(jlStores).ToList();
            var visits = CreateVisits(allCustomers, allStores, vehicles);
            context.Visits.AddRange(visits);
            await context.SaveChangesAsync();

            logger.LogInformation("Created {VisitCount} visits", visits.Count);

            // Create Segments
            var gmSegments = CreateSegments(greaseMonkey.Id);
            var jlSegments = CreateSegments(jiffyLube.Id);
            context.Segments.AddRange(gmSegments.Concat(jlSegments));
            await context.SaveChangesAsync();

            logger.LogInformation("Created segments");

            // Create Campaigns
            var gmCampaigns = CreateCampaigns(greaseMonkey.Id, gmSegments);
            var jlCampaigns = CreateCampaigns(jiffyLube.Id, jlSegments);
            context.Campaigns.AddRange(gmCampaigns.Concat(jlCampaigns));
            await context.SaveChangesAsync();

            logger.LogInformation("Created campaigns");

            logger.LogInformation("Database seed completed successfully!");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error seeding database");
            throw;
        }
    }

    private static Store CreateStore(Guid parentId, string name, string storeNumber, string address, string city, string state, string zipCode)
    {
        return new Store
        {
            Id = Guid.NewGuid(),
            ParentId = parentId,
            Name = name,
            StoreNumber = storeNumber,
            Address = address,
            City = city,
            State = state,
            ZipCode = zipCode,
            Phone = $"({Random.Shared.Next(200, 999)}) {Random.Shared.Next(200, 999)}-{Random.Shared.Next(1000, 9999)}",
            Email = $"store{storeNumber}@example.com",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "seed"
        };
    }

    private static List<Customer> CreateCustomers(Guid parentId, Store[] stores, int count)
    {
        var firstNames = new[] { "John", "Jane", "Michael", "Emily", "David", "Sarah", "Robert", "Lisa", "James", "Jennifer", "William", "Mary", "Richard", "Patricia", "Thomas", "Linda", "Charles", "Barbara", "Daniel", "Elizabeth" };
        var lastNames = new[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin" };
        var lifecycleStages = new[] { CustomerLifecycleStage.New, CustomerLifecycleStage.Active, CustomerLifecycleStage.Active, CustomerLifecycleStage.Active, CustomerLifecycleStage.AtRisk, CustomerLifecycleStage.Lapsed };

        var customers = new List<Customer>();

        for (int i = 0; i < count; i++)
        {
            var firstName = firstNames[Random.Shared.Next(firstNames.Length)];
            var lastName = lastNames[Random.Shared.Next(lastNames.Length)];
            var email = $"{firstName.ToLower()}.{lastName.ToLower()}{Random.Shared.Next(1, 999)}@example.com";

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = $"({Random.Shared.Next(200, 999)}) {Random.Shared.Next(200, 999)}-{Random.Shared.Next(1000, 9999)}",
                Address = $"{Random.Shared.Next(100, 9999)} {new[] { "Main", "Oak", "Maple", "Pine", "Cedar" }[Random.Shared.Next(5)]} St",
                City = stores[0].City,
                State = stores[0].State,
                ZipCode = $"{Random.Shared.Next(10000, 99999)}",
                DateOfBirth = DateTime.UtcNow.AddYears(-Random.Shared.Next(25, 65)),
                LifecycleStage = lifecycleStages[Random.Shared.Next(lifecycleStages.Length)],
                FirstVisitDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(30, 365)),
                LastVisitDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 180)),
                TotalVisits = Random.Shared.Next(1, 20),
                TotalSpent = Random.Shared.Next(100, 5000),
                AverageOrderValue = Random.Shared.Next(50, 300),
                DaysSinceLastVisit = Random.Shared.Next(1, 180),
                EmailOptIn = Random.Shared.Next(0, 100) > 20,
                SmsOptIn = Random.Shared.Next(0, 100) > 50,
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-Random.Shared.Next(30, 365)),
                CreatedBy = "seed"
            };

            customers.Add(customer);
        }

        return customers;
    }

    private static List<Vehicle> CreateVehicles(List<Customer> customers)
    {
        var makes = new[] { "Toyota", "Honda", "Ford", "Chevrolet", "Nissan", "Jeep", "RAM", "Subaru", "Hyundai", "Mazda" };
        var models = new[] { "Camry", "Accord", "F-150", "Silverado", "Altima", "Wrangler", "1500", "Outback", "Elantra", "CX-5" };
        var colors = new[] { "White", "Black", "Silver", "Gray", "Red", "Blue", "Green" };

        var vehicles = new List<Vehicle>();

        foreach (var customer in customers)
        {
            // Each customer gets 1-2 vehicles
            var vehicleCount = Random.Shared.Next(1, 3);

            for (int i = 0; i < vehicleCount; i++)
            {
                var vehicle = new Vehicle
                {
                    Id = Guid.NewGuid(),
                    ParentId = customer.ParentId,
                    CustomerId = customer.Id,
                    Make = makes[Random.Shared.Next(makes.Length)],
                    Model = models[Random.Shared.Next(models.Length)],
                    Year = Random.Shared.Next(2010, 2024),
                    Color = colors[Random.Shared.Next(colors.Length)],
                    VIN = $"1HGBH41JXMN{Random.Shared.Next(100000, 999999)}",
                    LicensePlate = $"{(char)Random.Shared.Next(65, 91)}{(char)Random.Shared.Next(65, 91)}{(char)Random.Shared.Next(65, 91)}{Random.Shared.Next(1000, 9999)}",
                    Mileage = Random.Shared.Next(5000, 150000),
                    IsPrimary = i == 0,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "seed"
                };

                vehicles.Add(vehicle);
            }
        }

        return vehicles;
    }

    private static List<Visit> CreateVisits(List<Customer> customers, List<Store> stores, List<Vehicle> vehicles)
    {
        var services = new[] { "Oil Change", "Tire Rotation", "Brake Service", "Engine Tune-Up", "Transmission Service", "Air Filter Replacement" };
        var visits = new List<Visit>();

        foreach (var customer in customers)
        {
            var customerVehicles = vehicles.Where(v => v.CustomerId == customer.Id).ToList();
            var visitCount = Random.Shared.Next(1, customer.TotalVisits + 1);

            for (int i = 0; i < visitCount; i++)
            {
                var store = stores.First(s => s.ParentId == customer.ParentId);
                var vehicle = customerVehicles.Any() ? customerVehicles[Random.Shared.Next(customerVehicles.Count)] : null;

                var totalAmount = Random.Shared.Next(50, 500);
                var discountAmount = Random.Shared.Next(0, 50);

                var visit = new Visit
                {
                    Id = Guid.NewGuid(),
                    ParentId = customer.ParentId,
                    CustomerId = customer.Id,
                    StoreId = store.Id,
                    VehicleId = vehicle?.Id,
                    InvoiceNumber = $"INV-{Random.Shared.Next(10000, 99999)}",
                    VisitDate = DateTime.UtcNow.AddDays(-Random.Shared.Next(1, 365)),
                    TotalAmount = totalAmount,
                    DiscountAmount = discountAmount,
                    NetAmount = totalAmount - discountAmount,
                    ServicesPerformed = string.Join(", ", services.OrderBy(x => Random.Shared.Next()).Take(Random.Shared.Next(1, 4))),
                    VehicleMileage = vehicle?.Mileage + Random.Shared.Next(0, 5000),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "seed"
                };

                visits.Add(visit);
            }
        }

        return visits;
    }

    private static List<Segment> CreateSegments(Guid parentId)
    {
        return new List<Segment>
        {
            new Segment
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = "High Value Customers",
                Description = "Customers with total spend over $1000",
                Type = SegmentType.Dynamic,
                CustomerCount = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            },
            new Segment
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = "At-Risk Customers",
                Description = "Customers who haven't visited in 90+ days",
                Type = SegmentType.Dynamic,
                CustomerCount = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            },
            new Segment
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                Name = "New Customers",
                Description = "Customers who joined in the last 30 days",
                Type = SegmentType.Dynamic,
                CustomerCount = 0,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            }
        };
    }

    private static List<Campaign> CreateCampaigns(Guid parentId, List<Segment> segments)
    {
        return new List<Campaign>
        {
            new Campaign
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                SegmentId = segments[0].Id,
                Name = "Spring Oil Change Special",
                Description = "$10 off any oil change service",
                Status = CampaignStatus.Active,
                Channel = CampaignChannel.Email,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EndDate = DateTime.UtcNow.AddDays(30),
                Budget = 500,
                Spent = 250,
                TargetAudience = 1000,
                Sent = 800,
                Delivered = 750,
                Opened = 300,
                Clicked = 100,
                Redeemed = 50,
                Revenue = 2500,
                ROAS = 10,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            },
            new Campaign
            {
                Id = Guid.NewGuid(),
                ParentId = parentId,
                SegmentId = segments[1].Id,
                Name = "Win-Back Campaign",
                Description = "Special offer for customers we haven't seen in a while",
                Status = CampaignStatus.Active,
                Channel = CampaignChannel.DirectMail,
                StartDate = DateTime.UtcNow.AddDays(-15),
                EndDate = DateTime.UtcNow.AddDays(45),
                Budget = 1000,
                Spent = 400,
                TargetAudience = 500,
                Sent = 500,
                Delivered = 480,
                Opened = 0,
                Clicked = 0,
                Redeemed = 25,
                Revenue = 1500,
                ROAS = 3.75m,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "seed"
            }
        };
    }
}
