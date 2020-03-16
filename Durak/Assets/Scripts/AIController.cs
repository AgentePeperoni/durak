using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    protected HandController _hand;

    public HandController Hand => _hand;

    protected virtual void Start()
    {
        _hand.Container.HideCards = true;
    }

    public virtual CardController MakeMove(CardController atkCard, CardSuit trumpSuit)
    {
        CardController result = null;

        if (GameManager.GameState == GameState.Attack)
        {
            List<CardController> filteredCards = _hand.Container.Cards.Where(c => c.Data.runtimeSuit != trumpSuit).OrderBy(c => c.Data.runtimePriority).ToList();
            if (filteredCards != null && filteredCards.Count > 0)
                result = filteredCards[0];
            else
            {
                filteredCards = _hand.Container.Cards.OrderBy(c => c.Data.runtimePriority).ToList();
                result = filteredCards[0];
            }
        }
        else if (GameManager.GameState == GameState.Defense)
        {
            if (atkCard.Data.runtimeSuit == trumpSuit)
            {
                List<CardController> trumps = GetOrderedSuitableCards(trumpSuit, atkCard.Data.runtimePriority);

                if (trumps != null && trumps.Count > 0)
                    result = trumps[0];
            }
            else
            {
                List<CardController> sameSuit = GetOrderedSuitableCards(atkCard.Data.runtimeSuit, atkCard.Data.runtimePriority);

                if (sameSuit != null && sameSuit.Count > 0)
                {
                    result = sameSuit[0];
                }
                else
                {
                    List<CardController> trumps = GetOrderedSuitableCards(trumpSuit, atkCard.Data.runtimePriority);
                    if (trumps != null && trumps.Count > 0)
                        result = trumps[0];
                }
            }
        }

        return result;
    }

    protected virtual List<CardController> GetOrderedSuitableCards(CardSuit suit, int minPriority)
    {
        return _hand.Container.Cards
            .Where(c => c.Data.runtimeSuit == suit && c.Data.runtimePriority > minPriority)
            .OrderBy(c => c.Data.runtimePriority).ToList();
    }
}