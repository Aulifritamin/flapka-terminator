using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenuPanel;
    [SerializeField] Player _player;

    private bool _isPaused = false;

    private void OnEnable()
    {
        _player.OnDie += TogglePause;

    }

    private void OnDisable()
    {
        _player.OnDie -= TogglePause;
    }


    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f;
            _mainMenuPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _mainMenuPanel.SetActive(false);
        }
    }
}
