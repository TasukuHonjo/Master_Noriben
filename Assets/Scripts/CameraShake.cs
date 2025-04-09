using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Vector3 defalutvelocity = Vector3.zero;
    public bool m_shakeFg = false;
    bool m_nowShake = false;
    float m_time = 0;              //ŽžŠÔ
    float m_changeTime = 0.02f;     //Ø‚è‘Ö‚¦ŽžŠÔ
    int m_shakeCount = 0;           //—h‚ê‚½‰ñ”
    int m_shakeCountMax = 20;       //—h‚ê‚éÅ‘å‰ñ”
    float m_shakeWidthDef = 2.5f;   //—h‚ê‚é•
    float m_shakeWidth = 0;         //—h‚ê‚é•
    float defRotX = 0;



    void Start()
    {
        defRotX = transform.rotation.eulerAngles.x;
    }


    void Update()
    {

        if (m_shakeFg && !m_nowShake)
        {
            m_nowShake = true;

            m_shakeWidth = m_shakeWidthDef;
        }


        if (!m_nowShake) { return; }
        m_time += Time.deltaTime;
        float rotX = defRotX;

        if (m_time >= m_changeTime)
        {
            m_time = 0;


            if (m_shakeCount % 2 == 0)
            {
                rotX += m_shakeWidth;
                transform.rotation = Quaternion.Euler(rotX, 0f, 0f);
            }

            else
            {
                rotX -= m_shakeWidth;
                transform.rotation = Quaternion.Euler(rotX, 0f, 0f);
                m_shakeWidth *= 0.7f;
            }

            ++m_shakeCount;

            if (m_shakeCount >= m_shakeCountMax)
            {
                m_shakeWidth = m_shakeWidthDef;
                m_shakeCount = 0;
                m_nowShake = false;
                m_shakeFg = false;
                transform.rotation = Quaternion.Euler(defRotX, 0f, 0f);
            }
        }
    }

    public void ShakeStart()
    {
        m_shakeFg = true;
    }
}
