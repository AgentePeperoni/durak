using UnityEngine;

public class DeckSounds : MonoBehaviour
{
    #region Serialized fields
    [Range(0, 1f)]
    [SerializeField]
    protected float _volume;

    [Space]
    [SerializeField]
    protected AudioClip _shuffleSound;
    #endregion

    public virtual void ShuffleSound()
    {
        AudioSource.PlayClipAtPoint(_shuffleSound, transform.position, _volume);
    }
}
