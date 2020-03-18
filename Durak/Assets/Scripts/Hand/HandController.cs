using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public event Action<IContainCards, CardController> OnCardInteracted;

    #region Public properties
    public HandContainer Container { get; private set; }
    public HandGraphics Graphics { get; private set; }
    #endregion

    #region Private MonoBehaviour methods
    private void Awake()
    {
        FindComponents();
    }

    private void Start()
    {
        InitializeComponents();
    }
    #endregion

    #region Private methods
    private void FindComponents()
    {
        Container = GetComponent<HandContainer>() ?? GetComponentInChildren<HandContainer>();
        Graphics = GetComponent<HandGraphics>() ?? GetComponentInChildren<HandGraphics>();
    }

    private void InitializeComponents()
    {
        if (Container != null)
        {
            Container.OnCardInteracted += OnCardInteractedWrapper;
            Container.OnContainerLockStateChanged += OnLockStateChanged;
        }
    }

    private void OnCardInteractedWrapper(CardController card)
    {
        OnCardInteracted?.Invoke(Container, card);
    }

    private void OnLockStateChanged(bool value)
    {
        if (value)
            Graphics.HideActiveGraphics();
        else
            Graphics.ShowActiveGraphics();
    }
    #endregion
}
