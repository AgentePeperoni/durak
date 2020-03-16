using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckContainer : MonoBehaviour, IContainCards
{
    public event Action<int> OnCardCountChanged;

    public List<CardController> Cards { get; protected set; }

    protected virtual void Awake()
    {
        Cards = new List<CardController>();
    }

    #region Public methods
    public virtual void AddCard(CardController card)
    {
        if (!Cards.Contains(card))
        {
            Cards.Add(card);
            OnCardCountChanged?.Invoke(Cards.Count);
        }
    }

    public virtual void RemoveCard(CardController card)
    {
        if (Cards.Remove(card))
            OnCardCountChanged?.Invoke(Cards.Count);
    }
    #endregion
}
