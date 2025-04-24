/*
 * ���̃X�N���v�g���A�^�b�`����GameObject�͎q�I�u�W�F�N�g�ɃQ�[�W�ƂȂ�v�f�̃I�u�W�F�N�g��ǉ�����
 */
using UnityEngine;

public class GageScript : MonoBehaviour
{
    [Range(0.0f, 100.0f)] public float percent = 0;                 //���p�[�Z���g��
    private Transform barObject = null;            //�o�[�ƂȂ�{��(size��position�𓮂����ăo�[���グ��������)
    private Transform backgroundObject = null;     //�ő�ǂ��܂ŐL�т邩(bar��������B���C���[�W)

    private float minPosY = -4, maxPosY = 0;
    private float minHeight = 0, maxHeight = 0;

    [SerializeField] private Transform youArrowTra = null;

    void Start()
    {
        //����������

        //�I�u�W�F�N�g�̏����A�^�b�`
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
        // ���`��ԁiLerp�j���g���č��WY�Ƒ傫��Y��ω�������
        float _newPosY = Mathf.Lerp(minPosY, maxPosY, percent * 0.01f);
        float _newHeight = Mathf.Lerp(minHeight, maxHeight, percent * 0.01f);

        // �ύX��K�p
        barObject.localPosition = new Vector3(barObject.localPosition.x, _newPosY, barObject.localPosition.z);
        barObject.localScale = new Vector3(barObject.localScale.x, _newHeight, barObject.localScale.z);

        if (!youArrowTra) { return; }
        _newPosY = Mathf.Lerp(minPosY, maxHeight * 0.5f, percent * 0.01f);
        youArrowTra.localPosition = new Vector3(youArrowTra.localPosition.x, _newPosY, youArrowTra.localPosition.z);

    }
}
