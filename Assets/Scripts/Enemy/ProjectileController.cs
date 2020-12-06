using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    protected Projectile _projectile;
    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rb;
    protected float _angle;
    private char _char;
    private int _bounces;
    private Color _bounceColor = new Color(0f, 0.8f, 0f);
    private Color _bounceColor1 = Color.green;

    protected virtual void Initialize() {}
    protected virtual void UpdateProjectile() {}
    protected virtual void OnBounce() {}

    public void SetProjectile(Projectile projectile, char character, float angle)
    {
        _projectile = projectile;
        _char = character;
        _angle = angle;
        Initialize();
    }

    void Start()
    {
        // string chars = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[/]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        _bounces = _projectile.Bounces;

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/mem5x6");
        int unicode = (int) _char;
        Sprite sprite = sprites[unicode-33];

        _spriteRenderer = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        _spriteRenderer.sprite = sprite;
        SetColor();

        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        collider.isTrigger = true;

        _rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        _rb.gravityScale = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {


        if (collider.tag == "Player")
        {
            // TODO: damage player
            Destroy(gameObject);
        }
        if (collider.tag == "Wall")
        {
            if (_bounces > 0)
            {
                OnBounce();
                _bounces--;
                SetColor();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    void FixedUpdate()
    {
        if (_projectile == null)
            return;
        UpdateProjectile();
    }

    private void SetColor()
    {
        Color color = Color.white;
        if (_projectile.SureHit)
            color = Color.red;
        else if (_projectile.Track)
            color = Color.cyan;
        else if (_bounces > 1)
            color = _bounceColor;
        else if (_bounces > 0)
            color = _bounceColor1;
        _spriteRenderer.color = color;
    }


}
