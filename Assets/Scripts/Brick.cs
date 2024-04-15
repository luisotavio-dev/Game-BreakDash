using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Color[] states;
    public int points = 100;
    public bool unbreakable;

    private SpriteRenderer brickSprite;
    private int health;

    private void Awake()
    {
        brickSprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ResetBrick();
    }

    private void Hit()
    {
        if (unbreakable)
        {
            return;
        }

        health--;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        } else
        {
            brickSprite.color = this.states[this.health - 1];
        }

        GameManager.Instance.OnBrickHit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            Hit();
        }
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);

        if (!unbreakable)
        {
            health = states.Length;
            brickSprite.color = this.states[this.health - 1];
        }
    }

}
