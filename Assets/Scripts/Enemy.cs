using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyCrashFX;
    [SerializeField] GameObject enemyHitVFX;

    [SerializeField] int scorePerHit;
    [SerializeField] int hitPoints = 8;

    ScoreBoard scoreBoard;
    Transform parentTransform;
    Rigidbody rb;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentTransform = GameObject.FindWithTag("Spawn").transform;
        AddRigidbody();
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);    
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            DestroyEnemy();
        }
    }

    void AddRigidbody()
    {
        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void ProcessHit()
    {
        SpawnEnemyVFX(enemyHitVFX);
        hitPoints--;
    }

    void DestroyEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        SpawnEnemyVFX(enemyCrashFX);
        Destroy(this.gameObject);
    }

    void SpawnEnemyVFX(GameObject enemyVFX)
    {
        GameObject vfx = Instantiate(enemyVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentTransform;
    }
}
