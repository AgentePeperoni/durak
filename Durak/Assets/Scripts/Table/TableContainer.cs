using System;
using System.Collections.Generic;
using UnityEngine;

public class TableContainer : MonoBehaviour, IContainCards
{
    #region Events
    public event Action<CardController> OnCardAdded;
    public event Action<CardController> OnCardRemoved;
    #endregion

    public List<CardController> Cards { get; protected set; }

    private void Awake()
    {
        Cards = new List<CardController>();
    }

    #region Public methods
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
    #endregion
}
