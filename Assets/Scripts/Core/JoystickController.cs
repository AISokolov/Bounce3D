using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform handle;
    [SerializeField] private BallController ballController;
    [SerializeField] private float handleRange = 60f;

    [Header("Handle Visuals")]
    [SerializeField] private Image handleImage;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite pressedSprite;

    private Vector2 input;

    public void OnPointerDown(PointerEventData eventData)
    {
        SetPressedVisual(true);
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            input = localPoint / handleRange;
            input = Vector2.ClampMagnitude(input, 1f);

            handle.anchoredPosition = input * handleRange;

            if (ballController != null)
                ballController.SetMoveInput(input);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        if (ballController != null)
            ballController.SetMoveInput(Vector2.zero);

        SetPressedVisual(false);
    }

    private void SetPressedVisual(bool isPressed)
    {
        if (handleImage == null) return;

        if (isPressed)
        {
            if (pressedSprite != null)
                handleImage.sprite = pressedSprite;
        }
        else
        {
            if (defaultSprite != null)
                handleImage.sprite = defaultSprite;
        }
    }
}
