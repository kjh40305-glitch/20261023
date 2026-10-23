using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] Rigidbody2D _rigidbody;
    Camera _camera;
    Vector3 _direction = Vector3.left;
    float _lifeTime = 3f;
    float _timer = 0;
    public void SetBullet(Vector3 position, Vector3 direction)
    {
        _direction = direction;
        transform.position = position;
        //_rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);
    }
    void RemoveBullet()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.TakeDamage(1);
        }
        //if (collision.collider.CompareTag("Enemy"))
        //{
        //    var enemy = collision.gameObject.GetComponent<EnemyController>();
        //    enemy.TakeDamage(1);
        //}
    }
    //void OnCollisionExit2D(Collision2D collision)
    //{

    //}
    //void OnCollisionStay2D(Collision2D collision)
    //{

    //}
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        //Invoke("RemoveBullet", _lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;

        var viewPos = _camera.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0 || viewPos.x > 1)
        {
            RemoveBullet();
        }
    }
}
