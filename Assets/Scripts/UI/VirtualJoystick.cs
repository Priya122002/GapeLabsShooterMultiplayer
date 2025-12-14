using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler
{
    [Header("UI References")]
    public RectTransform background;
    public RectTransform handle;

    [Header("Joystick Settings")]
    [Range(0f, 1f)]
    public float deadZone = 0.1f;   // Small movement ignored

    public Vector2 InputVector { get; private set; }

    float radius;

    void Awake()
    {
        // Radius = half of background size
        radius = background.sizeDelta.x * 0.5f;

        ResetJoystick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateHandle(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateHandle(eventData);
    }

    void UpdateHandle(PointerEventData eventData)
    {
        Vector2 localPoint;

        // Convert screen position to local position relative to background
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            null, // IMPORTANT for Screen Space Overlay
            out localPoint
        );

        // 🔒 1. Restrict handle inside circle
        localPoint = Vector2.ClampMagnitude(localPoint, radius);

        // Move handle
        handle.anchoredPosition = localPoint;

        // Normalize input (-1 to +1)
        Vector2 normalized = localPoint / radius;

        // 🔒 2. Dead zone restriction
        if (normalized.magnitude < deadZone)
            InputVector = Vector2.zero;
        else
            InputVector = normalized;

        // 🔒 3. Optional axis lock (UNCOMMENT if needed)
        // InputVector = new Vector2(InputVector.x, 0); // Horizontal only
        // InputVector = new Vector2(0, InputVector.y); // Vertical only
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 🔒 4. Reset joystick
        ResetJoystick();
    }

    void ResetJoystick()
    {
        handle.anchoredPosition = Vector2.zero;
        InputVector = Vector2.zero;
    }
}
