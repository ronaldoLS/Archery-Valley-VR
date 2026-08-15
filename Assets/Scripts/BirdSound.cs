using System.Collections;
using UnityEngine;

public class BirdSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] birdSounds;
    private AudioSource audioSource;

    private float minVolume = 0.2f;
    private float maxVolume = 1f;
    private float minInterval = 4f;
    private float maxInterval = 10f;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(PlayRandomBirdSound());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public AudioClip RandomBirdClip()
    {
        return birdSounds[Random.Range(0, birdSounds.Length)];
    }
    public void SetBirdSound(AudioClip clip)
    {
        audioSource.clip = clip;
    }
    IEnumerator PlayRandomBirdSound()
    {
        while (true)
        {
            SetBirdSound(RandomBirdClip());
            audioSource.volume = Random.Range(minVolume, maxVolume);
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length + Random.Range(minInterval, maxInterval));
        }
    }



}
