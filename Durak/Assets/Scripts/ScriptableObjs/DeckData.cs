using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Objects/Deck", order = 1)]
public class DeckData : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    protected CardData[] _cards;

    [NonSerialized]
    public List<CardData> runtimeCards;

    public void Reset() => runtimeCards = new List<CardData>(_cards);

    public virtual void OnAfterDeserialize() => Reset();
    public virtual void OnBeforeSerialize() { }
}
