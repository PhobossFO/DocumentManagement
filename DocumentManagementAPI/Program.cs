using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DocumentManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

            builder.Services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new MessagePackOutputFormatter());
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            builder.Services.AddOutputCache(options =>
            {
                options.AddBasePolicy(builder =>
                builder.Expire(TimeSpan.FromSeconds(25)));
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }

}
