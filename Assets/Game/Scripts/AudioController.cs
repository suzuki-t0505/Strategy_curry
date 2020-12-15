using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip SE_1;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void ButtonSE()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(SE_1);
        }
    }
}
