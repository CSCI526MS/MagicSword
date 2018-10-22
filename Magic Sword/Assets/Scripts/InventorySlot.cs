using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class InventorySlot : MonoBehaviour , IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
, IDropHandler {
    [SerializeField] public Image image;

    private Item _item;

    public event Action<InventorySlot> OnRightClickEvent;
    public event Action<InventorySlot> OnBeginDragEvent;
    public event Action<InventorySlot> OnEndDragEvent;
    public event Action<InventorySlot> OnDragEvent;
    public event Action<InventorySlot> OnDropEvent;

    private Color normalColor = Color.white;
	private Color disabledColor = new Color(1, 1, 1, 0);
	public Item Item {
		get { return _item; }
		set {
			_item = value;
			if (_item == null) {
				// image.enabled = false;
				image.color = disabledColor;
			} else {
				// image.enabled = true;
				image.sprite = _item.icon;
				image.color =  normalColor;
			}
		}
	}

    public virtual bool CanReceiveItem(Item item) {
		return true;
	}

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("inventorySlot click");

		if (eventData != null && eventData.button == PointerEventData.InputButton.Right) {
			if (OnRightClickEvent != null) {
				OnRightClickEvent(this);
			}
		}
	}

    public void OnBeginDrag(PointerEventData eventData) {
		// originalPosition = image.transform.position;

		if (OnBeginDragEvent != null) {
			OnBeginDragEvent(this);
		}
	}
	public void OnEndDrag(PointerEventData eventData) {
		// image.transform.position = originalPosition;
		if (OnEndDragEvent != null) {
			OnEndDragEvent(this);
		}
	}
	public void OnDrag(PointerEventData eventData) {
		// image.transform.position = Input.mousePosition;
		if (OnDragEvent != null) {
			OnDragEvent(this);
		}

	}
	public void OnDrop(PointerEventData eventData) {
		// image.transform.position = Input.mousePosition;
		if (OnDropEvent != null) {
			OnDropEvent(this);
		}

	}

    protected virtual void OnValidate()
    {
        if (image == null) {
            image = GetComponent<Image>();
        }
    }
}
