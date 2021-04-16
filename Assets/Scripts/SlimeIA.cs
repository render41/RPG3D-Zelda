using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIA : MonoBehaviour
{
    #region Variables
    private Animator anim;
    [SerializeField] private ParticleSystem hitFx;
    [SerializeField] [Range(0, 8)] private int hp;
    private bool isDie = false;

    [SerializeField] private enemyState state;
    public const float idleWaitTime = 3.0f;
    public const float patrolWaitTime = 5.0f;
    #endregion

    #region Mono
    private void Awake()
    {
        this.anim = GetComponent<Animator>();
        this.ChangeState(state);
    }

    private void Update()
    {
        StateManager();
    }
    #endregion

    #region States
    // O que acontece a cada frame quando acessar
    private void StateManager()
    {
        switch (state)
        {
            case enemyState.IDLE:
                break;
            case enemyState.ALERT:
                break;
            case enemyState.EXPLORE:
                break;
            case enemyState.FOLLOW:
                break;
            case enemyState.FURY:
                break;
            case enemyState.PATROL:
                break;
        }
    }

    // Quando acessar o estado
    private void ChangeState(enemyState newState)
    {
        StopAllCoroutines();
        this.state = newState;
        print(newState);
        switch (state)
        {
            case enemyState.IDLE:
                StartCoroutine("IDLE");
                break;
            case enemyState.ALERT:
                break;
            case enemyState.PATROL:
                StartCoroutine("PATROL");
                break;
        }
    }

    IEnumerator IDLE()
    {
        yield return new WaitForSeconds(idleWaitTime);
        if (Randomizer() < 50)
        {
            ChangeState(enemyState.IDLE);
        }
        else
        {
            ChangeState(enemyState.PATROL);
        }
    }

    IEnumerator PATROL()
    {
        yield return new WaitForSeconds(patrolWaitTime);
        ChangeState(enemyState.IDLE);
    }

    private int Randomizer()
    {
        int randomNumber = Random.Range(0, 100);
        return randomNumber;
    }
    #endregion

    #region Get Hit
    private void GetHit(int amount)
    {
        if (isDie == true)
        {
            return;
        }
        this.hp -= amount;
        if (hp > 0)
        {
            this.anim.SetTrigger("GetHit");
            this.hitFx.Emit(50);
        }
        else if (hp <= 0)
        {
            this.hp = 0;
            this.anim.SetTrigger("Die");
            StartCoroutine(IsDied());
        }

    }

    IEnumerator IsDied()
    {
        this.isDie = true;
        yield return new WaitForSeconds(2.0f);
        this.gameObject.SetActive(false);
    }
    #endregion
}
