using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionFX;

    PlayerController playerController;
    Rigidbody rb;
    BoxCollider[] colliders;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    void OnTriggerExit(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        playerController.enabled = false;
        explosionFX.Play();
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene()
    {
        DisableColliders();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void DisableColliders()
    {
        colliders = GetComponentsInChildren<BoxCollider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
