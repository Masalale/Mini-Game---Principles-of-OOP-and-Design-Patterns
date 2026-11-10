using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float laneCentering = 12f;
    [SerializeField] private Tilemap walls;

    private Rigidbody2D body;
    private Vector2 moveInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Called by the Player Input component (Behavior: Send Messages) when the Move action changes
    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        // Move gives (0.71, 0.71) for diagonals, rounding turns it back into whole directions
        moveInput = new Vector2(Mathf.Round(input.x), Mathf.Round(input.y));
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsGameOver)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        Vector3Int cell = walls.WorldToCell(body.position);
        Vector2 laneCentre = walls.GetCellCenterWorld(cell);
        Vector2 direction = moveInput;

        // Corridors are one tile wide, so moving diagonally just rubs on wall corners.
        // Holding two keys means "turn when you can": take the turn once it's open and
        // the knight is lined up with it, otherwise keep going.
        if (direction.x != 0 && direction.y != 0)
        {
            bool turnIsOpen = !walls.HasTile(cell + new Vector3Int(0, (int)direction.y, 0));
            bool linedUp = Mathf.Abs(laneCentre.x - body.position.x) < 0.15f;
            if (turnIsOpen && linedUp)
            {
                direction.x = 0;
            }
            else
            {
                direction.y = 0;
            }
        }

        Vector2 velocity = direction * speed;

        // Pull the knight to the middle of the lane he's moving along so he lines up with gaps
        // instead of catching on the corner of the wall next to him.
        if (direction.x != 0)
        {
            velocity.y = Mathf.Clamp((laneCentre.y - body.position.y) * laneCentering, -speed, speed);
        }
        else if (direction.y != 0)
        {
            velocity.x = Mathf.Clamp((laneCentre.x - body.position.x) * laneCentering, -speed, speed);
        }

        body.linearVelocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsGameOver)
        {
            return;
        }

        ITouchable touchable = other.GetComponent<ITouchable>();
        if (touchable != null)
        {
            touchable.OnPlayerTouched();
        }
    }
}
