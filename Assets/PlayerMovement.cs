using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Ground = "Ground";

    private const int jumpSpeed = 10;
    private const int moveSpeed = 10;
    public Rigidbody2D player;
    private bool _onGround;

    // Start is called before the first frame update
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        var moveHorizontal = Input.GetAxis(Horizontal);

        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
            var jumpMovement = new Vector2(0.0f, 1.0f);
            player.velocity = jumpMovement * jumpSpeed;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            var movement = new Vector2(1.0f, 0.0f);
            player.velocity = movement * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            var movement = new Vector2(-1.0f, 0.0f);
            player.velocity = movement * moveSpeed;
        }
    }


    public void OnCollisionEnter(Collision colInfo)
    {
        if (colInfo.gameObject.CompareTag("Ground")) _onGround = true;
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Ground")) _onGround = false;
    }
}