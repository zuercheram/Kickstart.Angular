using Kickstart.Angular.Shared;

namespace Kickstart.Angular.Services;

public interface IDrinkService
{
    Task<IList<DrinkModel>> GetAllDrinksAsync();
    Task<DrinkModel> GetDrinkByIdAsync(int id);
    Task<int> CreateDrinkAsync(DrinkModel drinkModel);
    Task UpdateDrinkAsync(DrinkModel drinkModel);
}
