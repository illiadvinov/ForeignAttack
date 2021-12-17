using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] int scorePerHit = 15;
     int killsInGame = 1;
    [SerializeField] int enemyHealth = 4;
    [SerializeField] GameObject hitVFX;
    

    ScoreBoard scoreBoard;
    KillScore killScore;
    GameObject parentGameObject;

    void Start()
    {
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");

        scoreBoard = FindObjectOfType<ScoreBoard>();
        killScore = FindObjectOfType<KillScore>();

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.GetComponent<Rigidbody>().useGravity = false;


    }

    

    void OnParticleCollision(GameObject other)
    {
        ProccesHit();
        if(enemyHealth < 1) 
        {
            KillEnemy();
            
        }
    }

    void ProccesHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;

        enemyHealth--;
        scoreBoard.IncreaseScore(scorePerHit);
    }
    void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parentGameObject.transform;
        killScore.IncreaseKillsScore(killsInGame);
        Destroy(gameObject);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
