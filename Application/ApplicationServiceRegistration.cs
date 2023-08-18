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
            services.AddMediatR(configuration =>
            {
                // RegisterServicesFromAssembly -> Mediatr tüm assembly'i tarayarak commands ve response'ları bulup kendi listesine ekler. 
                // Bu listeye görede ileride bir command isteği gelirse ilgili handler'ı çalıştırır.
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); 
            });

            return services;
        }
    }
}
