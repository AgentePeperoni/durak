using UnityEngine;

public class CardSounds : MonoBehaviour
{
    #region Serialized fields
    [Range(0, 1f)]
    [SerializeField]
    private float _volume;

    [Space]
    [SerializeField]
    private AudioClip _drawSound;
    [SerializeField]
    private AudioClip _pickSound;
    [SerializeField]
    private AudioClip _dropSound;
    #endregion

    #region Public methods
    public void DrawSound()
    {
        AudioSource.PlayClipAtPoint(_drawSound, transform.position, _volume);
    }

    public void PickSound()
    {
        AudioSource.PlayClipAtPoint(_pickSound, transform.position, _volume);
    }

    public void DropSound()
    {
        AudioSource.PlayClipAtPoint(_dropSound, transform.position, _volume);
    }
    #endregion
}
