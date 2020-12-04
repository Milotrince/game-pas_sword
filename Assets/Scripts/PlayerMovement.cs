using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;
    public Rigidbody2D rb;

    private Vector2 moveVector;
    private PlayerInputAction inputActions;

    void Awake() {
        inputActions = new PlayerInputAction();
        inputActions.Enable();
    }

    void onEnable() {
        inputActions.Enable();
    }

    void onDisable() {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update() {
        moveVector = inputActions.Player.Movement.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.MovePosition(rb.position + moveVector * speed * Time.fixedDeltaTime);
    }

}
