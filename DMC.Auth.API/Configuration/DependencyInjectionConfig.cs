using DMC.Auth.API.Application.Commands;
using DMC.Auth.API.Application.Events;
using DMC.Auth.API.Application.Queries;
using DMC.Auth.API.Data;
using DMC.Auth.API.Data.Repository;
using DMC.Core.Communication.Mediator;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DMC.Auth.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<AuthContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserQueries, UserQueries>();


            #region Mediator Command
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterUserCommand, ValidationResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, ValidationResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, ValidationResult>, UserCommandHandler>();
            #endregion

            #region Mediator Event
            services.AddScoped<INotificationHandler<RegisteredUserEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedUserEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<DeletedUserEvent>, UserEventHandler>();
            #endregion

        }
    }
}