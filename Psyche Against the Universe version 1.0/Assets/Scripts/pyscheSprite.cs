using UnityEngine;

public class pyscheSprite : MonoBehaviour
{
    public float speed = 10f; // Units per second
    public Vector3 direction = Vector3.right; // Move to the right

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        

    }
}
