﻿using PersonalPageWASM.Services;

namespace PersonalPageWASM.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<GitHubService>();
            services.AddScoped<BlogService>();
            services.AddScoped<TetrisGameService>();

            return services;
        }
    }
}
