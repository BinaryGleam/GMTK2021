﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStates
{
    FINE = 0,
    PANICKED,
    DEAD,
    CUTSCENE,
    COUNT
}

public class TopDownCtrl : MonoBehaviour
{
    private PlayerStates state = PlayerStates.FINE;
    private GameLayer headLayer = GameLayer.NONE;
    private Rigidbody2D myRigidbody = null;
    private Animator myAnimator = null;
    private SpriteRenderer mySpriteRenderer = null;
    private SpriteRenderer myChildSpriteRenderer = null;

    private bool[] activeLayers = new bool[(int)GameLayer.NONE];
    private List<Perturbator> pertubatorsRef = new List<Perturbator>();

    [SerializeField]
    private Animator playerFeedback = null;

    [Header("Locomotion stuff")]
    public float speed = 1f;

    [Header("Health stuff")]
    public float maxHp = 100;
    private float currentHp;
    [Tooltip("Dmg lost per sec")]
    public float dmgRate = 50;
    [Tooltip("Dmg recovered per sec")]
    public float healRate = 10;


    public PlayerStates State
	{
        get { return state; }
        set
		{
            state = value;

			switch (state)
			{
				case PlayerStates.FINE:
                    playerFeedback.Play("Normal");
					break;
				case PlayerStates.PANICKED:
                    playerFeedback.Play("Panick");
					break;
                case PlayerStates.DEAD:
                    playerFeedback.Play("Dead");
                    HeadLayer = GameLayer.NONE;
					break;
                case PlayerStates.CUTSCENE:
				case PlayerStates.COUNT:
				default:
					break;
			}
		}
	}

    public GameLayer HeadLayer
	{
        get { return headLayer; }
		set
		{
            headLayer = value;
			switch (headLayer)
			{
				case GameLayer.TOUCH:
				case GameLayer.HEAR:
				case GameLayer.SIGHT:
                    LayerObject[] layerObjects = FindObjectsOfType<LayerObject>();
                    currentHp = maxHp;
                    playerFeedback.GetComponent<Image>().enabled = true;
                    if(pertubatorsRef.Count != 0)
					{
                        State = PlayerStates.PANICKED;
					}
                    else
					{
                        State = PlayerStates.FINE;
                    }
                    foreach (LayerObject current in layerObjects)
                    {
                        if(current.currentLayer == headLayer)
						{
                            current.ActiveLayerObject();
						}
                        else
						{
                            current.DeactiveLayerObject();
                        }
                    }
                    break;
				case GameLayer.NONE:
                    LayerObject[] gatheredObjects = FindObjectsOfType<LayerObject>();
                    foreach (LayerObject actual in gatheredObjects)
                    {
                        actual.DeactiveLayerObject();
                    }
                    playerFeedback.GetComponent<Image>().enabled = false;
                    break;
				case GameLayer.SMELL:
				case GameLayer.TASTE:
				default:
					break;
			}
		}
	}

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
        if (state == PlayerStates.CUTSCENE)
            return;

        //CONTROLS
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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

        //STATE
        if(pertubatorsRef.Count != 0)
		{
            currentHp -= dmgRate * Time.deltaTime;

            if(currentHp <= 0)
			{
                transform.GetChild(0).gameObject.SetActive(false);
                State = PlayerStates.DEAD;
            }
        }
        else
		{
            currentHp = Mathf.Clamp(currentHp + healRate * Time.deltaTime,0,maxHp);
		}

        //DEBUG
        if(Input.GetKeyDown(KeyCode.Keypad0))
		{
            HeadLayer = GameLayer.HEAR;
		}
        else if(Input.GetKeyDown(KeyCode.Keypad1))
		{
            HeadLayer = GameLayer.SIGHT;
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2))
		{
            HeadLayer = GameLayer.TOUCH;
        }
    }

    public void AddPertubator(Perturbator inPertubator)
	{
        foreach(Perturbator perb in pertubatorsRef)
		{
            if(perb == inPertubator)
			{
                return;
			}
		}
        pertubatorsRef.Add(inPertubator);
        State = PlayerStates.PANICKED;
	}

    public void RemovePertubator(Perturbator inPertubator)
    {
        for(int i = 0; i<pertubatorsRef.Count; i++)
		{
            if(inPertubator == pertubatorsRef[i])
			{
                pertubatorsRef.RemoveAt(i);
			}
		}
        if(pertubatorsRef.Count == 0)
		{
            State = PlayerStates.FINE;
        }
    }
}
