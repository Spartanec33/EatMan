using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steps : MonoBehaviour
{
    private AudioSource _audioSource;
    private Transform _leg;
    private bool _collisionExit = false;
    private void Start()
    {
        _leg = transform.parent;
        _audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (_leg.rotation.x < 0 && _collisionExit)
        {
            _audioSource.Play();
        }
        _collisionExit = false;
    }
    private void OnCollisionExit(Collision collision)
    {
        _collisionExit = true;
    }

}
