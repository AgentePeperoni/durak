using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public bool keepMouseOffset;

    private Vector2 _offset = Vector2.zero;

    #region Public methods
    public void MouseDown()
    {
        if (keepMouseOffset)
            _offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void MouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + _offset;
    }

    public void MouseUp()
    {
        _offset = Vector2.zero;
    }
    #endregion
}
