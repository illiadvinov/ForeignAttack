using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioClip laserSFX;
    AudioSource audioSource;


    [Header("General Setup Settings")]


     [Header("Speed Controls")]

     [Tooltip("How fast ship moves up and down based upon player input")] 
     [SerializeField] float hSpeed = 1f;

     [ Tooltip("How fast ship moves left and right based upon player input")]
     [SerializeField] float vSpeed = 1f;

     

     [Tooltip("Rotation by vertical axes to make movement looks smoother")]
     [SerializeField] float zRange = 5f;

     [Tooltip("Rotation by horizontal axes to make movement looks smoother")]
     [SerializeField] float yRange = 5f;

     [Header("Lasers array")]
     [Tooltip("Add lasers in array")]
    [SerializeField] GameObject[] lasers;


     [Header("Screen Position based tuning")]
     [SerializeField] float pitchFactor = -2f;
     [SerializeField] float controlPitchFactor = -10f;

     [Header("Player input based tuning")]
     [SerializeField] float rollFactor = 0f;

     [SerializeField] float controlYawFactor = 0f;

    

     float verticalThrow, horizontalThrow;

     void Start() 
     {
         audioSource = GetComponent<AudioSource>();
     }
     


    void Update()
    {
        ProcessTranlation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * pitchFactor;
        float pitchDueToControl = verticalThrow * controlPitchFactor;

        float rollDueToPosition = transform.localPosition.z * rollFactor;

        float yawDueToControl = horizontalThrow * controlYawFactor;

        float pitch =  pitchDueToPosition + pitchDueToControl; // X axis
        float yaw = -90f  + yawDueToControl; // Y axis
        float roll = rollDueToPosition; // Z axis
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
       
    }


    void ProcessTranlation()
    {
         horizontalThrow = Input.GetAxis("Horizontal");
         verticalThrow = Input.GetAxis("Vertical");

        float zOffset = horizontalThrow * Time.deltaTime * hSpeed;
        float rawZPos = transform.localPosition.z + zOffset;
        float clampedZPos = Mathf.Clamp(rawZPos, -zRange, zRange);

        float yOffset = verticalThrow * Time.deltaTime * vSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3
        (transform.localPosition.x,
        clampedYPos,
        clampedZPos);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
           audioSource.PlayOneShot(laserSFX);
           SetLasersActive(true);
        }

        else 
        {
            SetLasersActive(false);
        }

        
    }

    void SetLasersActive(bool isActive)
    {
        
        foreach(GameObject laser in lasers)
        {
           var emission = laser.GetComponent<ParticleSystem>().emission;
           emission.enabled = isActive;
           

        }
    }

   
}
