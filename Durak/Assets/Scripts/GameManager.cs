using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameState GameState { get; protected set; }

    [SerializeField]
    private TransferManager _transferManager;
    [SerializeField]
    private AIController _oponentAI;

    [Space]
    [SerializeField]
    private HandController _hand1;
    [SerializeField]
    private HandController _hand2;
    [SerializeField]
    private TableController _table;
    [SerializeField]
    private DeckController _deck;

    [Space]
    [SerializeField]
    private Button _giveUpButton;
    [SerializeField]
    private GameObject _endGameMenu;
    [SerializeField]
    private Text _endGameText;

    [Space]
    [SerializeField]
    private int _cardsPerHand;

    private HandController _attackingHand;
    private HandController _defendingHand;

    private bool _successfulDefense;

    private void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        GameState = GameState.Begin;
        _giveUpButton.interactable = false;
        _successfulDefense = true;

        _hand1.OnCardInteracted += _transferManager.OpenTransfer;
        _hand2.OnCardInteracted += _transferManager.OpenTransfer;

        _table.OnAttackDone += () => StartCoroutine(Defend());
        _table.OnDefenseDone += () => StartCoroutine(Resolve());

        _attackingHand = _hand2;
        _defendingHand = _hand1;

        _attackingHand.Container.Lock();
        _defendingHand.Container.Lock();

        StartCoroutine(Draw());
    }

    private IEnumerator Attack()
    {
        GameState = GameState.Attack;

        _defendingHand.Container.Lock();
        if (_oponentAI.Hand.Equals(_attackingHand))
        {
            _attackingHand.Container.Lock();

            yield return new WaitForSeconds(0.2f);
            CardController passedCard = _oponentAI.MakeMove(null, _deck.TrumpSuit);
            _transferManager.InstantTransfer(_attackingHand.Container, passedCard, _table.Container);
        }
        else
            _attackingHand.Container.Unlock();
    }

    private IEnumerator Defend()
    {
        GameState = GameState.Defense;

        _attackingHand.Container.Lock();
        if (_oponentAI.Hand.Equals(_defendingHand))
        {
            _defendingHand.Container.Lock();

            yield return new WaitForSeconds(0.2f);
            CardController passedCard = _oponentAI.MakeMove(_table.AttackCard, _deck.TrumpSuit);
            if (passedCard == null)
                GiveUp();
            else
                _transferManager.InstantTransfer(_defendingHand.Container, passedCard, _table.Container);
        }
        else
        {
            _giveUpButton.interactable = true;
            _defendingHand.Container.Unlock();
        }
    }

    private IEnumerator Resolve()
    {
        GameState = GameState.Resolve;
        _giveUpButton.interactable = false;

        _attackingHand.Container.Lock();
        _defendingHand.Container.Lock();

        CardController defCard = _table.DefendCard;
        CardController atkCard = _table.AttackCard;
        if ((defCard.Data.runtimeSuit == _deck.TrumpSuit || defCard.Data.runtimeSuit == atkCard.Data.runtimeSuit) &&
            defCard.Data.runtimePriority > atkCard.Data.runtimePriority)
        {
            yield return new WaitForSeconds(2);

            _table.Container.RemoveCard(atkCard);
            _table.Container.RemoveCard(defCard);

            Destroy(atkCard.gameObject);
            Destroy(defCard.gameObject);

            _successfulDefense = true;
            StartCoroutine(Draw());
        }
        else
        {
            _transferManager.InstantTransfer(_table.Container, defCard, _defendingHand.Container);
            StartCoroutine(Defend());
        }
    }

    private IEnumerator Draw()
    {
        GameState = GameState.Draw;
        
        while ((_attackingHand.Container.Cards.Count < _cardsPerHand || _defendingHand.Container.Cards.Count < _cardsPerHand) && _deck.Container.Cards.Count > 0)
        {
            if (_attackingHand.Container.Cards.Count < _cardsPerHand)
            {
                yield return new WaitForSeconds(0.4f);
                _attackingHand.Container.AddCard(_deck.Draw());
            }
            if (_defendingHand.Container.Cards.Count < _cardsPerHand)
            {
                yield return new WaitForSeconds(0.4f);
                _defendingHand.Container.AddCard(_deck.Draw());
            }
        }

        CheckGame();
    }

    private void CheckGame()
    {
        if (_deck.Container.Cards.Count <= 0)
        {
            if (_attackingHand.Container.Cards.Count <= 0 && _defendingHand.Container.Cards.Count <= 0)
            {
                EndGame("Ничья!");
            }
            if (_attackingHand.Container.Cards.Count <= 0)
            {
                EndGame("Вы победили!");
            }
            else if (_defendingHand.Container.Cards.Count <= 0)
            {
                EndGame("Противник победил!");
            }
            else
                NextTurn();
        }
        else
            NextTurn();
    }

    private void NextTurn()
    {
        if (_successfulDefense)
        {
            HandController temp = _attackingHand;
            _attackingHand = _defendingHand;
            _defendingHand = temp;

            _successfulDefense = false;
        }

        StartCoroutine(Attack());
    }

    private void EndGame(string message)
    {
        GameState = GameState.End;

        _endGameMenu.SetActive(true);
        _endGameText.text = message;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GiveUp()
    {
        if (GameState == GameState.Defense)
        {
            _transferManager.InstantTransfer(_table.Container, _table.AttackCard, _defendingHand.Container);
            _transferManager.InstantTransfer(_table.Container, _table.DefendCard, _defendingHand.Container);

            _successfulDefense = false;
            _giveUpButton.interactable = false;
            StartCoroutine(Draw());
        }
    }
}

public enum GameState
{
    Begin,
    Attack,
    Defense,
    Resolve,
    Draw,
    End
}
