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
    private SwordController _swordController;
    private Sword _sword;
    private bool _flipped = false;

    public void SetPassword(Password password)
    {
        _sword = new Sword(password);
        _swordController.SetSword(_sword);
    }

    void Start()
    {
        _swordController = GetComponentInChildren<SwordController>();

        _inputActions = new PlayerInputAction();
        _inputActions.Enable();

        // attack
        _inputActions.Player.Attack.performed += _ => _swordController.SwingSword();
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
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            _flipped = !_flipped;
        }
    }



}
