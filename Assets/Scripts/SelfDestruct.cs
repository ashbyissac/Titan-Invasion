using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float delay = 3f;

    void Update()
    {
        Destroy(gameObject, delay);
    }
}
