using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePublicFunc : MonoBehaviour
{

    public GameObject[] toActivate = null;
    public GameObject[] toDeactivate = null;

    public void ActivateDeactivateShit()
	{
        foreach (GameObject current in toActivate)
        {
            current.SetActive(true);
        }
        foreach (GameObject current in toDeactivate)
        {
            Destroy(current);
        }
    }
}
