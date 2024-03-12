using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField] private AudioSource soundFXObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform,float volume)
    {
        //SPAWN IN GAME OBJECT
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //ASSIGN AUDIO CLIP
        audioSource.clip = audioClip;

        //ASSIGN VOLUME
        audioSource.volume = volume;

        //PLAY SOUND
        audioSource.Play();

        //GET LENGTH OF SOUND FX CLIP
        float clipLength = audioSource.clip.length;

        //DESTROY THE CLIP AFTER IT IS DONE PLAYING
        Destroy(audioSource.gameObject,clipLength);
    }
}
