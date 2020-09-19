using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BibliotecaOnlineAPI.Infraestructure;
using BibliotecaOnlineAPI.Repository;
using BibliotecaOnlineAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace BibliotecaOnlineAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILibrosRepository, LibrosRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddDbContext<BibliotecaDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddAutoMapper(typeof(BibliotecaOnlineAPI.Mapper.LibrosMapper));
            services.AddAutoMapper(typeof(BibliotecaOnlineAPI.Mapper.UsuariosMapper));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:TokenKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                };
            }
            );
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("APIBibliotecaLibros", new Microsoft.OpenApi.Models.OpenApiInfo()
                { 
                Title ="Biblioteca Online API", 
                Description = "Contiene los catalogos genericos de aplicaciones",
                Version = "1.0",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                { 
                Email ="soluciones@axsistec.com",
                Name = "Soporte tecnico de desarrollos",
                Url = new Uri("https://axsistecnologia.com"),

                },

                License = new Microsoft.OpenApi.Models.OpenApiLicense
                { 
                    Name = "BSD",
                    Url = new Uri("https://bsd.axsistec.com"),
                },
                
                }
                
                    );

                options.SwaggerDoc("APIBibliotecaUsuarios", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Biblioteca Online API",
                    Description = "Contiene los catalogos genericos de aplicaciones",
                    Version = "1.0",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "soluciones@axsistec.com",
                        Name = "Soporte tecnico de desarrollos",
                        Url = new Uri("https://axsistecnologia.com"),

                    },

                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "BSD",
                        Url = new Uri("https://bsd.axsistec.com"),
                    },

                }

                    );


                var XMLComentarios = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var APIRutaComentarios =Path.Combine(AppContext.BaseDirectory, XMLComentarios);
                options.IncludeXmlComments(APIRutaComentarios);

                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authentication",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer"
                    }
                    );
                options.AddSecurityRequirement(new OpenApiSecurityRequirement { 
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    } , new List<string>()
                }
                });


            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            
            app.UseSwagger( );
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/APIBibliotecaLibros/swagger.json", "API Libro Biblioteca");
                options.SwaggerEndpoint("/swagger/APIBibliotecaUsuarios/swagger.json", "API Usuario Biblioteca");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
