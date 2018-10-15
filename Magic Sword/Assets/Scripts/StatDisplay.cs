using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour {
	[SerializeField] Text nameText;
	[SerializeField] Text valueText;
	private void OnValidate()
	{
		Text[] texts = GetComponentsInChildren<Text>();
		nameText = texts[0];
		valueText = texts[1];
	}
}
