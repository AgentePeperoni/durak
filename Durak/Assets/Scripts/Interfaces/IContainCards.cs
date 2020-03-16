using System.Collections.Generic;

public interface IContainCards
{
    List<CardController> Cards { get; }

    void AddCard(CardController card);
    void RemoveCard(CardController card);
}