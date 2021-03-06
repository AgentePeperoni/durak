﻿using UnityEngine;

public class CardSounds : MonoBehaviour
{
    #region Serialized fields
    [Range(0, 1f)]
    [SerializeField]
    protected float _volume;

    [Space]
    [SerializeField]
    protected AudioClip _drawSound;
    [SerializeField]
    protected AudioClip _pickSound;
    [SerializeField]
    protected AudioClip _dropSound;
    #endregion

    #region Public methods
    public virtual void DrawSound()
    {
        AudioSource.PlayClipAtPoint(_drawSound, transform.position, _volume);
    }

    public virtual void PickSound()
    {
        AudioSource.PlayClipAtPoint(_pickSound, transform.position, _volume);
    }

    public virtual void DropSound()
    {
        AudioSource.PlayClipAtPoint(_dropSound, transform.position, _volume);
    }
    #endregion
}
