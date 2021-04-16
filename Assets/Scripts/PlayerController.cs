using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private CharacterController controller;
    private Animator anim;

    [Header("Config Player")]
    [SerializeField] [Range(1.0f, 8.0f)] private float speed = 3.0f;
    private Vector3 direction;
    private bool isRun;

    [Header("Attack Configs")]
    [SerializeField] private ParticleSystem fxAttack;
    [SerializeField] private Transform hitBox;
    [SerializeField] [Range(0.2f, 1.0f)] private float hitRange;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private int amountDamage;
    private bool isAtk = false;

    #endregion

    #region Mono
    private void Awake()
    {
        this.controller = GetComponent<CharacterController>();
        this.anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Movement();
        Attack();
    }
    #endregion

    #region Movement
    private void Movement()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        this.direction = new Vector3(moveH, 0.0f, moveV).normalized;

        if (this.direction.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(this.direction.x, this.direction.z) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);

            this.isRun = true;
        }
        else
        {
            this.isRun = false;
        }

        this.controller.Move(this.direction * this.speed * Time.deltaTime);

        this.anim.SetBool("isRun", this.isRun);
    }
    #endregion

    #region Attack
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !isAtk)
        {
            isAtk = true;
            anim.SetTrigger("Attack");
            fxAttack.Emit(1);

            Collider[] hitInfo = Physics.OverlapSphere(this.hitBox.position, this.hitRange, this.hitMask);
            foreach (Collider c in hitInfo)
            {
                c.gameObject.SendMessage("GetHit", this.amountDamage, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    private void AttackIsDone()
    {
        isAtk = false;
    }
    #endregion

    #region OnDrawGizmosSelected
    void OnDrawGizmosSelected()
    {
        if (this.hitBox != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(this.hitBox.position, this.hitRange);
        }

    }
    #endregion
}
