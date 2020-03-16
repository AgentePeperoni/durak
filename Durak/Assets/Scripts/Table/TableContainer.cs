using System;
using System.Collections.Generic;
using UnityEngine;

public class TableContainer : MonoBehaviour, IContainCards
{
    public event Action<CardController> OnCardAdded;
    public event Action<CardController> OnCardRemoved;

    public List<CardController> Cards { get; protected set; }

    protected virtual void Awake()
    {
        Cards = new List<CardController>();
    }

    public void AddCard(CardController card)
    {
        if (!Cards.Contains(card))
        {
            Cards.Add(card);
            card.Lock();
            card.Graphics.FaceUp();

            OnCardAdded?.Invoke(card);
        }
    }

    public void RemoveCard(CardController card)
    {
        if (Cards.Remove(card))
        {
            card.Unlock();

            OnCardRemoved?.Invoke(card);
        }
    }
}
