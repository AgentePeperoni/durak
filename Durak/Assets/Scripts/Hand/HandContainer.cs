using System;
using System.Collections.Generic;
using UnityEngine;

public class HandContainer : MonoBehaviour, IContainCards
{
    #region Events
    public event Action<CardController> OnCardInteracted;
    public event Action<bool> OnContainerLockStateChanged;
    #endregion

    #region Serialized fields
    [SerializeField]
    private Transform _root;
    [SerializeField]
    private float _cardSpacing;
    #endregion

    #region Public properties
    public List<CardController> Cards { get; private set; }
    public bool Locked { get; private set; }
    public bool HideCards { get; set; }
    #endregion

    private void Awake()
    {
        Cards = new List<CardController>();

        if (_root == null)
            _root = transform;

        Locked = false;
    }

    private void OnCardInteractedWrapper(CardController card)
    {
        OnCardInteracted?.Invoke(card);
    }

    #region Public methods
    public void AddCard(CardController card)
    {
        if (!Cards.Contains(card) && card != null)
        {
            Cards.Add(card);

            if (HideCards)
                card.Graphics.FaceDown();
            else
                card.Graphics.FaceUp();

            card.transform.rotation = Quaternion.identity;
            card.Interaction.OnMouseDownOccur += OnCardInteractedWrapper;

            if (Locked)
                card.Lock();
            
            ArrangeCards();
        }
    }

    public void RemoveCard(CardController card)
    {
        if (card != null)
        {
            Cards.Remove(card);
            card.Graphics.SetSortingLayer(10);
            card.Interaction.OnMouseDownOccur -= OnCardInteractedWrapper;

            if (Locked)
                card.Unlock();
            
            ArrangeCards();
        }
    }

    public void ArrangeCards()
    {
        float spacingPos = -_cardSpacing * Mathf.Floor(Cards.Count / 2f);

        int layer = 1;
        for (int i = 0; i < Cards.Count; ++i)
        {
            CardController card = Cards[i];

            card.Graphics.SetSortingLayer(layer);
            card.transform.position = new Vector3(spacingPos, _root.position.y, _root.position.z);

            spacingPos += _cardSpacing;
            layer += 2;
        }
    }

    public void Lock()
    {
        foreach (CardController c in Cards)
            c.Lock();

        Locked = true;
        OnContainerLockStateChanged?.Invoke(Locked);
    }

    public void Unlock()
    {
        foreach (CardController c in Cards)
            c.Unlock();

        Locked = false;
        OnContainerLockStateChanged?.Invoke(Locked);
    }
    #endregion
}
