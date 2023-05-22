using UnityEngine;

public class ParticleBullet : MonoBehaviour
{
    public int bulletDamage = 1;
    //private GameManager gameManager;
    public ParticleSystem particleSystem;

    //List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();


    public void BulletStart(int bulletDamage_, float fireRate)
    {
        //Set Damage
        bulletDamage = bulletDamage_;
        
        //Set FireRate
        var main = particleSystem.main;
        main.duration = fireRate;

        particleSystem.Play();
    }

    public void BulletStop()
    {
        particleSystem.Stop();
    }

    public void HardBulletStop()
    {
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public void BulletSetDamage(int bulletDamage_)
    {
        bulletDamage = bulletDamage_;
    }


    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out EnemyHealth en))
        {
            en.TakeDamage(bulletDamage);
        }
    }



}
