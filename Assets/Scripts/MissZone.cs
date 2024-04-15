using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MissZone : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            GameManager.Instance.OnBallMiss();
        }
    }
}
