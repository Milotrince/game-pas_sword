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
    private SpriteRenderer[] _spriteRenderers;
    private Transform[] _childTransforms;
    private SwordRenderer _swordRenderer;
    private Sword _sword;
    private bool _flipped = false;
    private bool _swinging = false;

    void Awake()
    {
        _sword = new Sword(new Password("0123456789"));
        _swordRenderer = GetComponentInChildren<SwordRenderer>();
        _swordRenderer.SetSword(_sword);

        _inputActions = new PlayerInputAction();
        _inputActions.Enable();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        var allTransforms = new List<Transform>(GetComponentsInChildren<Transform>());
        allTransforms.Remove(transform); // Don't want to flip parent
        _childTransforms = allTransforms.ToArray();

        // attack
        _inputActions.Player.Attack.performed += _ => SwingSword();
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
        if (shouldFlipLeft || shouldFlipRight)
        {
            foreach (SpriteRenderer sr in _spriteRenderers)
            {
                sr.flipX = !sr.flipX;
            }
            foreach (Transform tf in _childTransforms)
            {
                tf.localPosition = new Vector3(-tf.localPosition.x, tf.localPosition.y, 0f);
            }
            _flipped = !_flipped;
        }
    }

    private void SwingSword() {
        if (!_swinging)
            StartCoroutine(SwingAnimation());
    }

    IEnumerator SwingAnimation()
    {
        _swinging = true;

        Transform tf = _swordRenderer.gameObject.transform;
        float angle = _sword.SwingAngle/2f;
        float targetAngle = -angle;
        int sign = _flipped ? -1 : 1;
        tf.localRotation = Quaternion.Euler(0, 0, angle * sign);

        while (angle > targetAngle) {
            float percent = (angle-targetAngle)/_sword.SwingAngle;
            angle -= _sword.SwingSpeed * (2-percent);
            tf.localRotation = Quaternion.Euler(0, 0, angle * sign);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1f/_sword.SwingSpeed);

        tf.localRotation = Quaternion.identity;
        _swinging = false;
    }

}
