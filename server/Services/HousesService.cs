

namespace csharp_gregslist.Services;

public class HousesService
{
    private readonly HousesRepository _repository;

    public HousesService(HousesRepository repository)
    {
        _repository = repository;
    }


    internal List<House> GetHouses()
    {
        List<House> houses = _repository.GetHouses();
        return houses;
    }
    internal House GetHouseById(int houseId)
    {
        House house = _repository.GetHouseById(houseId);
        if (house == null)
        {
            throw new Exception($"Invalid Id: {houseId}");
        }
        return house;
    }
    internal House CreateHouse(House houseData)
    {
        House house = _repository.CreateHouse(houseData);
        return house;
    }

    internal string DestroyHouse(int houseId)
    {
        House house = GetHouseById(houseId);
        _repository.DestroyHouse(houseId);
        return $"{house.Id} Has Been Taken Off Gregslist";
    }

    internal House UpdateHouse(int houseId, House houseData)
    {
        House originalHouse = GetHouseById(houseId);

        originalHouse.Sqft = houseData.Sqft ?? originalHouse.Sqft;
        originalHouse.Bedrooms = houseData.Bedrooms ?? originalHouse.Bedrooms;
        originalHouse.Bathrooms = houseData.Bathrooms ?? originalHouse.Bathrooms;
        originalHouse.ImgUrl = houseData.ImgUrl ?? originalHouse.ImgUrl;
        originalHouse.Description = houseData.Description ?? originalHouse.Description;
        originalHouse.Price = houseData.Price ?? originalHouse.Price;

        _repository.UpdateHouse(originalHouse);
        return originalHouse;
    }
}