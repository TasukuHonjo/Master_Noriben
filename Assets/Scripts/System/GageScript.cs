/*
 * このスクリプトをアタッチしたGameObjectは子オブジェクトにゲージとなる要素のオブジェクトを追加する
 */
using UnityEngine;

public class GageScript : MonoBehaviour
{
    [Range(0.0f, 100.0f)] public float percent = 0;                 //何パーセントか
    private Transform barObject = null;            //バーとなる本体(sizeやpositionを動かしてバーを上げ下げする)
    private Transform backgroundObject = null;     //最大どこまで伸びるか(barがこれを隠すイメージ)

    private float minPosY = -4, maxPosY = 0;
    private float minHeight = 0, maxHeight = 0;

    [SerializeField] private Transform youArrowTra = null;

    void Start()
    {
        //初期化処理

        //オブジェクトの初期アタッチ
        barObject = transform.GetChild(0);
        backgroundObject = transform.GetChild(1);
        youArrowTra = transform.GetChild(2);

        minPosY = backgroundObject.localScale.y * - 0.5f;
        maxPosY = backgroundObject.transform.position.y;
        maxHeight = backgroundObject.localScale.y;
    }


    void Update()
    {
        if (percent < 0) { return; }
        // 線形補間（Lerp）を使って座標Yと大きさYを変化させる
        float _newPosY = Mathf.Lerp(minPosY, maxPosY, percent * 0.01f);
        float _newHeight = Mathf.Lerp(minHeight, maxHeight, percent * 0.01f);

        // 変更を適用
        barObject.localPosition = new Vector3(barObject.localPosition.x, _newPosY, barObject.localPosition.z);
        barObject.localScale = new Vector3(barObject.localScale.x, _newHeight, barObject.localScale.z);

        if (!youArrowTra) { return; }
        _newPosY = Mathf.Lerp(minPosY, maxHeight * 0.5f, percent * 0.01f);
        youArrowTra.localPosition = new Vector3(youArrowTra.localPosition.x, _newPosY, youArrowTra.localPosition.z);

    }
}
