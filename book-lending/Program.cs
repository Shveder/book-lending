using System.Text.Json.Serialization;
using book_lending.Data;
using book_lending.Middlewares;
using book_lending.Repository;
using book_lending.Repository.Interface;
using book_lending.Services;
using book_lending.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

ConfigureMiddleware(app);

await InitializeDatabaseAsync(app);

app.Run();

static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    
    services.AddTransient<IDbRepository, DbRepository>();
    services.AddTransient<ILibrarianService, LibrarianService>();
    services.AddTransient<IAuthorizationService, AuthorizationService>();
    services.AddTransient<IAdminService, AdminService>();
    services.AddTransient<ICaretakerService, CaretakerService>();
    services.AddTransient<IGetModelService, GetModelService>();
    services.AddTransient<IHandymanService, HandymanService>();
    
    services.AddDbContext<DataContext>(options => 
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
}

static void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
}

static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<DataContext>();
        
        await context.Database.MigrateAsync();
        await SeedDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

static async Task SeedDataAsync(DataContext context)
{
    if (await context.Roles.AnyAsync()) return;

    var caretakerRole = new Role { RoleName = "Caretaker" };
    var handymanRole = new Role { RoleName = "Handyman" };
    var librarianRole = new Role { RoleName = "Librarian" };

    context.Roles.AddRange(caretakerRole, handymanRole, librarianRole);

    var addBookOperation = new Operation { OperationName = "AddBook" };
    var repairBookOperation = new Operation { OperationName = "RepairBook" };
    var giveBookOperation = new Operation { OperationName = "GiveBook" };
    var returnBookOperation = new Operation { OperationName = "ReturnBook" };
    var deleteBookOperation = new Operation { OperationName = "DeleteDamagedBook" };

    context.Operations.AddRange(addBookOperation, repairBookOperation, giveBookOperation, returnBookOperation, deleteBookOperation);

    context.RoleOperations.AddRange(
        new RoleOperation { Role = caretakerRole, Operation = addBookOperation },
        new RoleOperation { Role = caretakerRole, Operation = deleteBookOperation },
        new RoleOperation { Role = handymanRole, Operation = repairBookOperation },
        new RoleOperation { Role = librarianRole, Operation = giveBookOperation },
        new RoleOperation { Role = librarianRole, Operation = returnBookOperation }
    );

    await context.SaveChangesAsync();
}
