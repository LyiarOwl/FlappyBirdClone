using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject _gameController;
    GameController _gameContr;
    Rigidbody2D _rb;
    bool _mustJump;
    public float JumpForce;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _gameContr = _gameController.GetComponent<GameController>();
    }
    private void Update()
    {
        if (!_gameContr.IsGameOver)
        {
            if (Input.GetButtonDown("Jump") && !_mustJump) 
            {
                _mustJump = true;
                _gameContr.IsGameBegins = true;
                _rb.bodyType = RigidbodyType2D.Dynamic;
                _gameContr.BeginGame();
            }
        }
    }
    private void FixedUpdate()
    {
        if (!_gameContr.IsGameOver)
        {
            if (_mustJump)
            {
                _rb.velocity = Vector2.up * JumpForce;
                _mustJump = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pipe") || other.gameObject.CompareTag("Ground"))
        {
            _gameContr.IsGameOver = true;
            _gameContr.IsGameBegins = false;
            _gameContr.SetGameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Scoring"))
        {
            _gameContr.IncreaseScore();
        }
    }
}
