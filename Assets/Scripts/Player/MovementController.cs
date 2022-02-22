using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1;
    private TileData currentTile;
    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;

    void Awake () {
        InitializeReferences();
    }

    void Update () {
        GetCurrentTileData();
        ControlMovement();
    }

    void InitializeReferences () {
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    void GetCurrentTileData () {
        currentTile = TileController.Instance.GetTileData(transform.position);
    }

    void ControlMovement () {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 predicitedPosition = new Vector3(transform.position.x + horizontal * movementSpeed, transform.position.y + vertical * movementSpeed, 0);
        TileData predicitedTile = TileController.Instance.GetTileData(predicitedPosition);
        if (predicitedTile.passable) {
            this.transform.Translate(new Vector3(horizontal * movementSpeed * currentTile.walkingSpeed, vertical * movementSpeed * currentTile.walkingSpeed, 0));
        }
        DetermineAnimatonDirection(horizontal, vertical);
    }

    void DetermineAnimatonDirection (float horizontal, float vertical) {
        if (horizontal > 0) {
            playerRenderer.flipX =true;
        } else if (horizontal < 0) {
            playerRenderer.flipX = false;
        }
        if (horizontal == 1 || vertical == 1 || horizontal == -1 || vertical == -1) {
                playerAnimator.SetBool("Moving", true);
            } else {
                playerAnimator.SetBool("Moving", false);
            }
    }
}
