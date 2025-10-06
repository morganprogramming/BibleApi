using AutoMapper;
using BibleApi.Models.DTO;
using BibleApi.Models.Http;

namespace BibleApi.Profiles
{
	public class HttpResponseProfile : Profile
	{
		public HttpResponseProfile()
		{
			CreateMap<Book, BookResponse>();
		}
	}
}
