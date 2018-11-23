using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FixedJoystick : Joystick
{
    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();
    private bool touch;
    private string currScene = "LevelOne";

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        touch = false;
    }

    private void Update()
    {
        if(currScene != SceneManager.GetActiveScene().name){
            inputVector = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
            touch = false;
            this.currScene = SceneManager.GetActiveScene().name;
        }

    }

    public override void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - joystickPosition;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        touch = true;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        touch = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        touch = false;
    }

    public bool isTouched()
    {
        return touch;
    }
}