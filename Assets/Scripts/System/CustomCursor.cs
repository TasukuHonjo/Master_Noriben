using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1.5f; // 感度

    float maxValue = 2.0f;
    float miniValue = 0.25f;

    private Vector3 cursorPosition; // 疑似カーソルの位置

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // カーソルを非表示にする
    }

    void Start()
    {
        // 初期位置を画面中央に設定
        cursorPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        transform.position = cursorPosition;
    }

    void Update()
    {
        //if (GameManager.instance.startFg == false) { return; }

        // マウスホイールで感度を変更
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            mouseSensitivity += scroll * 0.5f; // 0.5ずつ増減
            mouseSensitivity = Mathf.Clamp(mouseSensitivity, miniValue, maxValue);
        }

        // 0.25～1.5 を 0～1 に正規化
        //gageManager.percent = (mouseSensitivity - miniValue) / (maxValue - miniValue) * 100.0f;


        // マウスの移動量を取得（感度適用）
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100f;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100f;


        // 移動後のターゲット位置を計算
        Vector3 targetPosition = cursorPosition + new Vector3(moveX, moveY, 0);

        // 現在の位置からターゲット位置へ徐々に移動
        //cursorPosition = Vector3.Lerp(cursorPosition, targetPosition, 0.1f);
        cursorPosition = targetPosition;// こっちのほうが綺麗に反映されてる気がする(触ってみた感覚)

        // 画面内にカーソルを制限
        Vector3 clampedPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Mathf.Clamp(Camera.main.WorldToScreenPoint(cursorPosition).x, 0, Screen.width),
            Mathf.Clamp(Camera.main.WorldToScreenPoint(cursorPosition).y, 0, Screen.height),
            10f
        ));

        transform.position = clampedPosition;

        if (Input.GetKeyDown(KeyCode.Escape)) // ESCキーでカーソルを再表示
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
