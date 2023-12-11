using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("Speed of player moving horizontally and vertically based on player input")] 
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] float xRange = 12f;
    [SerializeField] float yRange = 8f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("Particles FX")]
    [SerializeField] ParticleSystem[] lasers;

    float xThrow;
    float yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {

        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * moveSpeed;
        float yOffset = yThrow * Time.deltaTime * moveSpeed;

        float currentXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
        float currentYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        transform.localPosition = new Vector3(currentXPos, currentYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire")) 
        {
            ToggleLasers(true);
        }
        else
        {
            ToggleLasers(false);
        }
    }

    void ToggleLasers(bool isActive)
    {
        foreach (var laser in lasers)
        {
            var emissionModule = laser.emission;
            emissionModule.enabled = isActive;
        }
    }
}
