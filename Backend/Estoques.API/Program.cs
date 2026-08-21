using Estoques.API.Services;
using Estoques.API.Validators;
using Estoques.Domain.Interfaces;
using Estoques.Infra.Data.Context;
using Estoques.Infra.Data.Repositories;
using Estoques.Service.DTOs;
using Estoques.Service.DTOs.Autenticacao;
using Estoques.Service.DTOs.Produto;
using Estoques.Service.DTOs.Usuario;
using Estoques.Service.Interfaces;
using Estoques.Service.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy("Desenvolvimento", policy => { policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod(); });
    options.AddPolicy("Producao", policy => { policy.WithOrigins(builder.Configuration["AllowedHosts"]!).AllowAnyHeader().AllowAnyMethod(); });
});

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var chaveSecreta = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chaveSecreta),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Emissor"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audiencia"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddDbContext<EstoquesDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAdquirenteRepository, AdquirenteRepository>();
builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IProdutoFabricanteRepository, ProdutoFabricanteRepository>();
builder.Services.AddScoped<IProdutoHistoricoRepository, ProdutoHistoricoRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoSituacaoRepository, ProdutoSituacaoRepository>();
builder.Services.AddScoped<IProdutoTipoRepository, ProdutoTipoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAdquirenteService, AdquirenteService>();
builder.Services.AddScoped<IFornecedorService, FornecedorService>();
builder.Services.AddScoped<IProdutoFabricanteService, ProdutoFabricanteService>();
builder.Services.AddScoped<IProdutoHistoricoService, ProdutoHistoricoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IProdutoSituacaoService, ProdutoSituacaoService>();
builder.Services.AddScoped<IProdutoTipoService, ProdutoTipoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IArmazenamentoService, ArmazenamentoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

builder.Services.AddScoped<IValidator<AutenticacaoEntradaDTO>, AutenticacaoEntradaDTOValidator>();
builder.Services.AddScoped<IValidator<AdquirenteDTO>, AdquirenteDTOValidator>();
builder.Services.AddScoped<IValidator<FornecedorDTO>, FornecedorDTOValidator>();
builder.Services.AddScoped<IValidator<ProdutoEntradaDTO>, ProdutoEntradaDTOValidator>();
builder.Services.AddScoped<IValidator<ProdutoFabricanteDTO>, ProdutoFabricanteDTOValidator>();
builder.Services.AddScoped<IValidator<ProdutoHistoricoDTO>, ProdutoHistoricoDTOValidator>();
builder.Services.AddScoped<IValidator<ProdutoSituacaoDTO>, ProdutoSituacaoDTOValidator>();
builder.Services.AddScoped<IValidator<ProdutoTipoDTO>, ProdutoTipoDTOValidator>();
builder.Services.AddScoped<IValidator<UsuarioEntradaDTO>, UsuarioEntradaDTOValidator>();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions { FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")), RequestPath = "/Uploads" });
app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.UseCors("Desenvolvimento");
}
if (!app.Environment.IsDevelopment())
{
    //app.UseHttpsRedirection();
    app.UseCors("Producao");
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();