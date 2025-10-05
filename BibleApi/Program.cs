
using BibleApi.Profiles;
using BibleApi.Repository;
using BibleApi.Services;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BibleApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
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
			builder.Services.AddSingleton<IRepository, BibleRepository>();
			builder.Services.AddSingleton<BibleService>();

			builder.Services.AddSingleton<ISqlQueryProvider>(sp => new QueryLoader(Assembly.GetExecutingAssembly()));
			builder.Services.AddSingleton<QueryValidator>();

			var app = builder.Build();

			// Startup Verification of Queries
			var queryValidator = app.Services.GetRequiredService<QueryValidator>();
			queryValidator.ValidateQueries<BookQueries>();

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
	}
}
