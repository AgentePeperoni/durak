using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Objects/Card", order = 2)]
public class CardData : ScriptableObject, ISerializationCallbackReceiver
{
    #region Serialized fields
    [SerializeField]
    protected int _priority;
    [SerializeField]
    protected CardSuit _suit;
    [SerializeField]
    protected Sprite _face;
    [SerializeField]
    protected Sprite _back;
    #endregion

    #region Public fields
    [NonSerialized]
    public int runtimePriority;
    [NonSerialized]
    public CardSuit runtimeSuit;
    [NonSerialized]
    public Sprite runtimeFace;
    [NonSerialized]
    public Sprite runtimeBack;
    #endregion

    public void Reset()
    {
        runtimePriority = _priority;
        runtimeSuit = _suit;
        runtimeFace = _face;
        runtimeBack = _back;
    }

    public virtual void OnAfterDeserialize() => Reset();
    public virtual void OnBeforeSerialize() { }
}

public enum CardSuit
{
    Clubs,
    Diamonds,
    Hearts,
    Spades
}
