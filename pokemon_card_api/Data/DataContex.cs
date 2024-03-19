using Microsoft.EntityFrameworkCore;
using pokemon_card_api.Model;

namespace pokemon_card_api.Data;

public class DataContex : DbContext
{
    public DataContex(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonCategory> PokemonCategories { get; set; }
    public DbSet<PokemonOwner> PokemonOwners { get; set; }
    public DbSet<Rewiew> Rewiews { get; set; }
    public DbSet<Rewiewer> Rewiewers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonCategory>()
            .HasKey(pc => new { pc.PokemonId, pc.CategoryId });
        modelBuilder.Entity<PokemonCategory>()
            .HasOne(p => p.Pokemon)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(c => c.PokemonId);
        modelBuilder.Entity<PokemonCategory>()
            .HasOne(p => p.Category)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(c => c.CategoryId);
        
        modelBuilder.Entity<PokemonOwner>()
            .HasKey(pc => new { po.PokemonId, po.OwnerId });
        modelBuilder.Entity<PokemonOwner>()
            .HasOne(p => p.Pokemon)
            .WithMany(pc => pc.PokemonOwners)
            .HasForeignKey(c => c.PokemonId);
        modelBuilder.Entity<PokemonOwner>()
            .HasOne(p => p.Owner)
            .WithMany(pc => pc.PokemonOwners)
            .HasForeignKey(c => c.OwnerId);

    }
    
}