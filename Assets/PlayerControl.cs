using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode moveUp = KeyCode.W;
    public KeyCode moveDown = KeyCode.S;
    public float speed = 5.0f;
    public float boundY = 4.0f; // Limite vertical da tela
    private Rigidbody2D rb2d;
    public Projectile laserPrefab;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        var vel = rb2d.velocity;
        if (Input.GetKey(moveUp))
        {
            vel.y = speed;
        }
        else if (Input.GetKey(moveDown))
        {
            vel.y = -speed;
        }
        else
        {
            vel.y = 0;
        }
        rb2d.velocity = vel;

        // Limita a posição vertical
        var pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -boundY, boundY);
        transform.position = pos;


        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Isso já usará a direção padrão do Projectile (horizontal)
            Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }
}