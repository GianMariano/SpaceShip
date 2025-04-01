using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public Vector3 direction = Vector3.right; // Mudança crítica: agora vai para a DIREITA (eixo X)
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movimenta o projétil
        transform.Translate(direction * speed * Time.deltaTime);

        // Destrói se sair da tela (opcional)
        if (Mathf.Abs(transform.position.x) > 10f || Mathf.Abs(transform.position.y) > 8f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Invader")) // Certifique-se que os inimigos têm a tag "Invader"!
        {
            Invaderr invader = other.GetComponent<Invaderr>();
            if (invader != null)
            {
                GameManager.Instance.OnInvaderKilled(invader);
            }
            Destroy(other.gameObject); // Destrói o inimigo
            Destroy(gameObject); // Destrói o projétil
        }
    }
}