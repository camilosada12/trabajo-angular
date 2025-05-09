using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Services;
using Entity.DTOs;
using Entity.Model;

namespace Web.Service_Extensions;

    public static class ServicesDependency
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            // repositorio genérico
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // servicios genéricos
            services.AddScoped(typeof(IGenericService<FormDto>), typeof(GenericService<FormDto, Form>));
            services.AddScoped(typeof(IGenericService<FormModuleDto>), typeof(GenericService<FormModuleDto, FormModule>));
            services.AddScoped(typeof(IGenericService<ModuleDto>), typeof(GenericService<ModuleDto, Module>));
            services.AddScoped(typeof(IGenericService<UserDto>), typeof(GenericService<UserDto, User>));
            services.AddScoped(typeof(IGenericService<RolUserDto>), typeof(GenericService<RolUserDto, RolUser>));
            services.AddScoped(typeof(IGenericService<rolDto>), typeof(GenericService<rolDto, rol>));
            services.AddScoped(typeof(IGenericService<PersonDto>), typeof(GenericService<PersonDto, Person>));
            services.AddScoped(typeof(IGenericService<PermissionDto>), typeof(GenericService<PermissionDto, Permission>));
            services.AddScoped(typeof(IGenericService<RolFormPermissionDto>), typeof(GenericService<RolFormPermissionDto, RolFormPermission>));

            return services;
        }
    }

