using System;
using AutoMapper;
using System.Text;
using Repositories;
using GroupStudyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using GroupStudyAPI.AutoMapper;
using BusinessObject.Sub_Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GroupStudyAPI
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

            services.AddControllers();
            services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IGroupMemberRepository, GroupMemberRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IJoinRequestRepository, JoinRequestRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var secretKey = Configuration["AppSettings:SecretKey"];
            var secertKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secertKeyBytes),
                    ClockSkew = TimeSpan.Zero
                };
            });



            //services.AddSwaggerGen(options =>
            // {
            //     options.AddSecurityDefinition("Oauth2", new OpenApiSecurityScheme
            //     {
            //         Description = "Standard Authorization header using Bearer",
            //         In = ParameterLocation.Header,
            //         Name = "Authorization",
            //         Type = SecuritySchemeType.ApiKey
            //     });
            //     options.OperationFilter<SecurityRequirementsOperationFilter>();
            // });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GroupStudyAPI", Version = "v1" });
            });
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                        .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                        .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);



        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GroupStudyAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
