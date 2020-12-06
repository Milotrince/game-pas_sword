using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public Text TitleText;
    public Text ContinueText;
    private bool _loading;

    // Start is called before the first frame update
    void Start()
    {
        _loading = false;
        StartCoroutine(FadeIn(TitleText));
        StartCoroutine(Blink(ContinueText));
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            _loading = true;
            SceneManager.LoadScene("GameScene");
        }

    }

    private IEnumerator FadeIn(Text text)
    {
        Color c = text.color;
        float alpha = 0f;
        while (alpha < 1f) {
            text.color = new Color(c.r, c.g, c.b, alpha);
            alpha += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Blink(Text text)
    {
        Color color = text.color;
        bool visible = false;
        text.color = Color.clear; 
        yield return new WaitForSeconds(1f);
        while (!_loading) {
            text.color = visible ? color : Color.clear;
            visible = !visible;
            yield return new WaitForSeconds(1f);
        }
    }



}
