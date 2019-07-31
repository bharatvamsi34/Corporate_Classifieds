using Classifieds.App.Services.Core.Repositories;
using Classifieds.App.Services.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Classifieds.App.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryCollection(this IServiceCollection services)
        {
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IFieldsRepository, FieldsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IInboxRepository, InboxRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();

            return services;
        }
    }
}
