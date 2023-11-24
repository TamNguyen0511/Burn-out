namespace _Game.Scripts.Enums
{
    /// <summary>
    /// This enum show the state of ingredient, which will be passive
    /// </summary>
    [System.Flags]
    public enum IngredientState
    {
        None = 1 << 0,
        Raw = 1 << 1,
        Burned = 1 << 2,
        Cut = 1 << 3,
        Grill = 1 << 4,
        Fry = 1 << 5,
        Steam = 1 << 6,
    }
}