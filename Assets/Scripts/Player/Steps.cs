using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private AudioSource _audioSource;
    private Transform _leg;
    private void Start()
    {
        _leg = transform.parent;
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_leg.rotation.x < 0)
            _audioSource.Play();
        
    }

}
