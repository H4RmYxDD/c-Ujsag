using ArticleDataBase;
using ArticleServices;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApiDocument();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("ArticleDbContext");
builder.Services.AddDbContext<ArticleDbContext>(options =>
  options.UseSqlServer(connectionString));

builder.Services.AddTransient<IService, Service>();
builder.Services.AddHttpClient();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<ArticleDbContext>();
builder.Services.AddAuthorization();

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("admin", policy => policy.RequireRole("Admin"))
    .AddPolicy("user", policy => policy.RequireRole("user"));

var allowSpecificOrigins = "_allowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ArticleDbContext>();
    var seeder = new ArticleDataBase.Seeder(db);
    await seeder.SeedAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}


app.UseHttpsRedirection();
app.UseCors(allowSpecificOrigins);

app.MapGroup("Account").WithTags("Account").MapIdentityApi<IdentityUser>();

app.MapGet("get/{id:int}", async (int id, IService service) =>
{
    return await service.GetArticleAsync(id);
}).RequireAuthorization();

app.MapGet("list", async (IService service) =>
{
    return await service.ListAllArticleAsync();
}).RequireAuthorization();

app.MapPost("createArticle", async (Article model, IService service) =>
{
    await service.CreateArticleAsync(model);

    return Results.Created();
}).RequireAuthorization();

app.MapPost("createAuthor", async (Author model, IService service) =>
{
    await service.CreateAuthorAsync(model);

    return Results.Created();
}).RequireAuthorization();


app.MapPut("update", async (Article model, IService service) =>
{
    await service.UpdateArticleAsync(model);

    return Results.Ok();
}).RequireAuthorization();

app.MapDelete("delete/{id:int}", async (int id, IService service) =>
{
    await service.DeleteArticleAsync(id);

    return Results.Ok();
}).RequireAuthorization();

app.Run();