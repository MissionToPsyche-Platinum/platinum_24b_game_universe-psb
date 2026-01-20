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
    private bool isLockedToPlayPile = false;

    private HandManager handManager;
    private Camera mainCam;
    private int originalSortingOrder;
    private SpriteRenderer spriteRenderer;
    private PlayPileDropZone playPileZone;
    private Rigidbody2D rb2d;

    // Sway variables
    private Vector3 previousMousePos;
    private Vector3 currentMouseVelocity;
    private float maxSwayRotation = 30f;
    private float swaySmoothing = 0.1f;
    private Tween swayTween;


    //for loop integration
    public PsychePlayer Player { get; private set; }
    public GameLoop GameLoop { get; private set; }

    public void SetOwner(PsychePlayer player, GameLoop gameLoop)
    {
        Player = player;
        GameLoop = gameLoop;
    }

    void Start()
    {
        mainCam = Camera.main;
        handManager = FindAnyObjectByType<HandManager>();
        playPileZone = FindAnyObjectByType<PlayPileDropZone>();
        targetPosition = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalSortingOrder = spriteRenderer.sortingOrder;
        
        baseScale = transform.localScale; // store actual prefab scale

        // Get or add Rigidbody2D for collision detection
        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }
        rb2d.bodyType = RigidbodyType2D.Kinematic; // Kinematic so it doesn't fall or rotate from physics
        rb2d.gravityScale = 0;
        rb2d.linearVelocity = Vector2.zero;
        rb2d.angularVelocity = 0;
        
        Debug.Log("Card " + name + " has Rigidbody2D: " + (rb2d != null));

        if (handManager != null)
            handManager.RegisterCard(this);
    }

    void Update()
    {
        if (isLockedToPlayPile)
        {
            // Card is locked to the play pile, don't move it
            return;
        }

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

    // change card sprite
    public void SetCardSprite(Sprite newSprite)
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = newSprite;
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

        if (playPileZone != null)
            playPileZone.ShowZone();

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

        if (playPileZone != null)
        {
            if (playPileZone.isCardInside && playPileZone.currentCard == this)
            {
                // Lock card in place in the pile
                transform.position = playPileZone.transform.position;
               
               //handManager.UnregisterCard(this);
                isLockedToPlayPile = true;

                // Show confirm button and pass this card
                UIPlayConfirm.Instance.ShowButton(this); //, Player, GameLoop);

                return; // STOP normal hand repositioning
            }

            // Hide confirm button
            UIPlayConfirm.Instance.HideButton();
            playPileZone.HideZone();
            isLockedToPlayPile = false;
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

    public bool IsBeingDragged()
    {
        return isDragging;
    }

    public void UnlockFromPlayPile()
    {
        isLockedToPlayPile = false;
        handManager.ReorderCard(this, transform.position.x);
        handManager.ClearDraggingCard();
        targetPosition = handManager.GetCardTargetPosition(this);
        // Reset rotation with DOTween
        if (swayTween != null && swayTween.IsActive())
            swayTween.Kill();
        swayTween = transform.DOLocalRotate(Vector3.zero, 0.8f);
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
