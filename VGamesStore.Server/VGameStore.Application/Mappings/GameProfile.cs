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
			CreateMap<Game, GameDto>();
			CreateMap<CreateGameDto, Game>();
			CreateMap<UpdateGameDto, Game>();
		}
	}

}
