using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CardInteraction : MonoBehaviour
{
    #region Events
    public event Action<CardController> OnMouseEnterOccur;
    public event Action<CardController> OnMouseDownOccur;
    public event Action<CardController> OnMouseDragOccur;
    public event Action<CardController> OnMouseUpOccur;
    public event Action<CardController> OnMouseExitOccur;
    #endregion

    public bool IsActive { get; protected set; }

    #region Protected fields
    protected BoxCollider2D _collider2D;
    protected CardController _reference;
    #endregion

    public virtual void Initialize(CardController reference, Vector2 extents)
    {
        _reference = reference;
        UpdateInteraction(extents);
    }

    public virtual void UpdateInteraction(Vector2 extents)
    {
        _collider2D.size = extents * 2;
    }

    public virtual void SetActive(bool value) => IsActive = value;

    #region Protected MonoBehaviour Methods
    protected virtual void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        IsActive = true;
    }

    protected virtual void OnMouseEnter()
    {
        if (IsActive)
            OnMouseEnterOccur?.Invoke(_reference);
    }

    protected virtual void OnMouseDown()
    {
        if (IsActive)
            OnMouseDownOccur?.Invoke(_reference);
    }

    protected virtual void OnMouseDrag()
    {
        if (IsActive)
            OnMouseDragOccur?.Invoke(_reference);
    }

    protected virtual void OnMouseUp()
    {
        if (IsActive)
            OnMouseUpOccur?.Invoke(_reference);
    }

    protected virtual void OnMouseExit()
    {
        if (IsActive)
            OnMouseExitOccur?.Invoke(_reference);
    }
    #endregion
}
