using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Hp;
    Animator _animator;
    private PlayerController _player;
    [SerializeField] GameObject _particle;

    public void TakeDamage(int damage)
    {
        Hp = Mathf.Max(0, Hp -  damage);
        if(Hp <= 0)
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetTrigger("Die");
            gameObject.layer = LayerMask.NameToLayer("DeadObject");
            Instantiate(_particle, transform.position, transform.rotation);
            Destroy(gameObject, 1.5f);
            _player.killCount++;
            return;
        }
        _animator.SetTrigger("Hit");
    }
    private void OnEnable()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp > 0)
        {
            if (gameObject.transform.position.x - _player.gameObject.transform.position.x > 0.1)
            {
                gameObject.transform.position += Vector3.left * 0.005f;
                _animator.SetBool("IsWalking", true);
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (_player.gameObject.transform.position.x - gameObject.transform.position.x > 0.1)
            {
                gameObject.transform.position += Vector3.right * 0.005f;
                _animator.SetBool("IsWalking", true);
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
