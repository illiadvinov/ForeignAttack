using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class CollisionHandler : MonoBehaviour
{
    GameObject[] col;
    [SerializeField] ParticleSystem explosion;

    [SerializeField] float loadDelay = 2.5f;
    GameObject[] lasers;
    private void Start() 
    {
        lasers = GameObject.FindGameObjectsWithTag("Lasers");
        col = GameObject.FindGameObjectsWithTag("PlayerCollider");
    }
   void OnCollisionEnter(Collision other) 
   {
           
        //Debug.Log(this.name + "--Collided with--" + other.gameObject.name); 
   }
   void OnTriggerEnter(Collider other) 
   {
       //Debug.Log($"{this.name} **Triggered by** {other.gameObject.name}"); 
       StartCrash();
   }

    void StartCrash()
    {
        
        explosion.Play();
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<PlayerController>().enabled = false;
        foreach(GameObject collides in col)
        {
               collides.GetComponent<Collider>().enabled = false;
        }

        foreach(GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = false;

        }
        
        
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
   {
       int currentIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentIndex);
   }

}
