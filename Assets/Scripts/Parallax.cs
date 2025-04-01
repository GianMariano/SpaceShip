using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private Vector3 startPos;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * parallaxEffect;

        if (transform.position.x <= startPos.x - length)
        {
            transform.position = new Vector3(startPos.x, transform.position.y, transform.position.z);
        }
    }
}
