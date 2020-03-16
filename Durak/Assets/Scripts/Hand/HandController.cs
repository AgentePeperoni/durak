using System;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public event Action<IContainCards, CardController> OnCardInteracted;

    public HandContainer Container { get; protected set; }
    public HandGraphics Graphics { get; protected set; }

    #region Protected MonoBehaviour methods
    protected virtual void Awake()
    {
        FindComponents();
    }

    protected virtual void Start()
    {
        InitializeComponents();
    }
    #endregion

    #region Protected methods
    protected virtual void FindComponents()
    {
        Container = GetComponent<HandContainer>() ?? GetComponentInChildren<HandContainer>();
        Graphics = GetComponent<HandGraphics>() ?? GetComponentInChildren<HandGraphics>();
    }

    protected virtual void InitializeComponents()
    {
        if (Container != null)
        {
            Container.OnCardInteracted += OnCardInteractedWrapper;
            Container.OnContainerLockStateChanged += OnLockStateChanged;
        }
    }

    protected virtual void OnCardInteractedWrapper(CardController card)
    {
        OnCardInteracted?.Invoke(Container, card);
    }

    protected virtual void OnLockStateChanged(bool value)
    {
        if (value)
            Graphics.HideActiveGraphics();
        else
            Graphics.ShowActiveGraphics();
    }
    #endregion
}
