using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetReactTemplateAdmin.Configuration
{
    public static class InitializeDatabaseConfig
    {
        public static void InitializeDatabase(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var installationService = serviceProvider.GetRequiredService<IInstallationService>();
            installationService.InstallData("en");
        }
    }
}