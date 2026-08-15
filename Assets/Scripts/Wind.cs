using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Wind : MonoBehaviour
{
    [SerializeField] private AudioClip _windSound;

    private AudioSource _audioSource;
    private float _minSpeed = 0.1f;
    private float _maxSpeed = 0.5f;
    private float _currentSpeed = 0.5f;
    private bool _isChangingVolume = false;
    private float _targetVolume;
    private float _maxVolume = 1f;
    private float _minVolume = 0.01f;
    private float _minInterval = 30f;
    private float _maxInterval = 60f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.spatialBlend = 0.8f; // Set spatial blend to 3D
        _audioSource.volume = RandomVolume();
        _audioSource.clip = _windSound;
        _audioSource.loop = true;
        _audioSource.Play();
        StartCoroutine(PlayWindSound());
    }

    // Update is called once per frame
    void Update()
    {
        if (_isChangingVolume)
        {            
            _audioSource.volume = Mathf.Lerp(_audioSource.volume, _targetVolume, _currentSpeed * Time.deltaTime);
            Debug.Log("increasing wind volume: " + _audioSource.volume);
            if (Mathf.Abs(_audioSource.volume - _targetVolume) < 0.01f)
            {
                _audioSource.volume = _targetVolume;
                _isChangingVolume = false;
                Debug.Log("finished changing wind volume: " + _audioSource.volume);
            }

        }
        
    }
    IEnumerator PlayWindSound()
    {
        while (true)
        {
            if (!_isChangingVolume)
            {
                _targetVolume = RandomVolume();
                _currentSpeed = RandomSpeed();
                _isChangingVolume = true;
            }

            Debug.Log("can increase wind volume: " + _isChangingVolume);

            yield return new WaitForSeconds(RandomInterval());
        }
    }
    private float RandomVolume()
    {
        return Random.Range(_minVolume, _maxVolume);
    }
    private float RandomSpeed()
    {
        return Random.Range(_minSpeed, _maxSpeed);
    }
    private float RandomInterval()
    {
        return Random.Range(_minInterval, _maxInterval);
    }
}
