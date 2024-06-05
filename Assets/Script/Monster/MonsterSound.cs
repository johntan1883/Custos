using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MonsterSound : MonoBehaviour
{
    public AudioClip BellSound;
    public GameObject Boy;

    private AudioSource audioSource;

    [SerializeField] private float minTime = 5f;
    [SerializeField] private float maxTime = 10f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundAtRandomIntervals());
    }

    IEnumerator PlaySoundAtRandomIntervals()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
            PlaySound();
            NotifyBoy();
            Debug.Log($"Play sound after {waitTime} seconds");
        }
    }

    private void PlaySound()
    {
        audioSource.PlayOneShot(BellSound);
    }

    private void NotifyBoy()
    {
        if (Boy != null)
        {
            BoyReactSound boy = Boy.GetComponent<BoyReactSound>();
            if (boy != null)
            {
                boy.ReactToSound(transform.position);
            }
        }
    }
}
