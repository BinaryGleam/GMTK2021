using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    UP = 0,
    RIGHT,
    LEFT,
    DOWN,
	COUNT
}

[System.Serializable]
public struct CompassDir
{
	public GameObject GO;
	public Vector2 Dir;
}

public class CompassSystem : MonoBehaviour
{
    Vector2 contactDirection = Vector2.zero;
	[SerializeField]
	CompassDir[] needles = new CompassDir[(int)Direction.COUNT];
	[SerializeField]
	float angleAcceptance = 0.33f;

	private void Update()
	{
		foreach (CompassDir currentNeedle in needles)
		{
			currentNeedle.GO.SetActive(false);
		}

		if (contactDirection != Vector2.zero)
		{
			foreach(CompassDir currentNeedle in needles)
			{
				if(Vector2.Dot(currentNeedle.Dir.normalized,contactDirection.normalized) >= angleAcceptance)
				{
					currentNeedle.GO.SetActive(true);
				}
			}
		}
	}

    public void LoadDirection(Vector2 dir)
	{
		contactDirection = dir;
	}
}
