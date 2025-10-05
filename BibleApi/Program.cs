
using BibleApi.Profiles;
using BibleApi.Repository;
using BibleApi.Services;

namespace BibleApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			ValidateQueries();

			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddAutoMapper(c =>
			{
				c.AddProfile<BibleProfile>();
			});
			builder.Services.AddSingleton<BibleRepository>();
			builder.Services.AddSingleton<BibleService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}

		private static void ValidateQueries()
		{
			QueryValidator.ValidateQueries<BookQueries>();
		}
	}
}
