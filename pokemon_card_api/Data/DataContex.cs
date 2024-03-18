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
    
}