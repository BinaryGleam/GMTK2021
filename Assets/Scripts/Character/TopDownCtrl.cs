using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownCtrl : MonoBehaviour
{
    private Rigidbody2D myRigidbody = null;
    private Animator myAnimator = null;
    private SpriteRenderer mySpriteRenderer = null;
    private SpriteRenderer myChildSpriteRenderer = null;

    private bool[] activeLayers = new bool[(int)GameLayer.NONE];
    private Perturbator[] pertubatorsRef;

    [SerializeField]
    private Image playerFeedback = null;

    public float speed = 1f;
    public float maxHp = 100;
    public float currentHp;

    public bool[] ActiveLayers
    {
        get
		{
            return activeLayers;
		}

        set
		{
            int i = 0;
            LayerObject[] layerObjects = FindObjectsOfType<LayerObject>();

            foreach (bool current in value)
			{
                activeLayers[i] = current;

                i++;
			}

            foreach(LayerObject currentObject in layerObjects)
			{
                if(activeLayers[(int)currentObject.currentLayer] == true)
				{
                    currentObject.ActiveLayerObject();
				}
                else
				{
                    currentObject.DeactiveLayerObject();
                }
            }
		}
	}

	private void Awake()
	{
        currentHp = maxHp;

        for (int i = 0; i < activeLayers.Length; i++)
            activeLayers[i] = false;
	}

	void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myChildSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //Idle
        if (horizontal == 0f && vertical == 0f)
		{
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
            myAnimator.Play("Idle");
		}
        else if (vertical <= -1f)
		{
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
            myAnimator.Play("Down");
        }
        else if (vertical >= 1f)
		{
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
            myAnimator.Play("Up");
        }
        else if (horizontal <= -1f)
		{
            myAnimator.Play("Horizontal");
            mySpriteRenderer.flipX = true;
            myChildSpriteRenderer.flipX = true;
        }
        else if (horizontal >= 1f)
		{
            myAnimator.Play("Horizontal");
            mySpriteRenderer.flipX = false;
            myChildSpriteRenderer.flipX = false;
        }

        myRigidbody.velocity = new Vector2(horizontal, vertical).normalized * speed;
    }
}
