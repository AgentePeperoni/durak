using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField]
    protected CardData _data;

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
    public CardGraphics Graphics { get; protected set; }
    public CardSounds Sounds { get; protected set; }
    public CardInteraction Interaction { get; protected set; }
    public CardMovement Movement { get; protected set; }
    public CardCollision Collision { get; protected set; }
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
        Graphics = GetComponent<CardGraphics>() ?? GetComponentInChildren<CardGraphics>();
        Interaction = GetComponent<CardInteraction>() ?? GetComponentInChildren<CardInteraction>();
        Movement = GetComponent<CardMovement>() ?? GetComponentInChildren<CardMovement>();
        Collision = GetComponent<CardCollision>() ?? GetComponentInChildren<CardCollision>();
        Sounds = GetComponent<CardSounds>() ?? GetComponentInChildren<CardSounds>();
    }

    protected virtual void InitializeComponents()
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
    public virtual void UpdateComponents()
    {
        Graphics?.UpdateGraphics(_data);
        Interaction?.UpdateInteraction(_data.runtimeFace.bounds.extents);
    }

    public virtual void Lock()
    {
        Interaction?.SetActive(false);
    }

    public virtual void Unlock()
    {
        Interaction?.SetActive(true);
    }
    #endregion
}
