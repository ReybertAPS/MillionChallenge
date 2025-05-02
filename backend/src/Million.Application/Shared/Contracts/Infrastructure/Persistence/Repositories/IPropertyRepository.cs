using Million.Domain.Entities;

namespace Million.Application.Shared.Contracts.Infrastructure.Persistence.Repositories;

public interface IPropertyRepository
{
    Task<(List<Property> Properties, long TotalCount)> GetAllPaginatedAsync(int pageIndex, int pageSize);
    Task<(List<Property> Properties, long TotalCount)> GetFilteredPaginatedAsync(string? name, string? address, decimal? minPrice, decimal? maxPrice, int pageIndex, int pageSize);
    Task<Property?> GetByIdAsync(string id);
}
