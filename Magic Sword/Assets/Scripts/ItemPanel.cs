using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanel : MonoBehaviour {

	[SerializeField] StatDisplay[] statDisplays;

	private void OnValidate()
	{
		statDisplays = GetComponentsInChildren<StatDisplay>();
	}
}
