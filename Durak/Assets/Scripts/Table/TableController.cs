using System;
using UnityEngine;

public class TableController : MonoBehaviour
{
    #region Events
    public event Action OnAttackDone;
    public event Action OnDefenseDone;
    #endregion

    #region Serialized fields
    [SerializeField]
    protected Transform _attackingCardRoot;
    [SerializeField]
    protected Transform _defendingCardRoot;
    #endregion

    #region Public properties
    public TableContainer Container { get; protected set; }

    public CardController AttackCard { get; protected set; }
    public CardController DefendCard { get; protected set; }
    #endregion

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
        Container = GetComponent<TableContainer>() ?? GetComponentInChildren<TableContainer>();
    }

    protected virtual void InitializeComponents()
    {
        if (Container != null)
        {
            Container.OnCardAdded += ManageAddedCard;
            Container.OnCardRemoved += ManageRemovedCard;
        }
    }

    protected virtual void ManageAddedCard(CardController card)
    {
        if (GameManager.GameState == GameState.Attack)
        {
            AttackCard = card;
            AttackCard.transform.position = _attackingCardRoot.position;

            OnAttackDone?.Invoke();
        }
        else if (GameManager.GameState == GameState.Defense)
        {
            DefendCard = card;
            DefendCard.transform.position = _defendingCardRoot.position;

            OnDefenseDone?.Invoke();
        }
    }

    protected virtual void ManageRemovedCard(CardController card)
    {
        if (card.Equals(AttackCard))
            AttackCard = null;
        else if (card.Equals(DefendCard))
            DefendCard = null;
    }
    #endregion
}