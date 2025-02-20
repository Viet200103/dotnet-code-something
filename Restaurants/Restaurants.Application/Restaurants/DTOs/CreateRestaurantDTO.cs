using System.ComponentModel.DataAnnotations;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.DTOs;

public class CreateRestaurantDTO
{
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Insert a valid category")]
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }


    [EmailAddress(ErrorMessage = "Please Provide a valid email address")]
    public string? ContactEmail { get; set; }

    [Phone(ErrorMessage = "Please provide a valid phone number")]
    public string? ContactNumber { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }

    [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Please provide a valid postal code (XX-XXX).")]
    public string? PostalCode { get; set; }

    public static Restaurant toEntity(CreateRestaurantDTO dto)
    {
        Address address = new Address()
        {
            Street = dto.Street,
            City = dto.City,
            PostalCode = dto.PostalCode,
        };


        Restaurant res = new Restaurant()
        {
            Name = dto.Name,
            Category = dto.Category,
            HasDelivery = dto.HasDelivery,
            Description = dto.Description,
            ContactEmail = dto.ContactEmail,
            ContactNumber = dto.ContactNumber,
            Address = address
        };

        return res;
    }
}
