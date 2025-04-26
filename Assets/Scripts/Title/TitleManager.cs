using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    public TextMeshPro explanationTMP;

    [SerializeField] string endlessExplanation = "";
    [SerializeField] string timeAttackExplanation = "";

    public void ChangeEndlessExplanation()
    {
        explanationTMP.text = endlessExplanation;
    }

    public void ChangeTimeAttackExplanation()
    {
        explanationTMP.text = timeAttackExplanation;
    }

    public void DefaultExplanation()
    {
        explanationTMP.text = "";
    }
}
