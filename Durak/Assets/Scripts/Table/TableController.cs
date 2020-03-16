using System;
using UnityEngine;

public class TableController : MonoBehaviour
{
    public event Action OnAttackDone;
    public event Action OnDefenseDone;

    [SerializeField]
    protected Transform _attackingCardRoot;
    [SerializeField]
    protected Transform _defendingCardRoot;

    public TableContainer Container { get; protected set; }

    public CardController AttackCard { get; protected set; }
    public CardController DefendCard { get; protected set; }

    protected virtual void Awake()
    {
        FindComponents();   
    }

    protected virtual void Start()
    {
        InitializeComponents();
    }

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