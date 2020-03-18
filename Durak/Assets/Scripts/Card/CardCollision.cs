using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CardCollision : MonoBehaviour
{
    #region Events
    public event Action<IContainCards, CardController> OnTriggerEnterOccur;
    public event Action<IContainCards, CardController> OnTriggerExitOccur;
    #endregion

    private CardController _reference;

    #region Private MonoBehaviour methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IContainCards collidedContainer = collision.gameObject.GetComponent<IContainCards>();
        if (collidedContainer != null)
            OnTriggerEnterOccur?.Invoke(collidedContainer, _reference);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IContainCards collidedContainer = collision.gameObject.GetComponent<IContainCards>();
        if (collidedContainer != null)
            OnTriggerExitOccur?.Invoke(collidedContainer, _reference);
    }
    #endregion

    public void Initialize(CardController reference)
    {
        _reference = reference;
    }
}
