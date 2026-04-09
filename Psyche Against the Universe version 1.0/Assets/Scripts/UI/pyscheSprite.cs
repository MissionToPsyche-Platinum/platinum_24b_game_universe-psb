using UnityEngine;

public class pyscheSprite : MonoBehaviour
{
    public float speed = 10f; // Units per second
    public Vector3 direction = Vector3.right; // Move to the right
    public Vector3 directionback = Vector3.left;
    public float switchDistance = 1000f;

    private Vector3 startPosition;
    private bool hasSwitched = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        float movement = Vector3.Distance(startPosition, transform.position); 
        if (!hasSwitched && movement >= switchDistance)
        {
            direction = Vector3.left;
            hasSwitched= true;
        }
        

    }
}
