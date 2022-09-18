using TestingBigSystems.Models;

public class StandardCard : Card
{
    public StandardCard(CardNumber number, CardColour colour)
    {
        Number = number;
        Colour = colour;
    }

    public CardNumber Number { get; }
    public CardColour Colour { get; }

    public override string DisplayName => $"{Number} of {Colour}";
}

public class JokerCard : Card
{
    public override string DisplayName => "Joker";
}

public abstract class Card
{
    public abstract string DisplayName { get; }
}