﻿using Application.Behavior;
using FluentValidation;
using Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IInvoiceRepository), typeof(InvoiceRepository));

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
