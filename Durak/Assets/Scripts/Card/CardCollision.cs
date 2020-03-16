using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CardCollision : MonoBehaviour
{
    #region Events
    public event Action<IContainCards, CardController> OnTriggerEnterOccur;
    public event Action<IContainCards, CardController> OnTriggerExitOccur;
    #endregion

    protected CardController _reference;

    #region Protected MonoBehaviour methods
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IContainCards collidedContainer = collision.gameObject.GetComponent<IContainCards>();
        if (collidedContainer != null)
            OnTriggerEnterOccur?.Invoke(collidedContainer, _reference);
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        IContainCards collidedContainer = collision.gameObject.GetComponent<IContainCards>();
        if (collidedContainer != null)
            OnTriggerExitOccur?.Invoke(collidedContainer, _reference);
    }
    #endregion

    public virtual void Initialize(CardController reference)
    {
        _reference = reference;
    }
}
