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

    public bool IsActive { get; private set; }

    #region Private fields
    private BoxCollider2D _collider2D;
    private CardController _reference;
    #endregion

    #region Private MonoBehaviour Methods
    private void Awake()
    {
        _collider2D = GetComponent<BoxCollider2D>();
        IsActive = true;
    }

    private void OnMouseEnter()
    {
        if (IsActive)
            OnMouseEnterOccur?.Invoke(_reference);
    }

    private void OnMouseDown()
    {
        if (IsActive)
            OnMouseDownOccur?.Invoke(_reference);
    }

    private void OnMouseDrag()
    {
        if (IsActive)
            OnMouseDragOccur?.Invoke(_reference);
    }

    private void OnMouseUp()
    {
        if (IsActive)
            OnMouseUpOccur?.Invoke(_reference);
    }

    private void OnMouseExit()
    {
        if (IsActive)
            OnMouseExitOccur?.Invoke(_reference);
    }
    #endregion

    #region Public methods
    public void Initialize(CardController reference, Vector2 extents)
    {
        _reference = reference;
        UpdateInteraction(extents);
    }

    public void UpdateInteraction(Vector2 extents)
    {
        _collider2D.size = extents * 2;
    }

    public void SetActive(bool value) => IsActive = value;
    #endregion
}
