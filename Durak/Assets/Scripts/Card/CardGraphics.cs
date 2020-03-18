using UnityEngine;

public class CardGraphics : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    private SpriteRenderer _faceRender;
    [SerializeField]
    private SpriteRenderer _backRender;
    #endregion

    #region Public properties
    public bool FacingUp { get; protected set; }
    public bool IsVisible { get; protected set; }
    #endregion

    #region Private MonoBehaviour methods
    private void OnValidate()
    {
        if (_faceRender == null)
            Debug.LogWarning($"SpriteRenderer лицевой стороны карты на объекте \"{gameObject.name}\" не задан!");
        if (_backRender == null)
            Debug.LogWarning($"SpriteRenderer задней стороны карты на объекте \"{gameObject.name}\" не задан!");
    }

    private void Awake()
    {
        IsVisible = true;
    }
    #endregion

    #region Public methods
    public void Initialize(CardData data)
    {
        UpdateGraphics(data);
    }

    public void UpdateGraphics(CardData data)
    {
        _faceRender.sprite = data.runtimeFace;
        _backRender.sprite = data.runtimeBack;
    }

    public void Show()
    {
        IsVisible = true;

        if (FacingUp)
            FaceUp();
        else
            FaceDown();
    }

    public void Hide()
    {
        IsVisible = false;

        _faceRender.enabled = IsVisible;
        _backRender.enabled = IsVisible;
    }

    public void FaceUp()
    {
        if (IsVisible)
        {
            _faceRender.enabled = true;
            _backRender.enabled = false;
        }

        FacingUp = true;
    }

    public void FaceDown()
    {
        if (IsVisible)
        {
            _faceRender.enabled = false;
            _backRender.enabled = true;
        }

        FacingUp = false;
    }

    public void SetSortingLayer(int faceLayer)
    {
        _faceRender.sortingOrder = faceLayer;
        _backRender.sortingOrder = faceLayer - 1;
    }

    public int GetSortingLayer() => _faceRender.sortingOrder;
    #endregion
}
