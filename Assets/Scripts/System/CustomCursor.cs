using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1.5f; // 感度

    float maxValue = 2.0f;
    float miniValue = 0.05f;

    private Vector3 cursorPosition; // 疑似カーソルの位置


    float left = 0;
    float right = 0;
    float bottom = 0;
    float top = 0;

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


        Camera cam = Camera.main;


        // カメラの中心座標
        Vector3 camPos = cam.transform.position;

        // カメラの縦幅の半分
        float halfHeight = cam.orthographicSize;

        // カメラの横幅の半分
        float halfWidth = halfHeight * cam.aspect;

        // 左・右・下・上のワールド座標
        left   = camPos.x - halfWidth;
        right  = camPos.x + halfWidth;
        bottom = camPos.y - halfHeight;
        top    = camPos.y + halfHeight;
    }

    void Update()
    {

        // マウスホイールで感度を変更
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            mouseSensitivity += scroll * 0.5f; // 0.5ずつ増減
            mouseSensitivity = Mathf.Clamp(mouseSensitivity, miniValue, maxValue);
        }

        



        // マウスの移動量を取得（感度適用）
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100f;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100f;




        //カーソルが画面左端だった時の処理
        if(cursorPosition.x <= left)
        {
            if(Input.GetAxis("Mouse X") < 0) { moveX = 0; }
        }
        //カーソルが画面右端だった時の処理
        if (cursorPosition.x >= right)
        {
            if (Input.GetAxis("Mouse X") > 0) { moveX = 0; }
        }
        //カーソルが画面下端だった時の処理
        if (cursorPosition.y <= bottom)
        {
            if (Input.GetAxis("Mouse Y") < 0) { moveY = 0; }
        }
        //カーソルが画面上端だった時の処理
        if (cursorPosition.y >= top)
        {
            if (Input.GetAxis("Mouse Y") > 0) { moveY = 0; }
        }

        // 移動後のターゲット位置を計算
        Vector3 targetPosition = cursorPosition + new Vector3(moveX, moveY, 0);

        // 現在の位置からターゲット位置へ徐々に移動
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

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(cursorPosition, new Vector3(1, 1, 1));
    }
}