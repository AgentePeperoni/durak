using UnityEngine;

public class CardGraphics : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    protected SpriteRenderer _faceRender;
    [SerializeField]
    protected SpriteRenderer _backRender;
    #endregion

    #region Public properties
    public bool FacingUp { get; protected set; }
    public bool IsVisible { get; protected set; }
    #endregion
    
    protected virtual void OnValidate()
    {
        if (_faceRender == null)
            Debug.LogWarning($"SpriteRenderer лицевой стороны карты на объекте \"{gameObject.name}\" не задан!");
        if (_backRender == null)
            Debug.LogWarning($"SpriteRenderer задней стороны карты на объекте \"{gameObject.name}\" не задан!");
    }

    protected virtual void Awake()
    {
        IsVisible = true;
    }

    #region Public methods
    public virtual void Initialize(CardData data)
    {
        UpdateGraphics(data);
    }

    public virtual void UpdateGraphics(CardData data)
    {
        _faceRender.sprite = data.runtimeFace;
        _backRender.sprite = data.runtimeBack;
    }

    public virtual void Show()
    {
        IsVisible = true;

        if (FacingUp)
            FaceUp();
        else
            FaceDown();
    }

    public virtual void Hide()
    {
        IsVisible = false;

        _faceRender.enabled = IsVisible;
        _backRender.enabled = IsVisible;
    }

    public virtual void FaceUp()
    {
        if (IsVisible)
        {
            _faceRender.enabled = true;
            _backRender.enabled = false;
        }

        FacingUp = true;
    }

    public virtual void FaceDown()
    {
        if (IsVisible)
        {
            _faceRender.enabled = false;
            _backRender.enabled = true;
        }

        FacingUp = false;
    }

    public virtual void SetSortingLayer(int faceLayer)
    {
        _faceRender.sortingOrder = faceLayer;
        _backRender.sortingOrder = faceLayer - 1;
    }

    public virtual int GetSortingLayer() => _faceRender.sortingOrder;
    #endregion
}
