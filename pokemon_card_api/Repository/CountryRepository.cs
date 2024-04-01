using AutoMapper;
using pokemon_card_api.Data;
using pokemon_card_api.Interface;
using pokemon_card_api.Model;

namespace pokemon_card_api.Repository;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    
    public CountryRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ICollection<Country> GetCountries()
    {
        return _context.Countries.ToList();
    }

    public Country GetCountry(int id)
    {
        return _context.Countries.Where(e => e.Id == id).FirstOrDefault();
    }

    public Country GetCountryByOwner(int ownerId)
    {
        return _context.Owners.Where(o => o.Id == ownerId).Select(c =>c.Country).FirstOrDefault();
    }

    public ICollection<Owner> GetOwnersFromCountry(int countryId)
    {
        return _context.Owners.Where(c =>  c.Country.Id == countryId).ToList();
    }

    public bool CountryExists(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
    }
}