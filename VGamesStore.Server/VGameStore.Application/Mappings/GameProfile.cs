using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGameStore.Application.DTOs;
using VGameStore.Core.Entities;
using AutoMapper;

namespace VGameStore.Application.Mappings
{
	public class GameProfile : Profile
	{
		public GameProfile()
		{
			CreateMap<Game, GameDto>()
				.ForMember(dest => dest.Genres,
			   opt => opt.MapFrom(src => src.GameGenres.Select(g => g.Genre.Name).ToList()));
			CreateMap<Game, GameDtoSpec>()
		   .ForMember(dest => dest.ImagesUrls, opt => opt.MapFrom(src =>
			   src.Images.Select(img => img.ImageUrl)))
		   .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
			   src.GameGenres.Select(gg => gg.Genre.Name)));
			CreateMap<CreateGameDto, Game>();
			CreateMap<UpdateGameDto, Game>();
		}
	}

}
