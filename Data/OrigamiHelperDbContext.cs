using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OrigamiHelper.Models;
using Microsoft.AspNetCore.Identity;

namespace OrigamiHelper.Data;

public class OrigamiHelperDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;

    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Complexity> Complexities { get; set; }
    public DbSet<ModelPaper> ModelPapers { get; set; }
    public DbSet<Paper> Papers { get; set; }
    public DbSet<Source> Sources { get; set; }

    public OrigamiHelperDbContext(DbContextOptions<OrigamiHelperDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            Name = "Admin",
            NormalizedName = "admin"
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
        {
            Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            UserName = "Administrator",
            Email = "admina@strator.comx",
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
        });

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
            UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
        });
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile
        {
            Id = 1,
            IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
            FirstName = "Admina",
            LastName = "Strator",
            Address = "101 Main Street",
        });

        // Seed Complexities
        modelBuilder.Entity<Complexity>().HasData(
            new Complexity { Id = 1, Difficulty = "Simple" },
            new Complexity { Id = 2, Difficulty = "Low Intermediate" },
            new Complexity { Id = 3, Difficulty = "Intermediate" },
            new Complexity { Id = 4, Difficulty = "High Intermediate" },
            new Complexity { Id = 5, Difficulty = "Complex" },
            new Complexity { Id = 6, Difficulty = "Super Complex" }
        );

        // Seed Papers
        modelBuilder.Entity<Paper>().HasData(
            new Paper { Id = 1, Brand = "Kami" },
            new Paper { Id = 2, Brand = "Washi" },
            new Paper { Id = 3, Brand = "Foil" }
        );

        // Seed Sources
        modelBuilder.Entity<Source>().HasData(
            new Source { Id = 1, Title = "Origami Design Secrets 2E" },
            new Source { Id = 2, Title = "Works of Satoshi Kamiya 1995-2003" }
        );

        // Seed Models
        modelBuilder.Entity<Model>().HasData(
            new Model
            {
                Id = 1,
                Title = "Cooking Rat",
                ComplexityId = 4,
                SourceId = 1,
                StepCount = 20,
                UserProfileId = 1,
                CreatedAt = DateTime.UtcNow,
                ModelImg = "/Images/rat.png",
                Artist = "Nguyen Hong Chuong"
            },
            new Model
            {
                Id = 2,
                Title = "Ancient Dragon",
                ComplexityId = 6,
                SourceId = 2,
                StepCount = 274,
                UserProfileId = 1,
                CreatedAt = DateTime.UtcNow,
                ModelImg = "/Images/dragon.jpg",
                Artist = "Satoshi Kamiya"
            }
        );

        // Seed ModelPapers (linking table)
        modelBuilder.Entity<ModelPaper>().HasData(
            new ModelPaper { Id = 1, ModelId = 1, PaperId = 1 },
            new ModelPaper { Id = 2, ModelId = 2, PaperId = 3 },
            new ModelPaper { Id = 3, ModelId = 2, PaperId = 2 } // Dragon also works with Washi
        );
    }
}