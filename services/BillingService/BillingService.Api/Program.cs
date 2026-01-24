using BillingService.Application.Interfaces;
using BillingService.Application.Services;
using BillingService.Domain.Repositories;
using BillingService.Infrastructure.Repositories;
using Framework.Repositories;
using Framework.Repositories.Implementations;
using MyApp.Middleware;
using SGen.Framework.Web.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:6118");

// Register services
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddSingleton<IInvoiceRepository, InMemoryInvoiceRepository>();
// Generic repository mapping
builder.Services.AddScoped(typeof(IQueryRepository<,>), typeof(QueryRepository<,>));
builder.Services.AddScoped(typeof(ICrudRepository<,>), typeof(CrudRepository<,>));
builder.Services.AddScoped(typeof(ICommandRepository<,>), typeof(CommandRepository<,>));
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ITaxService, TaxService>();
builder.Services.AddScoped<ITaxGroupService, TaxGroupService>();
builder.Services.AddScoped<ITaxGroupIgstDetailService, TaxGroupIgstDetailService>();
builder.Services.AddScoped<ITaxGroupSgstDetailService, TaxGroupSgstDetailService>();
builder.Services.AddScoped<ISupplierPaymentService, SupplierPaymentService>();
builder.Services.AddScoped<ISupplierReturnService, SupplierReturnService>();
builder.Services.AddScoped<ISaleInvoicePaymentService, SaleInvoicePaymentService>();
builder.Services.AddScoped<ISaleItemMasterSecvice, SaleItemMasterService>();
builder.Services.AddScoped<ISaleMasterService, SaleMasterService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILedgerMasterService, LedgerMasterService>();
builder.Services.AddScoped<ISubLedgerService, SubLedgerService>();
builder.Services.AddScoped<ILedgerCategoryService, LedgerCategoryService>();
builder.Services.AddScoped<IVoucherService, VoucherService>();
builder.Services.AddScoped<ILedgerDetailService, LedgerDetailService>();

//builder.Services.AddScoped<IIdentityServices, IdentityServices>();

builder.Services.AddControllers()
.AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.ReferenceHandler =
         System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
     options.JsonSerializerOptions.DefaultIgnoreCondition =
         System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
     options.JsonSerializerOptions.WriteIndented = true;
 });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

// 🔥 Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// ✅ Call Infrastructure service registration
builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddAuthentication(builder.Configuration);
//builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();
app.UseRequestTracking();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });
//}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// ✅ This is required for controller-based routes
app.MapControllers();
app.Run();


