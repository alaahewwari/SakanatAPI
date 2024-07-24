using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using Apartment = DataAccess.Models.Apartment;
namespace BusinessLogic.Repositories.Implementations;
public class ApartmentRepository(
    ApplicationDbContext context,
    ISieveProcessor sieveProcessor,
    IMapper mapper)
    : IApartmentRepository{
    public async Task<PagedResult<ApartmentOverviewResponseDto>> GetAllApartmentsAsync(SieveModel sieveModel)
    {
        sieveModel.SetDefaultPagination();
        var apartmentsQuery = context.Apartments.AsNoTracking();

        var filteredApartments = sieveProcessor.Apply(sieveModel, apartmentsQuery, applyPagination: false);
        var totalCount = await filteredApartments.CountAsync();
        
        var paginatedApartments = sieveProcessor.Apply(sieveModel, apartmentsQuery);
        var projectedApartments = await paginatedApartments
            .ProjectTo<ApartmentOverviewResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new PagedResult<ApartmentOverviewResponseDto>(
            projectedApartments,
            totalCount,
            sieveModel.Page!.Value,
            sieveModel.PageSize!.Value
            );
    }
    public async Task<Apartment?> CreateApartmentAsync(Apartment? apartment, ApplicationUser user)
    {
        var userApartments = await context.Apartments
                                      .Where(a => a.UserId == user.Id)
                                      .ToListAsync();
        if (userApartments.Any(a => a.Building == apartment.Building && a.ApartmentNumber == apartment.ApartmentNumber && a.FloorNumber == apartment.FloorNumber))
        {
            return null;
        }
        await context.Apartments.AddAsync(apartment);
        await context.SaveChangesAsync();
        return apartment;
    }
    public async Task<ApartmentResponseDto?> GetApartmentByIdAsync(Guid id)
    {
        var query = context.Apartments.AsNoTracking();
        var result = await query.
             ProjectTo<ApartmentResponseDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(a => a.Id == id);
        return result;
    }
    public async Task<IList<ApartmentResponseDto>?> GetApartmentsByUserId(Guid id, SieveModel sieveModel)
    {
        var query = context.Apartments
            .Where(a => a.UserId == id)
            .AsNoTracking();

        var result = await sieveProcessor.Apply(sieveModel, query)
            .ProjectTo<ApartmentResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return result;
    }
    public async Task<Apartment> UpdateApartmentAsync(Guid id, Apartment apartment)
    {
        var apartmentToUpdate = await context.Apartments.FirstOrDefaultAsync(a => a.Id == id);
        apartmentToUpdate.Name = apartment.Name;
        apartmentToUpdate.CreationDate = apartment.CreationDate;
        apartmentToUpdate.Price = apartment.Price;
        apartmentToUpdate.Description = apartment.Description;
        apartmentToUpdate.FurnishedStatus = apartment.FurnishedStatus;
        apartmentToUpdate.GenderAllowed = apartment.GenderAllowed;
        apartmentToUpdate.NumberOfBathrooms = apartment.NumberOfBathrooms;
        apartmentToUpdate.NumberOfRooms = apartment.NumberOfRooms;
        apartmentToUpdate.Region = apartment.Region;
        apartmentToUpdate.ApartmentNumber = apartment.ApartmentNumber;
        apartmentToUpdate.FloorNumber = apartment.FloorNumber;
        apartmentToUpdate.PriceCurrency = apartment.PriceCurrency;
        await context.SaveChangesAsync();
        return apartmentToUpdate;
    }
    public async Task<Apartment?> DeleteApartmentAsync(Guid id)
    {
        var apartment = await context.Apartments
            .FirstOrDefaultAsync(a => a.Id == id);
        if (apartment is not null)
        {
            context.Apartments.Remove(apartment);
            await context.SaveChangesAsync();
        }
        return apartment;
    }
    public async Task<IList<ApartmentImage>?> UploadApartmentImagesAsync(List<string> images, Guid id)
    {
        var apartmentImages = images.Select((image, index) => new ApartmentImage
        {
            ImagePath = image,
            ApartmentId = id,
            IsCover = true && index == 0
        }).ToList();
        await context.ApartmentImages.AddRangeAsync(apartmentImages);
        await context.SaveChangesAsync();
        return apartmentImages;
    }
    public async Task<ApartmentImageResponseDto> AddApartmentImageAsync(ApartmentImage image)
    {
        context.ApartmentImages.Add(image);
        await context.SaveChangesAsync();
        var result = await context.ApartmentImages
            .Where(ai => ai.Id == image.Id)
            .ProjectTo<ApartmentImageResponseDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return result!;
    }
    public async Task<IList<ApartmentImage>?> GetApartmentImagesAsync(Guid id)
    {
        return await context.ApartmentImages
            .Where(ai => ai.ApartmentId == id)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<IList<ApartmentOverviewResponseDto>> GetApartmentsByPriceAsync(decimal minPrice, decimal maxPrice)
    {
        var query = context.Apartments
            .Where(a => a.Price >= minPrice && a.Price <= maxPrice)
            .AsNoTracking();
        var result = await query
            .ProjectTo<ApartmentOverviewResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return result;
    }
    public async Task<bool> UpdateApartmentImageAsync(ApartmentImage image)
    {
        var apartmentImage = await context.ApartmentImages
            .FirstOrDefaultAsync(ai => ai.Id == image.Id);
        if (apartmentImage is not null)
        {
            apartmentImage.ImagePath = image.ImagePath;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<bool> DeleteApartmentImageAsync(Guid apartmentId, Guid imageId)
    {
        var apartmentImage = await context.Apartments.Where(a => a.Id == apartmentId)
             .SelectMany(a => a.ApartmentImages)
             .FirstOrDefaultAsync(ai => ai.Id == imageId);
        if (apartmentImage is not null)
        {
            context.ApartmentImages.Remove(apartmentImage);
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<ApartmentImage?> GetApartmentImageByIdAsync(Guid imageId)
    {
        var apartmentImage = await context.ApartmentImages.AsNoTracking()
            .FirstOrDefaultAsync(ai => ai.Id == imageId);
        return apartmentImage!;

    }
    public async Task<IList<ApartmentDiscountsResponseDto?>> GetAllDiscountsForApartmentsByUserIdAsync(Guid userId)
    {
        var result = await context.Apartments
         .AsNoTracking()
         .Where(a => a.UserId == userId)
         .ProjectTo<ApartmentDiscountsResponseDto>(mapper.ConfigurationProvider)
         .ToListAsync();

        return result;
    }
    public async Task<int> GetActiveApartmentWithDiscountsNumberAsync()
    {
        var result = await context.Apartments
            .AsNoTracking()
            .Where(a => a.IsVisible==true &&
            a.ApartmentDiscounts.Any())
            .CountAsync();
        return result;
    }
    public async Task<bool> ChangeApartmentAvailabilityAsync(Guid id)
    {
        var apartment = await context.Apartments
            .FirstOrDefaultAsync(a => a.Id == id);
        if (apartment is not null)
        {
            apartment.IsAvailable = !apartment.IsAvailable;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<bool> ChangeApartmentVisibilityAsync(Guid id)
    {
        var apartment = await context.Apartments
            .FirstOrDefaultAsync(a => a.Id == id);
        if (apartment is not null)
        {
            apartment.IsVisible = !apartment.IsVisible;
            await context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}