
using UnityEngine;
using UnityEngine.Events;



public class OriginalButton : MonoBehaviour
{
    public enum Type
    {
        Transform,
        Custom
    }
    [SerializeField] private Type type = Type.Transform;

    public GameObject cursorObj = null;
    // インスペクタから関数を指定可能
    [SerializeField] private UnityEvent onButtonTriggerEnter;
    [SerializeField] private UnityEvent onButtonClick;
    [SerializeField] private UnityEvent onButtonTriggerExit;


    private bool onTriggerEnter = false;
    private bool onTriggerExit = false;

    private float defSizeX = 0;
    private float defSizeY = 0;

    private Vector2 defPos = Vector2.zero;
    private Vector2 defSize = Vector2.zero;

    [SerializeField] private Vector2 pos = Vector2.zero;
    [SerializeField] private Vector2 size = Vector2.zero;

    private void Awake()
    {
        defSizeX = transform.localScale.x;
        defSizeY = transform.localScale.y;
    }

    private void Start()
    {
        defPos = transform.position;
        defSize = transform.localScale;

        if (!cursorObj) { cursorObj = GameObject.Find("Cursor"); }
    }

    private void Update()
    {
        if (cursorObj == null) { return; }

        if (type == Type.Transform)
        {
            //自身のオブジェクトに重なっていたら(フィジックスではない判定)
            float _right = this.transform.position.x + this.transform.localScale.x * 0.5f;
            float _left = this.transform.position.x - this.transform.localScale.x * 0.5f;
            float _top = this.transform.position.y + this.transform.localScale.y * 0.5f;
            float _bottom = this.transform.position.y - this.transform.localScale.y * 0.5f;
            if (_right > cursorObj.transform.position.x &&
                    _left < cursorObj.transform.position.x &&
                    _top > cursorObj.transform.position.y &&
                    _bottom < cursorObj.transform.position.y
                   )
            {
                //オブジェクトが当たったら一度だけ
                if (!onTriggerEnter)
                {
                    onTriggerEnter = true;
                    onButtonTriggerEnter.Invoke(); Debug.Log("onButtonTriggerEnter");
                    onTriggerExit = false;
                }

                //左クリックが押されたら
                if (Input.GetMouseButtonDown(0))
                {
                    onButtonClick.Invoke(); Debug.Log("onButtonClick");
                }
            }
            else
            {
                //オブジェクトが抜けたら一度だけ
                if (!onTriggerExit)
                {
                    onTriggerExit = true;
                    onButtonTriggerExit.Invoke(); Debug.Log("onButtonTriggerExit");
                    onTriggerEnter = false;
                }
            }
        }
        else
        {
            Vector2 _pos = pos + defPos;
            Vector2 _size = size + defSize;

            //自身のオブジェクトに重なっていたら(フィジックスではない判定)
            float _right = _pos.x + _size.x * 0.5f;
            float _left = _pos.x - _size.x * 0.5f;
            float _top = _pos.y + _size.y * 0.5f;
            float _bottom = _pos.y - _size.y * 0.5f;
            if (_right > cursorObj.transform.position.x &&
                    _left < cursorObj.transform.position.x &&
                    _top > cursorObj.transform.position.y &&
                    _bottom < cursorObj.transform.position.y
                   )
            {
                //オブジェクトが当たったら一度だけ
                if (!onTriggerEnter)
                {
                    onTriggerEnter = true;
                    onButtonTriggerEnter.Invoke(); Debug.Log("onButtonTriggerEnter");
                    onTriggerExit = false;
                }

                //左クリックが押されたら
                if (Input.GetMouseButtonDown(0))
                {
                    onButtonClick.Invoke(); Debug.Log("onButtonClick");
                }
            }
            else
            {
                //オブジェクトが抜けたら一度だけ
                if (!onTriggerExit)
                {
                    onTriggerExit = true;
                    onButtonTriggerExit.Invoke(); Debug.Log("onButtonTriggerExit");
                    onTriggerEnter = false;
                }
            }
        }

    }

    void OnDrawGizmos()
    {
        // ギズモの色を設定（選択されていない場合の色）
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        defPos = transform.position;
        defSize = transform.localScale;
        // オブジェクトの位置に球を描画
        Gizmos.DrawCube(pos + defPos, size + defSize);
    }

    public void ThisSizeUp(float _magnification)
    {
        this.transform.localScale = new Vector3(defSizeX * _magnification, defSizeY * _magnification, this.transform.localScale.z);
    }

    public void ThisSizeDef()
    {
        this.transform.localScale = new Vector3(defSizeX, defSizeY, this.transform.localScale.z);
    }


}