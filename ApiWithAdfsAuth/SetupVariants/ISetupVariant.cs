using Microsoft.Extensions.DependencyInjection;

namespace ApiWithAdfsAuth.SetupVariants
{
    public interface ISetupVariant
    {
        IServiceCollection ConfigureServices(IServiceCollection services);
    }
}
