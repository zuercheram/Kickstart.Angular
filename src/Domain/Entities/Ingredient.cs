using System.ComponentModel.DataAnnotations;

namespace Kickstart.Angular.Domain.Entities;

public class Ingredient : EntityTrackingBase
{
    public int IngredientId { get; set; }

    [MaxLength(255)] public string Name { get; set; } = string.Empty;

    public List<DrinkIngredient> DrinkIngredients { get; set; } = new();
    public ICollection<Drink>? Drinks { get; set; }
}
