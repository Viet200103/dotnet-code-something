using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(
    IRestaurantsRepository restaurantsRepository,
    ILogger<RestaurantsService> logger
): IRestaurantsService {
    public async Task<int> Create(CreateRestaurantDTO createRestaurantDTO)
    {
        logger.LogInformation("Creating a new restaurant");
        var restaurant = CreateRestaurantDTO.toEntity(createRestaurantDTO);
        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }

    public async Task<IEnumerable<RestaurantDTO>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllSync();
        var restaurantsDTO = restaurants.Select(r => RestaurantDTO.FromEntity(r));
        return restaurantsDTO;
    }

    public async Task<RestaurantDTO?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant {id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
        return RestaurantDTO.FromEntity(restaurant);
    }
}
