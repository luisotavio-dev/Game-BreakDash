using System.Collections.Generic;
using UnityEngine;

public class BrickSpawn : MonoBehaviour
{
    public GameObject canvas;
    public int rows;
    public int columns;
    public float spacing;
    public GameObject brickPrefab; // Prefab do bloco
    public Gradient gradient;

    void Start()
    {
        ResetBlocks();
    }

    void ResetBlocks()
    {
        // Obter o tamanho da tela em unidades de espaço de jogo
        float screenWidth = canvas.GetComponent<RectTransform>().rect.width;
        float screenHeight = canvas.GetComponent<RectTransform>().rect.height;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector2 spawnPos = (Vector2)transform.position + new Vector2(
                    x * (brickPrefab.transform.localScale.x + spacing),
                    -y * (brickPrefab.transform.localScale.y + spacing)
                );
                GameObject newBrick = Instantiate(brickPrefab, spawnPos, Quaternion.identity);
                newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)y / (columns - 1));
            }
        }
    }
}
