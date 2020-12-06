using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{


    private Sword _sword;
    private bool _swinging = false;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;


    public void SetSword(Sword sword)
    {
        this._sword = sword;
        CreateComponents();
    }

    public void SwingSword() {
        if (!_swinging)
            StartCoroutine(SwingAnimation());
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("sword hit enemy");
        }

    }

    private Vector2Int CalculateSwordSize()
    {
        int width = Mathf.Clamp( Mathf.RoundToInt(_sword.BaseDamage/ 100f), 1, 5 );
        int length = _sword.Password.String.Length;
        return new Vector2Int(length, width+2);
    }

    private void CreateComponents()
    {
        Vector2Int size = CalculateSwordSize();

        _collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        _collider.size = new Vector2(size.x, size.y) / 8f;
        _collider.offset = _collider.size / 2f * Vector2.down;
        _collider.enabled = false;

        _spriteRenderer = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        DrawSwordSprite(size);
    }

    private void DrawSwordSprite(Vector2Int size)
    {
        string password = _sword.Password.String;

        // TODO: make not arbitrary calculation
        Color color = Color.HSVToRGB(Random.value, 1, 1);

        // texture and sword size oriented →
        Texture2D texture = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);

        Color[] allClearPixels = new Color[size.x * size.y];
        for (int i = 0; i < allClearPixels.Length; i++)
        {
            allClearPixels[i] = Color.clear;
        }
        texture.SetPixels(allClearPixels);

        // Hilt
        texture.SetPixel(1, 0, color);
        texture.SetPixel(1, size.y-1, color);

        // Blade
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 1; j < size.y-1; j++)
            {
                texture.SetPixel(i, j, color);
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();

        Rect rect = new Rect(Vector2.zero, size);
        Vector2Int pivot = new Vector2Int(0, Mathf.CeilToInt(size.y/2f)-1);
        Sprite swordSprite = Sprite.Create(texture, rect, pivot, 8f);

        _spriteRenderer.sprite = swordSprite;
    }

    private IEnumerator SwingAnimation()
    {
        _swinging = true;
        _collider.enabled = true;

        float angle = _sword.SwingAngle/2f;
        float targetAngle = -angle;
        transform.localRotation = Quaternion.Euler(0, 0, angle);

        while (angle > targetAngle) {
            float percent = (angle-targetAngle)/_sword.SwingAngle;
            angle -= _sword.SwingSpeed * (2-percent);
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            yield return new WaitForFixedUpdate();
        }
        _collider.enabled = false;
        yield return new WaitForSeconds(1f/_sword.SwingSpeed);

        transform.localRotation = Quaternion.identity;
        _swinging = false;
    }

}
