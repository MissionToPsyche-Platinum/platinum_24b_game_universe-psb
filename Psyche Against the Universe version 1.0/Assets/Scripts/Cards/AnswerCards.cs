using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;

[RequireComponent(typeof(CardUI))]
[RequireComponent(typeof(CardMovement))]
public class AnswerCards : MonoBehaviour,
   // IPointerDownHandler,
   // IDragHandler,
    //IPointerUpHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    // -------------------------
    // DATA
    // -------------------------
    public string PlayedBy { get; set; }

    [field: SerializeField]
    public ScriptableCard CardData { get; private set; }

    public PsychePlayer Player { get; private set; }
    public GameLoop GameLoop { get; private set; }

    public void SetUp(ScriptableCard data)
    {
        CardData = data;
        GetComponent<CardUI>().SetCardUI();
    }

    public void SetOwner(PsychePlayer player, GameLoop gameLoop)
    {
        Player = player;
        GameLoop = gameLoop;
    }

    // -------------------------
    // MOVEMENT + DRAGGING
    // -------------------------
    //private bool isDragging = false;
    //private Vector3 offset;
    private Vector3 targetPosition;
    //private float moveSpeed = 10f;

   // private Camera mainCam;
    private HandManager handManager;
    private PlayPileDropZone playPileZone;

    // -------------------------
    // VISUALS
    // -------------------------
   // private SpriteRenderer spriteRenderer;
   // private int originalSortingOrder;
    private Vector3 baseScale;
    private float hoverMultiplier = 1.2f;

    // -------------------------
    // SWAY
    // -------------------------
    private Vector3 previousMousePos;
    private Vector3 currentMouseVelocity;
    private float maxSwayRotation = 30f;
    private float swaySmoothing = 0.1f;
    private Tween swayTween;

    // -------------------------
    // PLAY PILE LOCK
    // -------------------------
    private bool isLockedToPlayPile = false;

    private void Start()
    {
       // mainCam = Camera.main;
       // handManager = FindAnyObjectByType<HandManager>();
       // playPileZone = FindAnyObjectByType<PlayPileDropZone>();

        targetPosition = transform.position;

      //  spriteRenderer = GetComponent<SpriteRenderer>();
      //  if (spriteRenderer != null)
            //originalSortingOrder = spriteRenderer.sortingOrder;

        baseScale = transform.localScale;

        // Rigidbody2D for pile detection
        //Rigidbody2D rb2d = gameObject.GetComponent<Rigidbody2D>();
        //if (rb2d == null)
           // rb2d = gameObject.AddComponent<Rigidbody2D>();

       // rb2d.bodyType = RigidbodyType2D.Kinematic;
       // rb2d.gravityScale = 0;

        // Register with HandManager
      //  if (handManager != null)
           // handManager.RegisterCard(this);
    }

    private void Update()
    {
        if (isLockedToPlayPile)
           return;
    
       //if (!isDragging)
       // {
         //  transform.position = Vector3.Lerp(
            //   transform.position,
            //   targetPosition,
            //  Time.deltaTime * 10f
           // );
       // }
       // else
       // {
        //    currentMouseVelocity = Vector3.Lerp(
        //        currentMouseVelocity,
         //       Vector3.zero,
         //       Time.deltaTime * 5f
         //   );

         //   ApplySway();
       // }
    }

    // -------------------------
    // POINTER EVENTS
    // -------------------------

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        
        StartCoroutine(ScaleTo(baseScale.x * hoverMultiplier, 0.1f));
        //transform.localPosition += Vector3.up * (0.2f * baseScale.y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(baseScale.x, 0.1f));
        //transform.localPosition -= Vector3.up * (0.2f * baseScale.y);
    }

    public void LockToPlayPile()
    {
        isLockedToPlayPile = true;
    }

    /* public void OnPointerDown(PointerEventData eventData)
     {
         isDragging = true;

         if (playPileZone != null)
             playPileZone.ShowZone();

         if (spriteRenderer != null)
             spriteRenderer.sortingOrder = 100;

         handManager.SetDraggingCard(this);

         offset = transform.position - GetMouseWorldPos(eventData);
         previousMousePos = GetMouseWorldPos(eventData);

         swayTween?.Kill();
     }*/

    /*public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = originalSortingOrder;

        handManager.ReorderCard(this, transform.position.x);
        handManager.ClearDraggingCard();
        targetPosition = handManager.GetCardTargetPosition(this);

        if (playPileZone != null)
        {
            if (playPileZone.isCardInside && playPileZone.currentCard == this)
            {
                transform.position = playPileZone.transform.position;
                isLockedToPlayPile = true;

                UIPlayConfirm.Instance.ShowButton(this);
                return;
            }

            UIPlayConfirm.Instance.HideButton();
            playPileZone.HideZone();
            isLockedToPlayPile = false;
        }

        swayTween?.Kill();
        swayTween = transform.DOLocalRotate(Vector3.zero, 0.8f);
    }*/
    // public bool IsBeingDragged()
    // {
    // return isDragging;
    // }

    /* public void OnDrag(PointerEventData eventData)
     {
         Vector3 currentMousePos = GetMouseWorldPos(eventData);
         transform.position = currentMousePos + offset;

         currentMouseVelocity = (currentMousePos - previousMousePos) / Time.deltaTime;
         previousMousePos = currentMousePos;
     }*/

    // -------------------------
    // HELPERS
    // -------------------------

    /* private Vector3 GetMouseWorldPos(PointerEventData eventData)
     {
         Vector3 mousePoint = eventData.position;
         float distance = Mathf.Abs(mainCam.transform.position.z - transform.position.z);
         mousePoint.z = distance;
         return mainCam.ScreenToWorldPoint(mousePoint);
     }*/

    private IEnumerator ScaleTo(float targetScale, float duration)
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

    public void UnlockFromPlayPile()
    {
        isLockedToPlayPile = false;

        handManager.ReorderCard(this, transform.localPosition.x);
        handManager.ClearDraggingCard();
        SetTargetPosition(handManager.GetCardTargetPosition(this));

        transform.localRotation = Quaternion.identity;
    }

    /*public void UnlockFromPlayPile()
    {
        isLockedToPlayPile = false;

        // Tell HandManager to reposition this card in the hand
        handManager.ReorderCard(this, transform.localPosition.x);
        handManager.ClearDraggingCard();

        // Move card back to its target UI position
        SetTargetPosition(handManager.GetCardTargetPosition(this));

        // Reset rotation (UI-friendly)
        transform.localRotation = Quaternion.identity;
    }*/

    // public void UnlockFromPlayPile()
    //  {
    //      isLockedToPlayPile = false;
    //      handManager.ReorderCard(this, transform.position.x);
    //      handManager.ClearDraggingCard();
    //   /  targetPosition = handManager.GetCardTargetPosition(this);

    //      swayTween?.Kill();
    //     swayTween = transform.DOLocalRotate(Vector3.zero, 0.8f);
    //  }

    //private void ApplySway()
    // {
    //  float swayAmount = Mathf.Clamp(
    //    -currentMouseVelocity.x * 0.8f,
    //   -maxSwayRotation,
    //   maxSwayRotation
    // );

    //  if (Mathf.Abs(currentMouseVelocity.x) < 0.1f)
    //     swayAmount = 0f;

    // swayTween?.Kill();
    //  swayTween = transform.DOLocalRotate(new Vector3(0, 0, swayAmount), swaySmoothing);
    //  }
}
