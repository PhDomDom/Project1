using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
