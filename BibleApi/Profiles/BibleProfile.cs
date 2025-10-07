using AutoMapper;
using BibleApi.Models.Bible;
using BibleApi.Models.DTO;

namespace BibleApi.Profiles
{
	public class BibleProfile : Profile
	{
		public BibleProfile()
		{
			CreateMap<BookEntity, Book>()
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.Testament, opt => opt.MapFrom(src => MapTestament(src.Testament_Reference_Id)));


			CreateMap<VerseEntity, Verse>();
		}

		private static string MapTestament(int testamentReferenceId)
		{
			return testamentReferenceId switch
			{
				1 => "Old Testament",
				2 => "New Testament",
				3 => "External Book",
				_ => "Unknown"
			};
		}
	}
}
