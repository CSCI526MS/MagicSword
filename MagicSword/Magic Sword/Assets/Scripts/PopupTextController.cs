using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextController : MonoBehaviour {

    private static PopupText popupText;
    private static GameObject canvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        popupText = Resources.Load<PopupText>("Prefabs/PopupText");
        
    }

    public static void CreatePopupText(string text, Transform location, Color color)
    {
        PopupText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x+Random.Range(-0.5f,0.5f), location.position.y+Random.Range(0.5f,1f)));
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.CreateText(text, color);
    }
}
