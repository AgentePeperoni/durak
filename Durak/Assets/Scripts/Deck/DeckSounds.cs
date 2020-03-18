using UnityEngine;

public class DeckSounds : MonoBehaviour
{
    #region Serialized fields
    [Range(0, 1f)]
    [SerializeField]
    private float _volume;

    [Space]
    [SerializeField]
    private AudioClip _shuffleSound;
    #endregion

    public void ShuffleSound()
    {
        AudioSource.PlayClipAtPoint(_shuffleSound, transform.position, _volume);
    }
}
