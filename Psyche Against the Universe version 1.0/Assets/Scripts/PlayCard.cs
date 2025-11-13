using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using DG.Tweening;

public class PlayCard : MonoBehaviour,
    IPointerDownHandler,
    IDragHandler,
    IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 targetPosition;
    private Vector3 baseScale;
    private float hoverMultiplier = 1.2f;
    private float moveSpeed = 10f;

    private HandManager handManager;
    private Camera mainCam;
    private int originalSortingOrder;
    private SpriteRenderer spriteRenderer;

    // Sway variables
    private Vector3 previousMousePos;
    private Vector3 currentMouseVelocity;
    private float maxSwayRotation = 30f;
    private float swaySmoothing = 0.1f;
    private Tween swayTween;

    void Start()
    {
        mainCam = Camera.main;
        handManager = FindAnyObjectByType<HandManager>();
        targetPosition = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalSortingOrder = spriteRenderer.sortingOrder;
        
        baseScale = transform.localScale; // store actual prefab scale

        if (handManager != null)
            handManager.RegisterCard(this);
    }

    void Update()
    {
        if (!isDragging)
        {
            // Smoothly move toward target position (gravitating to hand)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            // While dragging, decay velocity when mouse stops moving
            currentMouseVelocity = Vector3.Lerp(currentMouseVelocity, Vector3.zero, Time.deltaTime * 5f);
            
            // Apply sway rotation based on velocity
            ApplySway();
        }
    }

    // ---------- Pointer Events (New Input System compatible) ----------

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(baseScale.x * hoverMultiplier, 0.1f));
        transform.localPosition += Vector3.up * (0.2f * baseScale.y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(baseScale.x, 0.1f));
        transform.localPosition -= Vector3.up * (0.2f * baseScale.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Card clicked: " + name);
        isDragging = true;

        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = 100;

        if (handManager != null)
            handManager.SetDraggingCard(this);

        offset = transform.position - GetMouseWorldPos(eventData);
        previousMousePos = GetMouseWorldPos(eventData);
        
        // Kill any existing sway tween
        if (swayTween != null && swayTween.IsActive())
            swayTween.Kill();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = originalSortingOrder;

        if (handManager != null)
        {
            handManager.ReorderCard(this, transform.position.x);
            handManager.ClearDraggingCard();
            targetPosition = handManager.GetCardTargetPosition(this);
        }

        // Reset rotation with DOTween
        if (swayTween != null && swayTween.IsActive())
            swayTween.Kill();
        swayTween = transform.DOLocalRotate(Vector3.zero, 0.8f);
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentMousePos = GetMouseWorldPos(eventData);
        transform.position = currentMousePos + offset;

        // Calculate velocity
        currentMouseVelocity = (currentMousePos - previousMousePos) / Time.deltaTime;
        previousMousePos = currentMousePos;
    }


    // ---------- Helpers ----------

    Vector3 GetMouseWorldPos(PointerEventData eventData)
    {
        Vector3 mousePoint = eventData.position;
        float distance = Mathf.Abs(mainCam.transform.position.z - transform.position.z);
        mousePoint.z = distance;
        return mainCam.ScreenToWorldPoint(mousePoint);
    }

    IEnumerator ScaleTo(float targetScale, float duration)
    {
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.one * targetScale;
        float t = 0;
        while (t < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }

    public void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
    }

    void ApplySway()
    {
        // Calculate sway angle based on horizontal velocity
        float swayAmount = Mathf.Clamp(-currentMouseVelocity.x * 0.8f, -maxSwayRotation, maxSwayRotation);
        
        // If velocity is very small, reset to 0
        if (Mathf.Abs(currentMouseVelocity.x) < 0.1f)
            swayAmount = 0f;
        
        // Kill existing tween and create new smooth sway
        if (swayTween != null && swayTween.IsActive())
            swayTween.Kill();
        
        swayTween = transform.DOLocalRotate(new Vector3(0, 0, swayAmount), swaySmoothing);
    }
}
