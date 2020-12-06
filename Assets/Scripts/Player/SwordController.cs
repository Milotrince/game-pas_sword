using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{

    private Sword _sword;
    private bool _swinging = false;
    private BoxCollider2D _collider;
    private SpriteRenderer _spriteRenderer;
    // private TrailRenderer _trail;
    private bool _initialized = false;

    void Start() {
        // _trail.emitting = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            EnemyController enemy = collider.gameObject.GetComponent<EnemyController>() as EnemyController;
            enemy.Die();
        }
    }

    public void SetSword(Sword sword)
    {
        this._sword = sword;
        CreateComponents();
        _initialized = true;
    }

    public void SwingSword() {
        if (!_swinging)
            StartCoroutine(SwingAnimation());
    }


    void Update() {
        if (!_initialized)
            return;

        Vector3 swordCenter = new Vector3();
        float angle = transform.rotation.z;
        swordCenter.x = _collider.size.x * Mathf.Cos(angle) / 2f;
        swordCenter.y = _collider.size.y * Mathf.Sin(angle) / 2f;
        // _trail.transform.localPosition = swordCenter;
    }

    private Vector2Int CalculateSwordSize()
    {
        Vector2 complexity = _sword.Password.CalculateCombinations();
        int width = Mathf.Clamp( Mathf.RoundToInt(complexity.y/ 100f), 1, 5 );
        int length = _sword.Password.String.Length;
        return new Vector2Int(length, width+2);
    }

    private void CreateComponents()
    {
        Vector2Int size = CalculateSwordSize();

        _collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        _collider.size = new Vector2(size.x, size.y) / 8f;
        _collider.offset = _collider.size / 2f * new Vector2(1, -1);
        _collider.enabled = false;

        _spriteRenderer = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        DrawSwordSprite(size);

        // _trail = GetComponentInChildren<TrailRenderer>();
        // _trail.emitting = false;
        // TODO: Later: fix trail
        // _trail.startWidth = size.x/4f; 
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
        // _trail.emitting = true;

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
        // _trail.emitting = false;
        yield return new WaitForSeconds(1f/_sword.SwingSpeed);

        transform.localRotation = Quaternion.identity;
        _swinging = false;
    }


}
