using UnityEngine;
using UnityEngine.UI;

public class DeckGraphics : MonoBehaviour
{
    #region Serialized fields
    [SerializeField]
    private GameObject _clubGraphics;
    [SerializeField]
    private GameObject _diamondGraphics;
    [SerializeField]
    private GameObject _heartGraphics;
    [SerializeField]
    private GameObject _spadeGraphics;

    [SerializeField]
    private Text _cardsLeftText;
    #endregion

    private void Start()
    {
        _clubGraphics.SetActive(false);
        _diamondGraphics.SetActive(false);
        _heartGraphics.SetActive(false);
        _spadeGraphics.SetActive(false);
    }

    #region Public methods
    public void ShowSuit(CardSuit suit)
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

    public void SetCardsCount(int count)
    {
        _cardsLeftText.text = count.ToString();
    }
    #endregion
}
