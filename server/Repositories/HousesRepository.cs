




namespace csharp_gregslist.Repositories;

public class HousesRepository
{
    private readonly IDbConnection _db;

    public HousesRepository(IDbConnection db)
    {
        _db = db;
    }


    internal List<House> GetHouses()
    {
        string sql = "SELECT * FROM houses;";
        List<House> houses = _db.Query<House>(sql).ToList();
        return houses;
    }
    internal House GetHouseById(int houseId)
    {
        string sql = "SELECT * FROM houses WHERE id = @houseId;";
        House house = _db.Query<House>(sql, new { houseId }).FirstOrDefault();
        return house;
    }
    internal House CreateHouse(House houseData)
    {
        string sql = @"INSERT INTO houses (
        sqft,
        bedrooms,
        bathrooms,
        imgUrl,
        description,
        price)
        VALUES (
            @Sqft,
            @Bedrooms,
            @Bathrooms,
            @ImgUrl,
            @Description,
            @Price);
            
            SELECT * FROM houses WHERE id = LAST_INSERT_ID();";

        House house = _db.Query<House>(sql, houseData).FirstOrDefault();
        return house;
    }
    internal void DestroyHouse(int houseId)
    {
        string sql = "DELETE FROM houses WHERE id = @houseId LIMIT 1;";
        _db.Execute(sql, new { houseId });
    }

    internal void UpdateHouse(House originalHouse)
    {
        string sql = @"
        UPDATE houses
        SET
        sqft = @sqft,
        bedrooms = @Bedrooms,
        bathrooms = @Bathrooms,
        imgUrl = @imgUrl,
        description = @Description,
        price = @Price
        WHERE id = @Id
        ;";

        _db.Execute(sql, originalHouse);
    }
}