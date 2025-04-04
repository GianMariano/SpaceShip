using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaderr : MonoBehaviour
{
    public GameObject missilePrefab;  // Refer�ncia ao prefab do proj�til
    public float fireRateMin = 1f;       // Taxa de disparo m�nima (tempo entre os tiros)
    public float fireRateMax = 3f;       // Taxa de disparo m�xima (tempo entre os tiros)
    public int score = 10;
    private bool canShoot = true;

    private void Start()
    {
        // Inicia o disparo com uma pequena espera para evitar disparos imediatos
        StartCoroutine(FireMissileCoroutine());
    }

    private void Update()
    {
        // Outras l�gicas do Invader podem ir aqui, como movimenta��o, por exemplo.
    }

    private IEnumerator FireMissileCoroutine()
    {
        // Espera um pequeno tempo antes do primeiro disparo (evita que todos atirem imediatamente)
        yield return new WaitForSeconds(Random.Range(0f, 1f));

        while (true)
        {
            if (canShoot)
            {
                // Com probabilidade de 1/3 (33%), o invader pode disparar
                if (Random.value < 0.23f)
                {
                    FireMissile();
                }
                canShoot = false;

                // Tempo de disparo aleat�rio para cada invader
                float fireRate = Random.Range(fireRateMin, fireRateMax);

                // Espera o tempo definido para o pr�ximo disparo
                yield return new WaitForSeconds(fireRate);
                canShoot = true;
            }
            yield return null;  // Espera um quadro
        }
    }

    private void FireMissile()
    {
        // Instancia o proj�til do invader
        Instantiate(missilePrefab, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o proj�til do invader atingiu o jogador
        if (other.CompareTag("Player"))
        {
            PlayerControl player = other.GetComponent<PlayerControl>();
            if (player != null)
            {
                GameManager.Instance.OnPlayerKilled(player);  // Chama o m�todo de morte do jogador
            }

            Destroy(other.gameObject);  // Destroi o jogador
            Destroy(gameObject);        // Destroi o invader
        }
    }
}