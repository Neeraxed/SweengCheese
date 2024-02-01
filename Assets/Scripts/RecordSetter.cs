using UnityEngine;
using UnityEngine.UI;

public class RecordSetter : MonoBehaviour
{
    public Text highScore;
    private void OnEnable()
    {
        highScore.text = PlayerPrefs.GetInt("CheeseScore", 0).ToString();
    }
}
