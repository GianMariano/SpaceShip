using UnityEngine;

public class MysteryShip : MonoBehaviour
{
    public float speed = 5f;
    public float cycleTime = 30f;
    public int score = 300;
    private Vector2 topDestination;
    private Vector2 bottomDestination;
    private int direction = -1; // -1 = desce, 1 = sobe
    private bool spawned;

    private void Start()
    {
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(Vector3.up);
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        topDestination = new Vector2(transform.position.x, topEdge.y + 1f);
        bottomDestination = new Vector2(transform.position.x, bottomEdge.y - 1f);
        Spawn();
    }

    private void Update()
    {
        if (!spawned) return;

        if (direction == 1)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    private void MoveUp()
    {
        transform.position += speed * Time.deltaTime * Vector3.up;
        if (transform.position.y >= topDestination.y) Despawn();
    }

    private void MoveDown()
    {
        transform.position += speed * Time.deltaTime * Vector3.down;
        if (transform.position.y <= bottomDestination.y) Despawn();
    }

    private void Spawn()
    {
        direction *= -1;
        transform.position = (direction == 1) ? bottomDestination : topDestination;
        spawned = true;
    }

    private void Despawn()
    {
        spawned = false;
        Invoke(nameof(Spawn), cycleTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            GameManager.Instance.OnMysteryShipKilled(this);
            Despawn();
            Destroy(gameObject);
        }
    }
}