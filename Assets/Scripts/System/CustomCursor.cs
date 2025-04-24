using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1.5f; // ���x

    float maxValue = 2.0f;
    float miniValue = 0.05f;

    private Vector3 cursorPosition; // �^���J�[�\���̈ʒu


    float left = 0;
    float right = 0;
    float bottom = 0;
    float top = 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // �J�[�\�����\���ɂ���
    }

    void Start()
    {
        // �����ʒu����ʒ����ɐݒ�
        cursorPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10f));
        transform.position = cursorPosition;


        Camera cam = Camera.main;


        // �J�����̒��S���W
        Vector3 camPos = cam.transform.position;

        // �J�����̏c���̔���
        float halfHeight = cam.orthographicSize;

        // �J�����̉����̔���
        float halfWidth = halfHeight * cam.aspect;

        // ���E�E�E���E��̃��[���h���W
        left   = camPos.x - halfWidth;
        right  = camPos.x + halfWidth;
        bottom = camPos.y - halfHeight;
        top    = camPos.y + halfHeight;
    }

    void Update()
    {

        // �}�E�X�z�C�[���Ŋ��x��ύX
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            mouseSensitivity += scroll * 0.5f; // 0.5������
            mouseSensitivity = Mathf.Clamp(mouseSensitivity, miniValue, maxValue);
        }

        



        // �}�E�X�̈ړ��ʂ��擾�i���x�K�p�j
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100f;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100f;




        //�J�[�\������ʍ��[���������̏���
        if(cursorPosition.x <= left)
        {
            if(Input.GetAxis("Mouse X") < 0) { moveX = 0; }
        }
        //�J�[�\������ʉE�[���������̏���
        if (cursorPosition.x >= right)
        {
            if (Input.GetAxis("Mouse X") > 0) { moveX = 0; }
        }
        //�J�[�\������ʉ��[���������̏���
        if (cursorPosition.y <= bottom)
        {
            if (Input.GetAxis("Mouse Y") < 0) { moveY = 0; }
        }
        //�J�[�\������ʏ�[���������̏���
        if (cursorPosition.y >= top)
        {
            if (Input.GetAxis("Mouse Y") > 0) { moveY = 0; }
        }

        // �ړ���̃^�[�Q�b�g�ʒu���v�Z
        Vector3 targetPosition = cursorPosition + new Vector3(moveX, moveY, 0);

        // ���݂̈ʒu����^�[�Q�b�g�ʒu�֏��X�Ɉړ�
        cursorPosition = targetPosition;// �������̂ق����Y��ɔ��f����Ă�C������(�G���Ă݂����o)

        // ��ʓ��ɃJ�[�\���𐧌�
        Vector3 clampedPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            Mathf.Clamp(Camera.main.WorldToScreenPoint(cursorPosition).x, 0, Screen.width),
            Mathf.Clamp(Camera.main.WorldToScreenPoint(cursorPosition).y, 0, Screen.height),
            10f
        ));

        transform.position = clampedPosition;

        if (Input.GetKeyDown(KeyCode.Escape)) // ESC�L�[�ŃJ�[�\�����ĕ\��
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