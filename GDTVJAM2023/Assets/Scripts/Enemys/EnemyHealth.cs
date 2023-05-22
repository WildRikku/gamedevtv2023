using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject explosionObject;
    public GameObject dieExplosionObject;
    public GameObject expOrb;
    private GameManager gameManager;
    public float enemyHealth = 2.0f;
    public int collisonDamage = 1; //wird alles �ber den Spieler abgefragt
    public float explosionForce = 5.0f;
    public bool expOrbSpawn = false;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            if (expOrbSpawn)
                Instantiate(expOrb, transform.position, transform.rotation);
            Instantiate(explosionObject, transform.position, transform.rotation);

            gameManager.UpdateEnemyCounter(-1);

            Destroy(gameObject);
        }
    }
}
