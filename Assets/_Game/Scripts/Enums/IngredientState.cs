namespace _Game.Scripts.Enums
{
    /// <summary>
    /// This enum show the state of ingredient, which will be passive
    /// </summary>
    [System.Flags]
    public enum IngredientState
    {
        // None = 1 << 0,
        Raw = 1 << 0,
        Burned = 1 << 1,
        Cut = 1 << 2,
        Grill = 1 << 3,
        Fry = 1 << 4,
        Steam = 1 << 5,
    }
}