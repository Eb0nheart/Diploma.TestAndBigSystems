using BigSystems.Aflevering1.Models;

namespace BigSystems.Aflevering1.Repositories;

public interface ICardsRepository
{
    Card GetCard();
}

internal class CardsRepository : ICardsRepository
{
    private readonly IList<Card> _cards;
    private readonly Random _random;

    public CardsRepository()
    {
        _random = new();
        _cards = new List<Card>();
        CreateCards();
    }

    private void CreateCards()
    {
        for (int i = 1; i <= 13; i++)
        {
            _cards.Add(new StandardCard((CardNumber)i, CardColour.Hearts));
            _cards.Add(new StandardCard((CardNumber)i, CardColour.Spades));
            _cards.Add(new StandardCard((CardNumber)i, CardColour.Diamonds));
            _cards.Add(new StandardCard((CardNumber)i, CardColour.Clubs));
            if (i % 4 == 0)
            {
                _cards.Add(new JokerCard());
            }
        }
    }

    public Card GetCard() => _cards.ElementAt(_random.Next(0, 54));
}
