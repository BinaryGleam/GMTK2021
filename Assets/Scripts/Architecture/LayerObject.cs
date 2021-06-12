using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	bool active = false;

	public void ActiveLayerObject()
	{
		if(active == false)
		{
			active = true;
			Debug.Log(gameObject.name + " just activated");
		}
	}

	public void DeactiveLayerObject()
	{
		if (active == true)
		{
			active = false;
			Debug.Log(gameObject.name + " just deactivated");
		}
	}
}
