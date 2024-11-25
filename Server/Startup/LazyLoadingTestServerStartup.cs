using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using LLM.Module.LazyLoadingTest.Repository;
using LLM.Module.LazyLoadingTest.Services;

namespace LLM.Module.LazyLoadingTest.Startup
{
    public class LazyLoadingTestServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ILazyLoadingTestService, ServerLazyLoadingTestService>();
            services.AddDbContextFactory<LazyLoadingTestContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
