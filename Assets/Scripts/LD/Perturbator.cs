using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perturbator : LayerObject
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "PertubatorDetector")
		{
			FindObjectOfType<TopDownCtrl>().AddPertubator(this);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "PertubatorDetector")
		{
			FindObjectOfType<TopDownCtrl>().RemovePertubator(this);
		}
	}
}
