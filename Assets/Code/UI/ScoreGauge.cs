using TMPro;
using UnityEngine;

public class ScoreGauge : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Update() => Text();
        
    private void Text()
    {
        if(text == null)
        {
            return;
        }

        text.text = ScoreManager.Instance.score.ToString();
    }
}
