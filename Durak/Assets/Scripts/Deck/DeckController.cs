using System.Linq;
using UnityEngine;

public class DeckController : MonoBehaviour
{
    [SerializeField]
    protected DeckData _data;
    [SerializeField]
    protected Transform _trumpRoot;

    [Space]
    [SerializeField]
    protected GameObject _cardPrefab;

    public DeckContainer Container { get; protected set; }
    public DeckGraphics Graphics { get; protected set; }
    public CardSuit TrumpSuit { get; protected set; }

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
        Container = GetComponent<DeckContainer>() ?? GetComponentInChildren<DeckContainer>();
        Graphics = GetComponent<DeckGraphics>() ?? GetComponentInChildren<DeckGraphics>();
    }

    protected virtual void InitializeComponents()
    {
        int sortingLayer = -_data.runtimeCards.Count * 2;
        foreach (CardData data in _data.runtimeCards)
        {
            GameObject cardObj = Instantiate(_cardPrefab, transform.position, transform.rotation);
            CardController card = cardObj.GetComponent<CardController>() ?? cardObj.GetComponentInChildren<CardController>();

            card.Data = data;
            card.Graphics.SetSortingLayer(sortingLayer);
            card.Graphics.FaceDown();
            card.Lock();

            Container.AddCard(card);
            sortingLayer += 2;
        }

        Shuffle(10);
        PickTrump();
        OrderSortingLayers();

        Graphics.ShowSuit(TrumpSuit);
    }

    protected virtual void PickTrump()
    {
        CardController trumpCard = Container.Cards[0];

        trumpCard.transform.SetPositionAndRotation(_trumpRoot.position, _trumpRoot.rotation);
        trumpCard.Graphics.FaceUp();

        TrumpSuit = trumpCard.Data.runtimeSuit;

        int highestPriority = Container.Cards.Max(card => card.Data.runtimePriority);
        foreach (CardController trump in Container.Cards.Where(card => card.Data.runtimeSuit == TrumpSuit))
            trump.Data.runtimePriority += highestPriority;
    }

    protected virtual void OrderSortingLayers()
    {
        int sortingLayer = -_data.runtimeCards.Count * 2;
        foreach (CardController card in Container.Cards)
        {
            card.Graphics.SetSortingLayer(sortingLayer);
            sortingLayer += 2;
        }
    }
    #endregion

    public virtual void Shuffle(int count)
    {
        for (int c = 0; c < count; ++c)
        {
            for (int i = Container.Cards.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);

                CardController temp = Container.Cards[i];
                Container.Cards[i] = Container.Cards[j];
                Container.Cards[j] = temp;
            }
        }
    }

    public virtual CardController Draw()
    {
        if (Container.Cards.Count > 0)
        {
            CardController drawnCard = Container.Cards[Container.Cards.Count - 1];
            Container.RemoveCard(drawnCard);

            return drawnCard;
        }

        return null;
    }
}
