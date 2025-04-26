using PersonDirectoryApi.Dtos;
using PersonDirectoryApi.Persistence.Repositories;

namespace PersonDirectoryApi.Services;

public interface ICityService
{
    Task<List<CityDto>> GetAsync(CitySearchDto citySearchDto, CancellationToken cancellationToken);
}

public class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;

    public CityService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CityDto>> GetAsync(CitySearchDto citySearchDto, CancellationToken cancellationToken)
    {
        var cities = await _unitOfWork.Cities.GetAllAsync(citySearchDto.PageNumber, citySearchDto.PageSize, cancellationToken);
        
        return cities.Select(city => new CityDto(city.Id, city.Name)).ToList();
    }
}