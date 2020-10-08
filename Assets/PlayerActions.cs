using UnityEngine;

namespace Player.Actions
{
    public class PlayerActions : MonoBehaviour
    {
        private const string Horizontal = "Horizontal";
        private const string Ground = "Ground";

        private bool _onGround;
        protected AudioSource _source;
        protected BoxCollider2D bc;

        protected int movementSpeed = 10;
        protected Rigidbody2D player;
        private int runningSpeed => movementSpeed * 2;

        public void Start()
        {
            player = GetComponent<Rigidbody2D>();
            _onGround = false;
            bc = gameObject.AddComponent<BoxCollider2D>();
            _source = GetComponent<AudioSource>();
        }


        public void Update()
        {
            var dt = Time.deltaTime;
            var movement =
                new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (movement.magnitude > 1) //Normalizing movement
                movement = movement / movement.magnitude;

            transform.Translate(movement * dt * movementSpeed);

            if (IsGrounded() && Input.GetButtonDown("Jump")) //rb is the player's rigidbody
                player.velocity = new Vector3(player.velocity.x, movementSpeed);
        }

        public void OnCollisionEnter2D(Collision2D colInfo)
        {
            if (colInfo.gameObject.CompareTag(Ground))
            {
                _onGround = true;
                _source.Play();
            }
        }

        public void OnCollisionExit2D(Collision2D colInfo)
        {
            if (colInfo.gameObject.CompareTag(Ground)) _onGround = false;
        }

        public void OnCollisionStay2D(Collision2D colInfo)
        {
            if (colInfo.gameObject.CompareTag(Ground)) _onGround = true;
        }

        //...
        public bool IsGrounded()
        {
            return _onGround;
        }
    }
}