using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour 
{
	float startWidth;

	private RectTransform rt;
	


	// Use this for initialization
	void Start ()
	{
		rt = (RectTransform) transform;
		startWidth = rt.rect.width;

	}
	
	// Update is called once per frame
	void Update () 
	{
		rt.sizeDelta = new Vector2(GameController.Instance.GetRemainingTimeRatio() * startWidth, rt.sizeDelta.y);
	}
}
