using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    // 하위 변수들은 플레이어 세팅을 위한 컴포넌트 가져오는 변수들
    [SerializeField]
    private VariableJoystick variableJoystick;
    private Animator anim;
    private Rigidbody2D rb;
    private WeaponsAndBuffs wab;
    private Vector3 ActiveDir;

    // 게임 내에서 필요한 플레이어 세팅값
    [SerializeField]
    private float max_health;
    private float attack_speed;
    public float player_move_speed;
    private float magnetic_radius;
    private float critical_rate;
    private float critical_damage;
    private float weapon_move_speed_rate;
    private float weapon_damage;
    private float invincibility_time;

    // 기본 값
    private float current_health;
    private int current_exp;
    private int max_exp;
    private bool gettingDamage;
    private bool facingRight;
    // private bool playerAlive;
    private bool isDashing;
    private bool dashAvailable;
    private float dashingPower = 9f;
    private float dashingTime = 0.25f;
    private float dashingCooldown = 2.5f;

    public void setDashAvailable(bool value) {
        dashAvailable = value;
    }

    
    private void Awake() {
        Instance = this;
    }
    private void OnDestroy() {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        invincibility_time = 0.2f;
        wab = GetComponent<WeaponsAndBuffs>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gettingDamage = false;
        facingRight = true;
        // playerAlive = true;
        isDashing = false;
        dashAvailable = true;
        current_health = max_health;
        wab.setAttackSpeed(attack_speed);
        wab.setCriticalDamage(critical_damage);
        wab.setCriticalRate(critical_rate);
        wab.setMagneticRadius(magnetic_radius);
        wab.setMaxHealth(max_health);
        wab.setPlayerMoveSpeed(player_move_speed);
        wab.setWeaponDamage(weapon_damage);
        wab.setWeaponMoveSpeedRate(weapon_move_speed_rate);
        wab.setInvincibilityTime(invincibility_time);
        ActiveDir = new Vector3(0f, 1f, 0f);
    }

    private void Update() {
        anim.SetBool("dashing", isDashing);
        MovePlayer();
    }

    public Vector3 getActiveDir() {return ActiveDir;}
    public int getCurrentExp() {return current_exp;}
    public int getMaxExp() {return max_exp;}
    public float getCurrentHP() {return current_health;}
    public float getMaxHP() {return max_health;}
    public float calculateWeaponDamage() {
        if (Random.Range(0, critical_rate) <= critical_rate) {
            return weapon_damage * critical_damage;
        } else {return weapon_damage;}
    }
    public Transform getPlayerTransform() {
        return transform;
    }

    private void MovePlayer() {
        if (!isDashing) {
            float toX = variableJoystick.Horizontal;
            float toY = variableJoystick.Vertical;
            Vector3 direction = new Vector3(toX, toY, 0f);
            direction.Normalize();
            anim.SetBool("IsRunning", direction != Vector3.zero);
            if (toX < 0 && facingRight) {
                FlipPlayer();
            } else if (toX > 0 && !facingRight) {
                FlipPlayer();
            }
            if (direction != Vector3.zero) {ActiveDir = direction;}
            if ((transform.position.x <= -11.2f && direction.x < 0) || (transform.position.x >= 11.7f && direction.x > 0)) {
                direction.x = 0f;
            }
            if ((transform.position.y <= -10.4f && direction.y < 0) || (transform.position.y >= 9.9f && direction.y > 0)) {
                direction.y = 0f;
            }
            transform.position += direction * player_move_speed * Time.deltaTime;
        }
    }

    void FlipPlayer() {
        facingRight = !facingRight;
        Vector3 tempLocalScale = transform.localScale;
        tempLocalScale.x *= -1; 
        transform.localScale = tempLocalScale;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy_Bat") {
            if (!gettingDamage) {
                current_health -= other.GetComponent<EnemyBat>().damage;
                WhenPlayerDamaged();
            }
        }
        if (other.gameObject.tag == "Enemy_Spider") {
            if (!gettingDamage) {
                current_health -= other.GetComponent<EnemySpider>().damage;
                WhenPlayerDamaged();
            }
        }
        if (other.gameObject.tag == "Enemy_Slime") {
            if (!gettingDamage) {
                current_health -= other.GetComponent<EnemySlime>().damage;
                WhenPlayerDamaged();
            }
        }
        if (other.gameObject.tag == "Enemy_OneEye") {
            if (!gettingDamage) {
                current_health -= other.GetComponent<EnemyOneEye>().damage;
                WhenPlayerDamaged();
            }
        }
        if (other.gameObject.tag == "EXP") {
            current_exp += other.GetComponent<EXP>().exp_amount;
            GameManager.Instance.UpdateEXP();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Gold") {
            GameManager.Instance.AddGold();
        }
    }

    private void WhenPlayerDamaged() {
        GameManager.Instance.UpdateHP();
        if (current_health <= 0) {
            PlayerDie();
        }
        Debug.Log(current_health);
        StartCoroutine("OnDamage");
    }

    IEnumerator OnDamage() {
        gettingDamage = true;
        yield return new WaitForSeconds(invincibility_time);
        gettingDamage = false;
    }

    public void PlayerDie() {
        // playerAlive = false;
        GameManager.Instance.GameOver();
    }

    // private void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.tag == "Enemy_Bat") {
    //         Debug.Log("Bat");
    //         if (!gettingDamage) {
    //             current_health -= other.GetComponent<EnemyBat>().damage;
    //             Debug.Log("Damged");
    //             WhenPlayerDamaged();
    //         }
    //     }
    //     if (other.gameObject.tag == "Enemy_Spider") {
    //         if (!gettingDamage) {
    //             current_health -= other.GetComponent<EnemySpider>().damage;
    //             WhenPlayerDamaged();
    //         }
    //     }
    //     if (other.gameObject.tag == "Enemy_Slime") {
    //         if (!gettingDamage) {
    //             current_health -= other.GetComponent<EnemySlime>().damage;
    //             WhenPlayerDamaged();
    //         }
    //     }
    //     if (other.gameObject.tag == "Enemy_OneEye") {
    //         if (!gettingDamage) {
    //             current_health -= other.GetComponent<EnemyOneEye>().damage;
    //             WhenPlayerDamaged();
    //         }
    //     }
    //     if (other.gameObject.tag == "EXP") {
    //         current_exp += other.GetComponent<EXP>().exp_amount;
    //         GameManager.Instance.UpdateEXP();
    //         Destroy(other.gameObject);
    //     }
    //     if (other.gameObject.tag == "Gold") {
    //         GameManager.Instance.AddGold();
    //     }
    // }

    public void DashClicked() {
        if (dashAvailable) {
            StartCoroutine("Dash");
        }
    }
    IEnumerator Dash() {
        dashAvailable = false;
        isDashing = true;
        anim.SetTrigger("dash");
        float startTime = Time.time;
        while (Time.time < startTime + dashingTime) {
            transform.position += ActiveDir * dashingPower * Time.deltaTime;
            yield return null;
        }
        isDashing = false;
        startTime = Time.time;
        while (Time.time - startTime < dashingCooldown) {
            GameManager.Instance.UpdateDashCool(Time.time - startTime);
            yield return null;
        }
        dashAvailable = true;
    }
}
