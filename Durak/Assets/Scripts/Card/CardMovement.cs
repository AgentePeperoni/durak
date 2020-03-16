using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public bool keepMouseOffset;

    protected Vector2 _offset = Vector2.zero;

    #region Public methods
    public virtual void MouseDown()
    {
        if (keepMouseOffset)
            _offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public virtual void MouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
    }

    public virtual void MouseUp()
    {
        _offset = Vector2.zero;
    }
    #endregion
}
