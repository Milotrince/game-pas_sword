using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Image _canvasImage;

    public void SetPassword(Password password)
    {
        _player.SetPassword(password);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        Tint(new Color(0f, 0f, 0f, 0.5f));
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        Tint(Color.clear);
    }

    public void Tint(Color color)
    {
        _canvasImage.color = color;
    }
}
