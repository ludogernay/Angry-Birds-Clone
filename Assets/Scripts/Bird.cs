using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchforce = 500;
    [SerializeField] float _maxDragDistance = 5;
    [SerializeField] ParticleSystem _particleSystem;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    bool _hasDied;
    Animator _animator;
    void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }
    void OnMouseDown() {
        _spriteRenderer.color = Color.red;
    }
    void OnMouseUp() {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _spriteRenderer.color = Color.white;
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchforce);
    }
    void OnMouseDrag() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;
        float distance = Vector2.Distance(desiredPosition, _startPosition);
        if (distance > _maxDragDistance) {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistance);
        }
        if (desiredPosition.x > _startPosition.x) {
            desiredPosition.x = _startPosition.x;
        }
        _rigidbody2D.position = desiredPosition;
    }
    void OnCollisionEnter2D(Collision2D other) {
        if (!_hasDied) {
           // _animator.SetBool("Dead",true);
            _hasDied = true;
            _rigidbody2D.freezeRotation = false;
            StartCoroutine(ResetAfterDelay());
        }
    }
    IEnumerator ResetAfterDelay() {
        yield return new WaitForSeconds(3);
        _particleSystem.Play();
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(2);
        _animator.SetBool("Dead",true);
        _rigidbody2D.freezeRotation = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.rotation = 0;
        yield return new WaitForSeconds(1);
        _spriteRenderer.enabled = true;
        _hasDied = false;
    }
}
