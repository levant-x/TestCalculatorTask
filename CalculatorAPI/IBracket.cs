namespace CalculatorAPI
{
    public enum BracketType
    {
        Opening = 1, Closing = -1
    }

    public interface IBracket
    {
        BracketType Type { get; }
    }
}