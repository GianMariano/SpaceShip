using UnityEngine;

public class InvaderProjectile : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction = Vector3.left; // Mudança crítica: agora vai para a ESQUERDA (eixo X)
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Certifique-se que o jogador tem a tag "Player"!
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (player != null)
            {
                player.Die();
                GameManager.Instance.OnPlayerKilled(player);
            }
            Destroy(gameObject);
        }
    }
}