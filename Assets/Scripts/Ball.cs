using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    public TMP_Text ballSpeedText;

    private float OriginalSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        OriginalSpeed = speed;
        ResetBall();
    }

    private void Update()
    {
        if (ballSpeedText != null)
        {
            ballSpeedText.text = this.rb.velocity.magnitude.ToString();
        }
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = new()
        {
            x = Random.Range(-1f, 1f),
            y = -1f
        };

        rb.AddForce(force.normalized * speed, ForceMode2D.Impulse);
    }

    public void ResetBall()
    {
        speed = OriginalSpeed;
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;

        CancelInvoke();
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    public void IncreaseSpeed(float value)
    {
        speed += value;
    }
}
