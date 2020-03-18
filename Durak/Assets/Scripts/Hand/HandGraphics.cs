using UnityEngine;

public class HandGraphics : MonoBehaviour
{
    [SerializeField]
    private GameObject _activeGraphics;

    #region Public methods
    public void ShowActiveGraphics()
    {
        _activeGraphics.SetActive(true);
    }

    public void HideActiveGraphics()
    {
        _activeGraphics.SetActive(false);
    }
    #endregion
}
