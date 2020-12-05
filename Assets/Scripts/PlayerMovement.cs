using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public Rigidbody2D rb;


    private Vector2 moveVector;
    private PlayerInputAction inputActions;
    private bool flipped = false;
    private SpriteRenderer[] spriteRenderers;
    private Transform[] childTransforms;

    void Awake() {
        inputActions = new PlayerInputAction();
        inputActions.Enable();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        var allTransforms = new List<Transform>(GetComponentsInChildren<Transform>());
        allTransforms.Remove(transform); // Don't want to flip parent
        childTransforms = allTransforms.ToArray();
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
        UpdateSprite();
    }

    // Update is called once per frame
    void FixedUpdate() {
        rb.velocity = moveVector * speed;
    }

    void UpdateSprite() {
        float epsilon = 0.01f;
        bool shouldFlipLeft = !flipped && moveVector.x < -epsilon;
        bool shouldFlipRight = flipped && moveVector.x > epsilon;
        if (shouldFlipLeft || shouldFlipRight) {
            foreach (SpriteRenderer sr in spriteRenderers) {
                sr.flipX = !sr.flipX;
            }
            foreach (Transform tf in childTransforms) {
                tf.localPosition = new Vector3(-tf.localPosition.x, tf.localPosition.y, 0f);
            }
            flipped = !flipped;
        }
    }

}
