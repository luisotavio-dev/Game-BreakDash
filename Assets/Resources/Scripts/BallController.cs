using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f; // Velocidade inicial da bola
    public Transform respawnPoint; // Ponto de respawn da bola em caso de morte

    private Rigidbody2D rb;
    private Vector2 initialDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartBallMovement();
    }

    void StartBallMovement()
    {
        // Define uma direção inicial para a bola (para baixo)
        initialDirection = Vector2.down;

        // Adiciona uma componente de movimento horizontal aleatório
        float randomHorizontal = Random.Range(-0.5f, 0.5f);

        // Adiciona um pequeno desvio vertical aleatório para evitar movimento vertical reto
        float randomVertical = Random.Range(-0.5f, 0.5f);

        initialDirection += new Vector2(randomHorizontal, randomVertical);

        // Normaliza o vetor para manter a mesma velocidade
        initialDirection = initialDirection.normalized;

        // Aplica a velocidade à bola
        rb.velocity = initialDirection * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.name == "DeathZone")
        {
            // Se a bola entrar na zona de morte, reinicia a posição dela
            RespawnBall();
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
    }

    void RespawnBall()
    {
        // Define a posição da bola para o ponto de respawn
        transform.position = respawnPoint.position;

        // Reinicia a velocidade da bola
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;

        // Reinicia o movimento da bola
        StartBallMovement();
    }
}
