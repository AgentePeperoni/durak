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
    private Transform _attackingCardRoot;
    [SerializeField]
    private Transform _defendingCardRoot;
    #endregion

    #region Public properties
    public TableContainer Container { get; private set; }

    public CardController AttackCard { get; private set; }
    public CardController DefendCard { get; private set; }
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
        Container = GetComponent<TableContainer>() ?? GetComponentInChildren<TableContainer>();
    }

    private void InitializeComponents()
    {
        if (Container != null)
        {
            Container.OnCardAdded += ManageAddedCard;
            Container.OnCardRemoved += ManageRemovedCard;
        }
    }

    private void ManageAddedCard(CardController card)
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

    private void ManageRemovedCard(CardController card)
    {
        if (card.Equals(AttackCard))
            AttackCard = null;
        else if (card.Equals(DefendCard))
            DefendCard = null;
    }
    #endregion
}