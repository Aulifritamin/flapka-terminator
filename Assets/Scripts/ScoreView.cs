using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreAmount;

    private float _currentScore = 0f;

    private void Update()
    {
        _currentScore += Time.deltaTime;
        _scoreAmount.text = Mathf.FloorToInt(_currentScore).ToString();
    }
}
