using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<remarks>
/// Controls You and sword 
///</remarks>
public class PlayerController: MonoBehaviour
{
    public float MoveSpeed = 12f;
    public Rigidbody2D Rigidbody;


    private Vector2 _moveVector;
    private PlayerInputAction _inputActions;
    private bool _flipped = false;
    private SpriteRenderer[] _spriteRenderers;
    private Transform[] _childTransforms;
    private SwordRenderer _swordRenderer;
    [SerializeField] private Sword _sword;

    void Awake()
    {
        _sword = new Sword(new Password("0123"));
        _swordRenderer = GetComponentInChildren<SwordRenderer>();
        _swordRenderer.SetSword(_sword);

        _inputActions = new PlayerInputAction();
        _inputActions.Enable();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        var allTransforms = new List<Transform>(GetComponentsInChildren<Transform>());
        allTransforms.Remove(transform); // Don't want to flip parent
        _childTransforms = allTransforms.ToArray();
    }

    void onEnable()
    {
        _inputActions.Enable();
    }

    void onDisable()
    {
        _inputActions.Disable();
    }

    void Update()
    {
        // movement
        _moveVector = _inputActions.Player.Movement.ReadValue<Vector2>();
        UpdateSprite();

        // attack
        
    }

    void FixedUpdate()
    {
        Rigidbody.velocity = _moveVector * MoveSpeed;
    }

    void UpdateSprite()
    {
        float epsilon = 0.01f;
        bool shouldFlipLeft = !_flipped && _moveVector.x < -epsilon;
        bool shouldFlipRight = _flipped && _moveVector.x > epsilon;
        if (shouldFlipLeft || shouldFlipRight) {
            foreach (SpriteRenderer sr in _spriteRenderers) {
                sr.flipX = !sr.flipX;
            }
            foreach (Transform tf in _childTransforms) {
                tf.localPosition = new Vector3(-tf.localPosition.x, tf.localPosition.y, 0f);
            }
            _flipped = !_flipped;
        }
    }

    IEnumerator Swing()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f) {
            yield return null;
        }

    }

}
