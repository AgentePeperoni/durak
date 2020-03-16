using UnityEngine;

public class DeckSounds : MonoBehaviour
{
    [Range(0, 1f)]
    [SerializeField]
    protected float _volume;

    [Space]
    [SerializeField]
    protected AudioClip _shuffleSound;

    public virtual void ShuffleSound()
    {
        AudioSource.PlayClipAtPoint(_shuffleSound, transform.position, _volume);
    }
}
