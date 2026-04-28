using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    private float hoverMultiplier = 1.65f;
    private float moveSpeed = 10f;
    [SerializeField] private bool isLockedToPlayPile = false;

    private HandManager handManager;
    private Camera mainCam;
    private int originalSortingOrder;
    private SpriteRenderer spriteRenderer;
    private PlayPileDropZone playPileZone;
    private Rigidbody2D rb2d;

    private Vector3 previousMousePos;
    private Vector3 currentMouseVelocity;
    private float maxSwayRotation = 30f;
    private float swaySmoothing = 0.1f;
    private Tween swayTween;

    private RectTransform rectTransform;
    private Canvas parentCanvas;

    public PsychePlayer Player { get; private set; }
    public GameLoop GameLoop { get; private set; }

    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image artworkImage;

    [SerializeField] private TMP_Text seriousText;
    [SerializeField] private TMP_Text scifiText;
    [SerializeField] private TMP_Text funnyText;
    [SerializeField] private TMP_Text chaoticText;

    private AnswerCard cardData;

    private bool isFaceDown = false;
    public bool IsFaceDown => isFaceDown;

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

        baseScale = transform.localScale;

        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
        }

        rb2d.bodyType = RigidbodyType2D.Kinematic;
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
            return;
        }

        if (!isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
        else
        {
            currentMouseVelocity = Vector3.Lerp(currentMouseVelocity, Vector3.zero, Time.deltaTime * 5f);
            ApplySway();
        }
    }

    public void SetCard(AnswerCard data)
    {
        cardData = data;

        if (titleText != null) titleText.text = data.title;
        if (descriptionText != null) descriptionText.text = data.description;

        if (backgroundImage != null) backgroundImage.sprite = data.background;
        if (artworkImage != null)
        {
            artworkImage.sprite = data.artwork;
            artworkImage.enabled = true;
        }

        if (seriousText != null) seriousText.text = data.WeightSerious.ToString();
        if (scifiText != null) scifiText.text = data.WeightSciFi.ToString();
        if (funnyText != null) funnyText.text = data.WeightFunny.ToString();
        if (chaoticText != null) chaoticText.text = data.WeightChaotic.ToString();

        isFaceDown = false;
    }

    public void ShowFaceDown()
    {
        isFaceDown = true;

        if (titleText != null) titleText.text = string.Empty;
        if (descriptionText != null) descriptionText.text = string.Empty;

        if (seriousText != null) seriousText.text = string.Empty;
        if (scifiText != null) scifiText.text = string.Empty;
        if (funnyText != null) funnyText.text = string.Empty;
        if (chaoticText != null) chaoticText.text = string.Empty;

        if (artworkImage != null) artworkImage.enabled = false;
    }

    public void ShowFaceUp()
    {
        isFaceDown = false;

        if (cardData != null)
        {
            SetCard(cardData);
        }

        if (artworkImage != null)
            artworkImage.enabled = true;
    }

    public void SetCardSprite(Sprite newSprite)
    {
        if (spriteRenderer != null)
            spriteRenderer.sprite = newSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(baseScale.x * hoverMultiplier, 0.1f));

        if (!isLockedToPlayPile)
            handManager.PlayHandHover();

        AudioManager.Instance.PlaySFX("CardHover");
        this.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(baseScale.x, 0.1f));
        handManager.PlayHandShow();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Card clicked: " + name);
        isDragging = true;

        AudioManager.Instance.PlaySFX("CardClick");

        handManager.PlayHandShow();

        if (playPileZone != null)
            playPileZone.ShowZone();

        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = 100;

        if (handManager != null)
            handManager.SetDraggingCard(this);

        offset = transform.position - GetMouseWorldPos(eventData);
        previousMousePos = GetMouseWorldPos(eventData);

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
                transform.position = playPileZone.transform.position;
                isLockedToPlayPile = true;

                playPileZone.Dim();

                UIPlayConfirm.Instance.ShowButton(this);

                return;
            }

            UIPlayConfirm.Instance.HideButton();
            playPileZone.HideZone();
            isLockedToPlayPile = false;
        }

        if (swayTween != null && swayTween.IsActive())
            swayTween.Kill();

        swayTween = transform.DOLocalRotate(Vector3.zero, 0.8f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentMousePos = GetMouseWorldPos(eventData);
        transform.position = currentMousePos + offset;

        currentMouseVelocity = (currentMousePos - previousMousePos) / Time.deltaTime;
        previousMousePos = currentMousePos;
    }

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

        if (swayTween != null && swayTween.IsActive())
            swayTween.Kill();

        swayTween = transform.DOLocalRotate(Vector3.zero, 0.8f);
    }

    public void ApplyAnswerCard(AnswerCard data)
    {
        SetCard(data);
    }

    void ApplySway()
    {
        float swayAmount = Mathf.Clamp(-currentMouseVelocity.x * 0.8f, -maxSwayRotation, maxSwayRotation);

        if (Mathf.Abs(currentMouseVelocity.x) < 0.1f)
            swayAmount = 0f;

        if (swayTween != null && swayTween.IsActive())
            swayTween.Kill();

        swayTween = transform.DOLocalRotate(new Vector3(0, 0, swayAmount), swaySmoothing);
    }
}