using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField]
    private CardData _data;

    #region Public properties
    public CardData Data
    {
        get => _data;
        set
        {
            _data = value;
            UpdateComponents();
        }
    }
    public CardGraphics Graphics { get; private set; }
    public CardSounds Sounds { get; private set; }
    public CardInteraction Interaction { get; private set; }
    public CardMovement Movement { get; private set; }
    public CardCollision Collision { get; private set; }
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
        Graphics = GetComponent<CardGraphics>() ?? GetComponentInChildren<CardGraphics>();
        Interaction = GetComponent<CardInteraction>() ?? GetComponentInChildren<CardInteraction>();
        Movement = GetComponent<CardMovement>() ?? GetComponentInChildren<CardMovement>();
        Collision = GetComponent<CardCollision>() ?? GetComponentInChildren<CardCollision>();
        Sounds = GetComponent<CardSounds>() ?? GetComponentInChildren<CardSounds>();
    }

    private void InitializeComponents()
    {
        Graphics?.Initialize(_data);
        Interaction?.Initialize(this, _data.runtimeFace.bounds.extents);

        if (Interaction != null && Movement != null)
        {
            Interaction.OnMouseDownOccur += (r) => Movement.MouseDown();
            Interaction.OnMouseDragOccur += (r) => Movement.MouseDrag();
            Interaction.OnMouseUpOccur += (r) => Movement.MouseUp();
        }

        if (Sounds != null)
        {
            Interaction.OnMouseDownOccur += (c) => Sounds.PickSound();
            Interaction.OnMouseUpOccur += (c) => Sounds.DropSound();
        }

        Collision?.Initialize(this);
    }
    #endregion

    #region Public methods
    public void UpdateComponents()
    {
        Graphics?.UpdateGraphics(_data);
        Interaction?.UpdateInteraction(_data.runtimeFace.bounds.extents);
    }

    public void Lock()
    {
        Interaction?.SetActive(false);
    }

    public void Unlock()
    {
        Interaction?.SetActive(true);
    }
    #endregion
}
