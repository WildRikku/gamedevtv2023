using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLaser : MonoBehaviour
{
    [Header("Bullet Particle")]
    public List<ParticleSystem> particleSystems;

    [Header("Weapon Settings")]
    public int bulletDamage = 10;
    public float realodInterval = 5f;
    public int bulletMaxCount = 10;
    public float spawnInterval = 0.1f;
    public string audioClip = "";
    private float nextSpawnTime = 0f;
    private int bulletCount = 0;
    

    public LineRenderer lr;
    //public BoxCollider boxCollider;
    public bool laserIsEnable = false;

    /* **************************************************************************** */
    /* LIFECYCLE METHODEN---------------------------------------------------------- */
    /* **************************************************************************** */
    private void Start()
    {
        //StartValues();
        bulletCount = bulletMaxCount;

        // set damage to particle system
        foreach (ParticleSystem weapon in particleSystems)
        {
            weapon.GetComponent<ParticleBullet>().bulletDamage = bulletDamage;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
        //SetLRPosition();
        Raycast_();
    }




    /* **************************************************************************** */
    /* RUNTIME METHODEN------------------------------------------------------------ */
    /* **************************************************************************** */
    // set start values fom the weaponController
    private void StartValues()
    {
        PlayerWeaponController weaponController = GameObject.Find("Player").GetComponent<PlayerWeaponController>();
        bulletDamage = weaponController.ffDamage;
        bulletMaxCount = weaponController.ffbulletCount;
        realodInterval = weaponController.ffReloadTime;
    }

    // shooting controller
    void Shooting()
    {
        if (bulletCount == bulletMaxCount)
        {
            Invoke("RealodWeapon", realodInterval);
            bulletCount++;
            lr.enabled = false;
            laserIsEnable = false;
            //boxCollider.enabled = false;
        }

        if (bulletCount < bulletMaxCount)
        {
            if (Time.time >= nextSpawnTime)
            {
                // shooting sound
                //AudioManager.Instance.PlaySFX(audioClip);

                // emit 1 particle of each weapon
                foreach (ParticleSystem weapon in particleSystems)
                {
                    if (weapon != null)
                        weapon.Emit(1);
                        bulletCount++;
                }
               
                nextSpawnTime = Time.time + spawnInterval;
            }
        }
    }

    void SetLRPosition()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + transform.forward * 5);
    }

    // realod a salve of weapons
    void RealodWeapon()
    {
        bulletCount = 0;
        lr.enabled = true;
        laserIsEnable = true;
        //boxCollider.enabled = true;
        //lr.SetPosition(1, transform.position + transform.forward * 5);
    }

    void Raycast_()
    {
        lr.SetPosition(0, transform.position);

        //lr.SetPosition(0, transform.position);
        int raycastDistance = 5; // Die maximale Entfernung des Raycasts
        int layerMask = (1 << 6) | (1 << 9); // Bitmaske f�r Render-Layer 6 und 8

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, layerMask))
        {
            // Kollision mit einem Objekt auf den gew�nschten Render-Layern
            GameObject collidedObject = hit.collider.gameObject;
            Debug.Log("Kollision mit " + collidedObject.name);

            lr.SetPosition(1, collidedObject.transform.position);
        }
        else
        {
            lr.SetPosition(1, transform.position + transform.forward * raycastDistance);
        }
    }

   
}
