using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Hp;
    Animator _animator;
    private PlayerController _player;

    public void TakeDamage(int damage)
    {
        Hp = Mathf.Max(0, Hp -  damage);
        if(Hp <= 0)
        {
            _animator.SetTrigger("Die");
            gameObject.layer = LayerMask.NameToLayer("DeadObject");
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
        
    }
}
