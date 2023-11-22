namespace _Game.Scripts.Enums
{
    [System.Flags]
    public enum KitchenCounterType
    {
        None = 0,
        FreeCounter = 1 << 1,
        IngredientCounter = 1 << 2,
        PreparationCounter = 1 << 3,
        CookCounter = 1 << 4,
        ServeCounter = 1 << 5
    }
}