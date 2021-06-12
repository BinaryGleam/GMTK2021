using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBasic : MonoBehaviour
{
    //Mon temps d'attente
    public float delay = 5f;
    //Mon chronometre
    float chrono = 0f;

    //Se fait une fois quand on appuie sur le bouton play
    void Start()
    {
        //On met le temps entre chaque action 
        chrono = delay;
    }

    //Se fait tout le temps
    void Update()
    {
        chrono -= Time.deltaTime;
        //chrono = chrono - Time.deltaTime;
        if (chrono <= 0f)
		{
            GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            chrono = delay;
		}
    }
}
