using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    private CharacterController controller;
    private Animator anim;

    [Header("Config Player")]
    [SerializeField] private float speed = 3.0f;
    private Vector3 direction;
    private bool isRun;
    #endregion
    
    #region Mono
    private void Awake() {
        this.controller = GetComponent<CharacterController>();
        this.anim = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        Movement();
    }
    #endregion

    #region Movement
    private void Movement(){
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        this.direction = new Vector3(moveH, 0.0f, moveV).normalized;

        if (this.direction.magnitude > 0.1)
        {
            float targetAngle = Mathf.Atan2(this.direction.x, this.direction.z) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0.0f, targetAngle,0.0f);

            this.isRun = true;
        } else{
            this.isRun = false;
        }

        this.controller.Move(this.direction * this.speed * Time.deltaTime);

        this.anim.SetBool("isRun", this.isRun);
    }
    #endregion
}
