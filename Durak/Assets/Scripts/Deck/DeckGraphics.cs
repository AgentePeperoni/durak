using UnityEngine;

public class DeckGraphics : MonoBehaviour
{
    [SerializeField]
    protected GameObject _clubGraphics;
    [SerializeField]
    protected GameObject _diamondGraphics;
    [SerializeField]
    protected GameObject _heartGraphics;
    [SerializeField]
    protected GameObject _spadeGraphics;
    
    protected virtual void Start()
    {
        _clubGraphics.SetActive(false);
        _diamondGraphics.SetActive(false);
        _heartGraphics.SetActive(false);
        _spadeGraphics.SetActive(false);
    }

    public virtual void ShowSuit(CardSuit suit)
    {
        switch (suit)
        {
            case CardSuit.Clubs:
                _clubGraphics.SetActive(true);
                _diamondGraphics.SetActive(false);
                _heartGraphics.SetActive(false);
                _spadeGraphics.SetActive(false);
                break;
            case CardSuit.Diamonds:
                _clubGraphics.SetActive(false);
                _diamondGraphics.SetActive(true);
                _heartGraphics.SetActive(false);
                _spadeGraphics.SetActive(false);
                break;
            case CardSuit.Hearts:
                _clubGraphics.SetActive(false);
                _diamondGraphics.SetActive(false);
                _heartGraphics.SetActive(true);
                _spadeGraphics.SetActive(false);
                break;
            case CardSuit.Spades:
                _clubGraphics.SetActive(false);
                _diamondGraphics.SetActive(false);
                _heartGraphics.SetActive(false);
                _spadeGraphics.SetActive(true);
                break;
        }
    }
}
