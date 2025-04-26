using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlassShatterTransition : MonoBehaviour
{
    public int fragmentCountX = 10;
    public int fragmentCountY = 10;
    public GameObject fragmentPrefab;
    public string nextSceneName;

    private Texture2D capturedTexture;
    private RawImage screenImage;

    private void Start()
    {
        StartCoroutine(CaptureScreenAndShatter());
    }

    IEnumerator CaptureScreenAndShatter()
    {
        yield return new WaitForEndOfFrame();

        // 画面キャプチャ
        capturedTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        capturedTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        capturedTexture.Apply();

        // キャプチャ画像から破片を作る
        CreateFragments();

        // キャプチャ画像本体はもう不要
        Destroy(capturedTexture);
        capturedTexture = null;

        // 少し待ってシーン遷移
        yield return new WaitForSeconds(1.5f);
        //SceneManager.LoadScene(nextSceneName);
        Debug.Log("シーン変わるよ");
    }

    void CreateFragments()
    {
        float fragWidth = Screen.width / (float)fragmentCountX;
        float fragHeight = Screen.height / (float)fragmentCountY;

        for (int y = 0; y < fragmentCountY; y++)
        {
            for (int x = 0; x < fragmentCountX; x++)
            {
                // 破片の画像部分を切り抜き
                Texture2D fragTex = new Texture2D((int)fragWidth, (int)fragHeight, TextureFormat.RGB24, false);
                fragTex.SetPixels(capturedTexture.GetPixels(
                    (int)(x * fragWidth),
                    (int)(y * fragHeight),
                    (int)fragWidth,
                    (int)fragHeight
                ));
                fragTex.Apply();

                // Fragment生成
                GameObject frag = Instantiate(fragmentPrefab, transform);

                // 画像設定
                RawImage fragImage = frag.GetComponent<RawImage>();
                fragImage.texture = fragTex;
                fragImage.SetNativeSize();

                // RectTransform設定
                RectTransform fragRect = frag.GetComponent<RectTransform>();
                fragRect.sizeDelta = new Vector2(fragWidth, fragHeight);
                fragRect.anchorMin = new Vector2(0, 0);
                fragRect.anchorMax = new Vector2(0, 0);
                fragRect.pivot = new Vector2(0.5f, 0.5f);
                fragRect.position = new Vector2(x * fragWidth + fragWidth / 2, y * fragHeight + fragHeight / 2);

                // Rigidbody2Dで飛ばす
                Rigidbody2D rb = frag.AddComponent<Rigidbody2D>();
                float forceX = Random.Range(-300f, 300f);
                float forceY = Random.Range(100f, 500f);
                rb.AddForce(new Vector2(forceX, forceY),ForceMode2D.Impulse);
                rb.AddTorque(Random.Range(-500f, 500f));
            }
        }
    }
}
