using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 1.5f; // ���x

    float maxValue = 2.0f;
    float miniValue = 0.25f;

    private Vector3 cursorPosition; // �^���J�[�\���̈ʒu

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
    }

    void Update()
    {
        //if (GameManager.instance.startFg == false) { return; }

        // �}�E�X�z�C�[���Ŋ��x��ύX
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            mouseSensitivity += scroll * 0.5f; // 0.5������
            mouseSensitivity = Mathf.Clamp(mouseSensitivity, miniValue, maxValue);
        }

        // 0.25�`1.5 �� 0�`1 �ɐ��K��
        //gageManager.percent = (mouseSensitivity - miniValue) / (maxValue - miniValue) * 100.0f;


        // �}�E�X�̈ړ��ʂ��擾�i���x�K�p�j
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100f;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100f;


        // �ړ���̃^�[�Q�b�g�ʒu���v�Z
        Vector3 targetPosition = cursorPosition + new Vector3(moveX, moveY, 0);

        // ���݂̈ʒu����^�[�Q�b�g�ʒu�֏��X�Ɉړ�
        //cursorPosition = Vector3.Lerp(cursorPosition, targetPosition, 0.1f);
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
}
