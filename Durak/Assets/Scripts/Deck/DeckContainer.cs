using System.Collections.Generic;
using UnityEngine;

public class DeckContainer : MonoBehaviour, IContainCards
{
    public List<CardController> Cards { get; protected set; }

    protected virtual void Awake()
    {
        Cards = new List<CardController>();
    }

    public virtual void AddCard(CardController card)
    {
        if (!Cards.Contains(card))
            Cards.Add(card);
    }

    public virtual void RemoveCard(CardController card)
    {
        Cards.Remove(card);
    }
}
