using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepByStepReact.Infrastructure.BackendFolders
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddBackendFolders(this IMvcBuilder services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddRazorOptions(o =>
            {
                o.ViewLocationExpanders.Add(new BackendViewLocationExpander());
            });

            return services;
        }
    }
}
