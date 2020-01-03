using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    float _scrollSpeed, _tileSizeZ;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * _scrollSpeed, _tileSizeZ);
        transform.position = _startPosition - Vector3.forward * newPosition;
    }
}
