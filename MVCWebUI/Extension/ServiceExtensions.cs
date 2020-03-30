using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCWebUI;
using MVCWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HillLabTest.Extension
{
    public static class ServiceExtensions
    {
        public static void ConfigureHttpClientWrapper(this IServiceCollection services)
        {
            services.AddScoped<IProductHttpClientWrapper, ProductHttpClientWrapper>();
        }

    }
}
