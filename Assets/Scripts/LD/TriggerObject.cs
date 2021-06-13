using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    public string targetTag = "Player";
    public UnityEvent OnTargetEnter;
    public UnityEvent OnTargetExit;

    public bool destroyOnEnter = false;
    public bool destroyOnExit = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == targetTag)
		{
			OnTargetEnter?.Invoke();
			if (destroyOnEnter)
				Destroy(gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == targetTag)
		{
			OnTargetExit?.Invoke();
			if (destroyOnExit)
				Destroy(gameObject);
		}
	}

}
