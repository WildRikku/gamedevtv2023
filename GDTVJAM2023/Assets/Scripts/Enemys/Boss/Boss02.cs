using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : MonoBehaviour
{
    public GameObject[] objectsToActivateOnDeath; // Array f�r die zu aktivierenden Gameobjekte
    public GameObject[] weapons;

    public float activationDelay = 0.5f; // Zeitverz�gerung zwischen den Aktivierungen

    public MineController mineController;
    public GameObject explosionObject;

    private void Start()
    {
        //StartCoroutine(ActivateObjectsWithDelay());
        mineController.detectionRange = 100;
        mineController.rotationSpeed = 35;
        BattleStarts();
    }

    private IEnumerator ActivateObjectsWithDelay()
    {
        foreach (GameObject obj in weapons)
        {
            obj.SetActive(true);
            Instantiate(explosionObject, obj.transform.position, obj.transform.rotation);
            yield return new WaitForSeconds(activationDelay);
            
        }
        BattleStarts();

    }

    private void BattleStarts()
    {
        foreach (GameObject weapon in weapons)
        {
            if (weapon != null)
                weapon.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        foreach (GameObject objects in objectsToActivateOnDeath)
        {
            if (objects != null)
            {
                //Instantiate(explosionObject, objects.transform.position, objects.transform.rotation);
                objects.SetActive(true);

            }
        }
    }
}