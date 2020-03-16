using UnityEngine;

public class HandGraphics : MonoBehaviour
{
    [SerializeField]
    protected GameObject _activeGraphics;

    #region Public methods
    public virtual void ShowActiveGraphics()
    {
        _activeGraphics.SetActive(true);
    }

    public virtual void HideActiveGraphics()
    {
        _activeGraphics.SetActive(false);
    }
    #endregion
}
