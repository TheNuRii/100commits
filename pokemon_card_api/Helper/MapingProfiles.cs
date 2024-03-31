using AutoMapper;
using pokemon_card_api.Dto;
using pokemon_card_api.Model;

namespace pokemon_card_api.Helper;

public class MapingProfile : Profile
{
    public MapingProfile()
    {
        CreateMap<Pokemon, PokemonDto>();
        CreateMap<Category, CategoryDto>();
    }
}
