using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int Hp;
    Animator _animator;
    public void TakeDamage(int damage)
    {
        Hp = Mathf.Max(0, Hp -  damage);
        if(Hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
        _animator.SetTrigger("Hit");
    }
    private void OnEnable()
    {
        Hp = 10;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
