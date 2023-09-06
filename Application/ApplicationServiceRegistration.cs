using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

            // Validation middleware enabled
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(configuration =>
            {
                // RegisterServicesFromAssembly -> Mediatr tüm assembly'i tarayarak commands ve response'ları bulup kendi listesine ekler. 
                // Bu listeye görede ileride bir command isteği gelirse ilgili handler'ı çalıştırır.
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                // Mediatr'a bir request geldiğinde bunu validation middleware'inden geçirmesini sağlar.
                configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
                configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
                configuration.AddOpenBehavior(typeof(CachingBehaviour<,>));
            });

            return services;
        }

        // Assembly'de bulunan tüm BaseBusinessRules sınıflarını arayıp IoC'ye ekler.
       public static IServiceCollection AddSubClassesOfType(
           this IServiceCollection services,
           Assembly assembly,
           Type type,
           Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                if (addWithLifeCycle == null)
                    services.AddScoped(item);

                else
                    addWithLifeCycle(services, type);
            return services;
        }
    }
}
