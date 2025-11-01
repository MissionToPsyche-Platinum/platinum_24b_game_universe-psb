using UnityEngine;
using System.Collections;

public class PlayCard : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Vector3 targetPosition;
    private float hoverScale = 1.2f;
    private float normalScale = 1f;
    private float moveSpeed = 10f;

    private HandManager handManager;
    private Camera mainCam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = Camera.main;
        handManager = FindObjectOfType<HandManager>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDragging)
        {
            // Smoothly move to target position (gravitating to hand)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
    }

    void OnMouseEnter()
    {
        // Enlarge slightly on hover
        StopAllCoroutines();
        StartCoroutine(ScaleTo(hoverScale, 0.1f));
        transform.localPosition += Vector3.up * 0.2f; // move up slightly
    }

    void OnMouseExit()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(normalScale, 0.1f));
        transform.localPosition -= Vector3.up * 0.2f;
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        isDragging = false;
        // Snap back to hand layout
        if (handManager != null)
            targetPosition = handManager.GetCardTargetPosition(this);
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 10f; // distance from camera
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
}
