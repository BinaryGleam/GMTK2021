using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameLayer
{
	TOUCH = 0,
	HEAR,
	SMELL,
	SIGHT,
	TASTE,
	NONE
}

public class LayerObject : MonoBehaviour
{
	public GameLayer currentLayer = GameLayer.TOUCH;
	public UnityEvent OnLayerActivated, OnLayerDeactivated;
	bool active = false;


	public void ActiveLayerObject()
	{
		if(active == false)
		{
			active = true;
			Debug.Log(gameObject.name + " just activated");
			OnLayerActivated?.Invoke();
		}
	}

	public void DeactiveLayerObject()
	{
		if (active == true)
		{
			active = false;
			Debug.Log(gameObject.name + " just deactivated");
			OnLayerDeactivated?.Invoke();
		}
	}
}
