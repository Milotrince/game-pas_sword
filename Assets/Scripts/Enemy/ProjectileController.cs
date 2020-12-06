using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    protected Projectile _projectile;
    protected float _angle;
    private char _char;
    private SpriteRenderer _spriteRenderer;

    protected virtual void Initialize() {}
    protected virtual void UpdateProjectile() {}

    void Start()
    {
        // string chars = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[/]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/mem5x6");
        int unicode = (int) _char;
        Sprite sprite = sprites[unicode-33];

        _spriteRenderer = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        _spriteRenderer.sprite = sprite;
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (_projectile == null)
            return;
        UpdateProjectile();
    }

    public void SetProjectile(Projectile projectile, char character, float angle)
    {
        _projectile = projectile;
        _char = character;
        _angle = angle;
        Initialize();
    }

}
