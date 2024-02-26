using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piggies : MonoBehaviour
{
    [SerializeField] PiggiesSo _piggiesSo;
    [SerializeField] GameObject bird;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] Sprite deadSprite;
    [SerializeField] Gamemanager _gamemanager;
    CircleCollider2D _pigCollider;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _gamemanager = FindObjectOfType<Gamemanager>();
        bird = FindObjectOfType<Bird>().gameObject;
        _pigCollider = GetComponent<CircleCollider2D>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contact = other.GetContact(0); // Obtenez le premier point de contact

        // Convertir les coordonnées du point de contact du monde vers les coordonnées locales de l'objet
        Vector3 pointOfContactLocal = transform.InverseTransformPoint(contact.point);

        Debug.Log("Point de contact local : " + pointOfContactLocal.y);
        if (other.relativeVelocity.magnitude > 4 || other.gameObject == bird || (pointOfContactLocal.y > 0.3f && other.relativeVelocity.magnitude > 1))
        {
            _spriteRenderer.sprite = deadSprite;
            StartCoroutine(ResetAfterDelay());
        }
    }
    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(2);
        _particleSystem.Play();
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        _rigidbody2D.freezeRotation = true;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.rotation = 0;
        yield return new WaitForSeconds(1);
        _gamemanager.IsPiggieDied();
        gameObject.SetActive(false);
    }
}
