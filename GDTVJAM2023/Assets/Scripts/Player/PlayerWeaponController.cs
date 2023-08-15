using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [Header("Weapon buffs")]
    private ShipData shipData;
    public int bulletCritChance = 5;
    public int bulletCritDamage = 125;
    public int burnDamageChance = 1;
    public int burnDamage = 5;
    public int burnTickCount = 5;
    public float rocketAOERadius = 0;

    [Header("Passiv abilitys")]
    public bool isHeadCannon = false;
    public bool isRocketLauncher = false;
    public bool isFireFlies = false;
    public bool isBulletWings = false;
    public bool isLifeModul = false;
    public bool isSpreadGun = false;
    public bool isFrontShield = false;
    public bool isBackShield = false;
    public bool isNovaExplosion = false;
    public bool isRockedWings = false;
    public bool isFrontLaser = false;
    public bool isOrbitalLaser = false;

    private HeadCannon isHeadCannonInstalled;
    private PeriodSpawner isRocketLauncherInstalled;
    private Fireflies isFireFliesInstalled;
    private BulletWings isBulletWingsInstalled;
    private LifeModul isLifeModulInstalled;
    private SpreadGun isSpreadGunInstalled;
    private FrontShieldSpawner isFrontShieldInstalled;
    private BackShieldSpawner isBackShieldInstalled;
    private NovaExplosion isNovaExplosionInstalled;
    private RocketWings isRockedWingsInstalled;
    private FrontLaser isFrontLaserInstalled;
    private OrbitalLaser isOrbitalLaserInstalled;



    [Header("Objects")]
    public GameObject frontShield;
    public GameObject backShield;


    [Header("Head Cannon")]
    public int hcBulletDamage = 4;
    public int hcSalveCount = 6;
    public float hcReloadTime = 2.5f;
    public GameObject headCannon;


    [Header("Rocket Launcher")]
    public int rlDamage = 20;
    public float rlLifeTime;
    public float rlReloadTime = 5f;
    public GameObject rocketLauncher;


    [Header("Fireflies")]
    public int ffDamage = 10;
    public float ffReloadTime = 5.5f;
    public int ffbulletCount = 16;
    public GameObject fireFlys;


    [Header("Bullet Wings")]
    public int bwDamage = 6;
    public float bwRealoadTime = 6f;
    public int bwSalveCount = 6;
    public GameObject bulletWings;


    [Header("Life Modul")]
    public int lmLifePerTick = 1;
    public float lmReloadTime = 10;
    public GameObject lifeModul;


    [Header("Spread Gun")]
    public int sgDamage = 8;
    public float sgReloadTime = 3f;
    public int sgBulletCount = 8;
    public GameObject spreadGun;


    [Header("Nova Explosion")]
    public int neDamage = 3;
    public float neReloadTime = 4.5f;
    public float neRadius = 3f;
    public GameObject novaExplosion;


    [Header("Rocket Wings")]
    public int rwDamage = 3;
    public float rwReloadTime = 4.5f;
    public int rwSalveCount = 6;
    public GameObject rocketWings;


    [Header("Front Laser")]
    public int flDamage = 4;
    public float flReloadTime = 6f;
    public float flShootingTime = 3f;
    public GameObject frontLaser;


    [Header("Orbital Laser")]
    public int olDamage = 10;
    public float olReloadTime = 3f;
    public GameObject orbitalLaser;


    [Header("Container")]
    public Transform passivParentContainer;


    [Header("Shield Controll")]
    public float fsSpawnTime = 10f;
    public int fsShieldLife = 3;
    public float bsSpawnTime = 6f;
    public int bsShildLife = 2; 
    public bool isFrontShieldEnabled = false;
    public bool isBackShieldLeft = false;
    public bool isBackShieldRight = false;


    //private Objects
    private PlayerController playerController;
    private PlayerMWController playerMWController;
    private UpgradeChooseList upgradeChooseList;



    /* **************************************************************************** */
    /* LIFECYCLE METHODEN---------------------------------------------------------- */
    /* **************************************************************************** */
    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerMWController = gameObject.GetComponent<PlayerMWController>();
        upgradeChooseList = GameObject.Find("Game Manager").GetComponent<UpgradeChooseList>();


        // copy ship Data
        shipData = playerController.shipData;

        upgradeChooseList.mcBulletLvl = shipData.bulletClass;
        upgradeChooseList.mcExplosionLvl = shipData.explosionClass;
        upgradeChooseList.mcLaserLvl = shipData.laserClass;
        upgradeChooseList.mcSupportLvl = shipData.supportClass;
        upgradeChooseList.scSwarmLvl = shipData.swarmClass;
        upgradeChooseList.scDefenceLvl = shipData.defenseClass;
        upgradeChooseList.scTargetingLvl = shipData.targetClass;
        upgradeChooseList.scDirectionLvl = shipData.directionClass;

        // set start class upgrades
        if (shipData.bulletClass > 0)
        {
            upgradeChooseList.baseBulletCritChance += upgradeChooseList.critChance;
            upgradeChooseList.baseBulletCritDamage += upgradeChooseList.critDamage;
        }
        if (shipData.explosionClass > 0)
        {
            upgradeChooseList.baseRocketAOERadius += upgradeChooseList.aoeRange;
        }
        if (shipData.laserClass > 0)
        {
            upgradeChooseList.baseLaserBurnDamageChance += upgradeChooseList.buringChance;
        }
        if (shipData.supportClass > 0)
        {
            upgradeChooseList.baseSupportRealoadTime += upgradeChooseList.supportRealodTime;
        }

        // Start Values
        WeaponChoose();

        // upgrade Weapon Values
        UpdateWeaponValues();
    }




    /* **************************************************************************** */
    /* WEAPON MANAGEMENT----------------------------------------------------------- */
    /* **************************************************************************** */
    public void WeaponChoose()
    {
        GameObject go = null;
        if (isHeadCannon == true && isHeadCannonInstalled == null)
        {
            go = Instantiate(headCannon, passivParentContainer);
            isHeadCannonInstalled = go.GetComponent<HeadCannon>();
            upgradeChooseList.weaponIndexInstalled[6] = true;
        }
        if (isRocketLauncher == true && isRocketLauncherInstalled == null)
        {
            go = Instantiate(rocketLauncher, passivParentContainer);
            isRocketLauncherInstalled = go.GetComponent<PeriodSpawner>();
            upgradeChooseList.weaponIndexInstalled[7] = true;
        }
        if (isFireFlies == true && isFireFliesInstalled == null)
        {
            go = Instantiate(fireFlys, passivParentContainer);
            isFireFliesInstalled = go.GetComponent<Fireflies>();
            upgradeChooseList.weaponIndexInstalled[8] = true;
        }
        if (isBulletWings == true && isBulletWingsInstalled == null)
        {
            go = Instantiate(bulletWings, passivParentContainer);
            isBulletWingsInstalled = go.GetComponent<BulletWings>();
            upgradeChooseList.weaponIndexInstalled[9] = true;
        }
        if (isLifeModul == true && isLifeModulInstalled == null)
        {
            go = Instantiate(lifeModul, passivParentContainer);
            isLifeModulInstalled = go.GetComponent<LifeModul>();
            upgradeChooseList.weaponIndexInstalled[10] = true;
        }
        if (isSpreadGun == true && isSpreadGunInstalled == null)
        {
            go = Instantiate(spreadGun, passivParentContainer);
            isSpreadGunInstalled = go.GetComponent<SpreadGun>();
            upgradeChooseList.weaponIndexInstalled[11] = true;
        }
        if (isFrontShield == true && isFrontShieldInstalled == null)
        {
            var shild = Instantiate(frontShield, passivParentContainer);
            shild.name = frontShield.name;
            isFrontShieldInstalled = shild.GetComponent<FrontShieldSpawner>();
            upgradeChooseList.weaponIndexInstalled[12] = true;
        }
        if (isBackShield == true && isBackShieldInstalled == null)
        {
            var shild = Instantiate(backShield, passivParentContainer);
            shild.name = backShield.name;
            isBackShieldInstalled = shild.GetComponent<BackShieldSpawner>();
            upgradeChooseList.weaponIndexInstalled[13] = true;
        }
        if (isNovaExplosion == true && isNovaExplosionInstalled == null)
        {
            go = Instantiate(novaExplosion, passivParentContainer);
            isNovaExplosionInstalled = go.GetComponent<NovaExplosion>();
            upgradeChooseList.weaponIndexInstalled[14] = true;
        }
        if (isRockedWings == true && isRockedWingsInstalled == null)
        {
            go = Instantiate(rocketWings, passivParentContainer);
            isRockedWingsInstalled = go.GetComponent<RocketWings>();
            upgradeChooseList.weaponIndexInstalled[15] = true;
        }
        if (isFrontLaser == true && isFrontLaserInstalled == null)
        {
            go = Instantiate(frontLaser, passivParentContainer);
            upgradeChooseList.weaponIndexInstalled[16] = true;
            isFrontLaserInstalled = go.GetComponent<FrontLaser>();
        }
        if (isOrbitalLaser == true && isOrbitalLaserInstalled == null)
        {
            go = Instantiate(orbitalLaser);
            isOrbitalLaserInstalled = go.GetComponent<OrbitalLaser>();
            upgradeChooseList.weaponIndexInstalled[17] = true;
        }

        Invoke("UpdateWeaponValues", 0.1f);
    }


    public void UpdateWeaponValues()
    {
        Debug.Log("weapon Upgrade");

        // main Class
        bulletCritChance = upgradeChooseList.baseBulletCritChance;
        bulletCritDamage = upgradeChooseList.baseBulletCritDamage;

        burnDamageChance = upgradeChooseList.baseLaserBurnDamageChance;
        rocketAOERadius = 1 + upgradeChooseList.baseRocketAOERadius * 0.1f;
        float suReloadTime = (upgradeChooseList.baseSupportRealoadTime * 0.1f);

        // sub class
        int scSwarmLvl_ = upgradeChooseList.scSwarmLvl;
        float scDefenceLvl_ = upgradeChooseList.scDefenceLvl * 0.1f;
        int scTargetingLvl_ = upgradeChooseList.scTargetingLvl;
        int scDirectionLvl_ = upgradeChooseList.scDirectionLvl;


        // Head Cannon - bullet - target
        if (isHeadCannonInstalled != null)
        {
            isHeadCannonInstalled.bulletDamage = hcBulletDamage + scTargetingLvl_;
            isHeadCannonInstalled.fireSalveMax = hcSalveCount;
            isHeadCannonInstalled.reloadSalveInterval = Mathf.Max(0.1f, hcReloadTime - (hcReloadTime*suReloadTime));
        }

        // Rocket Launcher - rocket - target
        if (isRocketLauncherInstalled != null)
        {
            isRocketLauncherInstalled.rocketDamage = rlDamage + scTargetingLvl_;
            isRocketLauncherInstalled.lifeTime = rlLifeTime;
            isRocketLauncherInstalled.spawnInterval = Mathf.Max(0.1f, rlReloadTime - (rlReloadTime*suReloadTime));
        }

        // Fireflies - bullet - backwards
        if (isFireFliesInstalled != null)
        {
            isFireFliesInstalled.bulletDamage = ffDamage + scDirectionLvl_;
            isFireFliesInstalled.realodInterval = Mathf.Max(0.1f, ffReloadTime - (ffReloadTime*suReloadTime));
            isFireFliesInstalled.bulletMaxCount = ffbulletCount;
        }

        // Bullet Wings - bullet - swarm
        if (isBulletWingsInstalled != null)
        {
            isBulletWingsInstalled.bulletDamage = bwDamage;
            isBulletWingsInstalled.realodInterval = Mathf.Max(0.1f, bwRealoadTime - (bwRealoadTime * suReloadTime));
            isBulletWingsInstalled.salveMaxCount = bwSalveCount + scSwarmLvl_;
        }

        // Life Modul - support
        if (isLifeModulInstalled != null)
        {
            isLifeModulInstalled.nextHealTick = Mathf.Max(0.1f, lmReloadTime - (lmReloadTime * suReloadTime));
            isLifeModulInstalled.healthPerTick = lmLifePerTick;
        }

        // Spread Gun - bullet - swarm
        if (isSpreadGunInstalled != null)
        {
            isSpreadGunInstalled.bulletDamage = sgDamage;
            isSpreadGunInstalled.realodInterval = Mathf.Max(0.1f, sgReloadTime - (sgReloadTime * suReloadTime));
            isSpreadGunInstalled.bulletMaxCount = sgBulletCount + scSwarmLvl_;
        }

        // Front Shield - support - defence
        if (isFrontShieldInstalled != null)
        {
            isFrontShieldInstalled.spawnInterval = Mathf.Max(0.1f, fsSpawnTime - (fsSpawnTime * suReloadTime) - (fsSpawnTime * scDefenceLvl_));
            isFrontShieldInstalled.shieldLife = fsShieldLife;
        }

        // Back Shield - support - defence
        if (isBackShieldInstalled != null)
        {
            isBackShieldInstalled.spawnInterval = Mathf.Max(0.1f, bsSpawnTime - (bsSpawnTime * suReloadTime) - (bsSpawnTime * scDefenceLvl_));
            isBackShieldInstalled.shieldLife = bsShildLife;
        }

        // Nova Explosion - explosion - defence
        if (isNovaExplosionInstalled != null)
        {
            isNovaExplosionInstalled.novaDamage = neDamage;
            isNovaExplosionInstalled.spawnInterval = Mathf.Max(0.1f, neReloadTime - (neReloadTime*(scDefenceLvl_ + suReloadTime)));
            isNovaExplosionInstalled.explosionRadius = neRadius * rocketAOERadius;
        }

        // Rocket Wings - explosion - swarm
        if (isRockedWingsInstalled != null)
        {
            isRockedWingsInstalled.rocketDamage = rwDamage;
            isRockedWingsInstalled.relodInterval = Mathf.Max(0.1f, rwReloadTime - (rwReloadTime * suReloadTime));
            isRockedWingsInstalled.salveMaxCount = rwSalveCount + scSwarmLvl_;
        }

        // Front Laser - laser
        if (isFrontLaserInstalled != null)
        {
            isFrontLaserInstalled.bulletDamage = flDamage;
            isFrontLaserInstalled.realodInterval = Mathf.Max (1f, flReloadTime -(flReloadTime * suReloadTime));
            isFrontLaserInstalled.laserShootTime = flShootingTime;
        }

        // Orbital Laser - laser - defence
        if (isOrbitalLaserInstalled != null)
        {
            isOrbitalLaserInstalled.damage = olDamage;
            isOrbitalLaserInstalled.realoadTime = Mathf.Max(0.1f, olReloadTime - (olReloadTime* (scDefenceLvl_+ suReloadTime)));

            isOrbitalLaserInstalled.UpdateOrbs();
        }
    }


}
