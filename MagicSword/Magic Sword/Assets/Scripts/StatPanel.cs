using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPanel : MonoBehaviour {

	[SerializeField] StatDisplay[] statDisplays;

	private void OnValidate()
	{
		statDisplays = GetComponentsInChildren<StatDisplay>();
	}
}
