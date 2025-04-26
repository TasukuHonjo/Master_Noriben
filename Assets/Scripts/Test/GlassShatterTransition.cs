using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlassShatterTransition : MonoBehaviour
{
    public int fragmentCountX = 10;
    public int fragmentCountY = 10;
    public GameObject fragmentPrefab;
    public string nextSceneName;

    private Texture2D capturedTexture;

    public List<GameObject> activeObj = new List<GameObject>();



    public void StartBreakScene()
    {
        StartCoroutine(CaptureScreenAndShatter());
    }

    IEnumerator CaptureScreenAndShatter()
    {
        yield return new WaitForEndOfFrame();

        // ��ʃL���v�`��
        capturedTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        capturedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        capturedTexture.Apply();

        foreach (GameObject obj in activeObj)
        {
            obj.SetActive(false);
        }

        CreateFragments();

        Destroy(capturedTexture);
        capturedTexture = null;

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextSceneName);
    }

    void CreateFragments()
    {
        float fragWidth = Screen.width / (float)fragmentCountX;
        float fragHeight = Screen.height / (float)fragmentCountY;

        for (int y = 0; y < fragmentCountY; y++)
        {
            for (int x = 0; x < fragmentCountX; x++)
            {
                // �j�ЗpTexture�쐬
                Texture2D fragTex = new Texture2D((int)fragWidth, (int)fragHeight, TextureFormat.RGBA32, false);
                fragTex.SetPixels(capturedTexture.GetPixels(
                    (int)(x * fragWidth),
                    (int)(y * fragHeight),
                    (int)fragWidth,
                    (int)fragHeight
                ));
                fragTex.Apply();

                // Sprite����
                Sprite fragSprite = Sprite.Create(
                    fragTex,
                    new Rect(0, 0, fragTex.width, fragTex.height),
                    new Vector2(0.5f, 0.5f),
                    100f // Pixels Per Unit�i�K�X�����j
                );

                // Fragment�쐬
                GameObject frag = Instantiate(fragmentPrefab, transform);

                // SpriteRenderer�ݒ�
                SpriteRenderer renderer = frag.GetComponent<SpriteRenderer>();
                renderer.sprite = fragSprite;

                // �ʒu�ݒ�
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(
                    x * fragWidth + fragWidth / 2,
                    y * fragHeight + fragHeight / 2,
                    10f // �K����Z�ʒu
                ));
                frag.transform.position = worldPos;

                // Rigidbody2D�Ŕ�΂�
                Rigidbody2D rb = frag.GetComponent<Rigidbody2D>();
                float forceX = Random.Range(-10f, 10f);
                float forceY = Random.Range(100f, 500f);
                rb.AddForce(new Vector2(forceX, 0),ForceMode2D.Impulse);
                rb.AddTorque(Random.Range(-5, 5),ForceMode2D.Impulse);
            }
        }
    }
}
