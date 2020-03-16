using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CardCollision : MonoBehaviour
{
    public event Action<IContainCards, CardController> OnTriggerEnterOccur;
    public event Action<IContainCards, CardController> OnTriggerExitOccur;

    protected CardController _reference;
    
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

    public virtual void Initialize(CardController reference)
    {
        _reference = reference;
    }
}
