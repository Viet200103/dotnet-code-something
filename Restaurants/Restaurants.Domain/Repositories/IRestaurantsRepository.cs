using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllSync();
    Task<Restaurant?> GetByIdAsync(int id);

    Task<int> Create(Restaurant entity);
}
