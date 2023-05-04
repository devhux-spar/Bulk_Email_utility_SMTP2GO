using System;
//using Microsoft.AspNetCore.Mvc;
//[assembly: ApiController]

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.AddControllers();
    services.AddRazorPages();
}


// Add services to the container.
//builder.Services.AddRazorPages();
//builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.Run();
