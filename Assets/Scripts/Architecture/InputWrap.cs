using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public static class ActionExtension
{
	public static void SafeCall(this UnityEvent action)
	{
		if (null != action)
			action.Invoke();
	}
}

[Serializable]
public class InputButton
{
	#region Variables

	public string name;
	public UnityEvent pressedEvent, onPressedStay, releasedEvent;

	[HideInInspector]
	public bool pressed = false;

	#endregion

	#region Methods

	public virtual void Reset()
	{
		pressedEvent = releasedEvent = null;
	}

	#endregion
}

[Serializable]
public class InputAxis : InputButton
{
	#region Variables

	public UnityEvent negativeEvent, onNegativeStay;

	[HideInInspector]
	public float value = 0.0f;
	[HideInInspector]
	public float prevValue = 0.0f;

	[HideInInspector]
	public bool negative = false;

	#endregion

	#region Methods

	public override void Reset()
	{
		base.Reset();
		negativeEvent = null;
		value = 0f;
	}

	#endregion
}

public class InputWrap : MonoBehaviour
{

	#region Variables

	public List<InputButton> buttons = new List<InputButton>();
	public List<InputAxis> axis = new List<InputAxis>();

	public delegate void Protocol();

	#endregion

	#region Properties

	#endregion

	#region Methods

	private void Update()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			if (Input.GetButtonDown(buttons[i].name))
			{
				buttons[i].pressed = true;
				buttons[i].pressedEvent.SafeCall();
			}
			else if (Input.GetButtonUp(buttons[i].name))
			{
				buttons[i].pressed = false;
				buttons[i].releasedEvent.SafeCall();
			}
			else if (buttons[i].pressed == true)
				buttons[i].onPressedStay.SafeCall();
		}

		for (int i = 0; i < axis.Count; i++)
		{
			axis[i].prevValue = axis[i].value;
			axis[i].value = Input.GetAxis(axis[i].name);

			if (axis[i].prevValue != 0 && axis[i].value == 0)
			{
				axis[i].releasedEvent.SafeCall();
				axis[i].negative = axis[i].pressed = false;
			}
			else if (axis[i].prevValue <= 0 && axis[i].value > 0)
			{
				axis[i].pressed = true;
				axis[i].negative = false;
				axis[i].pressedEvent.SafeCall();
			}
			else if (axis[i].prevValue >= 0 && axis[i].value < 0)
			{
				axis[i].negative = true;
				axis[i].pressed = false;
				axis[i].negativeEvent.SafeCall();
			}
			else if (axis[i].pressed == true)
				axis[i].onPressedStay.SafeCall();
			else if (axis[i].negative == true)
				axis[i].onNegativeStay.SafeCall();
		}
	}

	public float GetAxisByName(string name)
	{
		for (int i = 0; i < axis.Count; i++)
		{
			if (axis[i].name == name)
			{
				return axis[i].value;
			}
		}
		return 0f;
	}

	public void Reset()
	{
		foreach (InputButton butt in buttons)
		{
			butt.Reset();
		}
		foreach (InputAxis ax in axis)
		{
			ax.Reset();
		}
	}

	#endregion
}
