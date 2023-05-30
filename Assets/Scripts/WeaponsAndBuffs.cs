using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsAndBuffs : MonoBehaviour
{
    // 무기 사용을 위해 필요한 변수들
    private Vector3 direction;
    [SerializeField]
    private VariableJoystick variableJoystick;

    // 무기 종류들
    // 기본 공격 (Fireball)
    [SerializeField]
    private GameObject[] basic_attack;
    private int[] basic_attack_damages;
    public int basic_attack_level;

    // 파이어 배리어 (일정 반경 안의 적 지속적 데미지)
    [SerializeField]
    private GameObject[] fire_barrier;
    public int fire_barrier_level;

    // 블랙홀 (발사 후 처음 맞는 상대 주위에 블랙홀 생성, 주위의 상대편 이동 불가 상태 부여)
    [SerializeField]
    private GameObject[] black_hole;
    public int black_hole_level;

    // 천둥번개 (플레이어 주위에 일정 간격동안 번개 내려침)
    [SerializeField]
    private GameObject[] thunder;
    public int thunder_level;

    // 뿔로 검기 발사 (초승달 모양의 검기를 방출하여 일정 방향으로 움직임)
    [SerializeField]
    private GameObject[] horn_wave;
    public int horn_wave_level;

    // 버프 종류들
    // 플레이어 버프
    private int attack_speed_rate_buff_level;
    private int max_health_buff_level;
    private int player_move_speed_buff_level;
    private int magnetic_raius_buff_level;
    private int critical_rate_buff_level;
    private int critical_damage_buff_level;
    private int weapon_move_speed_rate_buff_level;
    private int weapon_damage_buff_level;
    private int invincibility_time_buff_level;
    

    // 능력치들
    // 플레이어 능력치
    private float attack_speed;
    private float max_health;
    private float player_move_speed;
    private float magnetic_radius;
    private float critical_rate;
    private float critical_damage;
    private float weapon_move_speed_rate;
    private float weapon_damage;
    private float invincibility_time;

    public void setAttackSpeed (float speed) {
        attack_speed = speed;
    }
    public void setMaxHealth(float hp) {
        max_health = hp;
    }
    public void setPlayerMoveSpeed(float speed) {
        player_move_speed = speed;
    }
    public void setMagneticRadius(float radius) {
        magnetic_radius = radius;
    }
    public void setCriticalRate(float rate) {
        critical_rate = rate;
    }
    public void setCriticalDamage(float damage) {
        critical_damage = damage;
    }
    public void setWeaponMoveSpeedRate(float rate) {
        weapon_move_speed_rate = rate;
    }
    public void setWeaponDamage(float rate) {
        weapon_damage = rate;
    }
    public void setInvincibilityTime(float time) {
        invincibility_time = time;
    }
    
    public void levelUp() {
        
    }
    
    private void Start() {
        direction = new Vector3(0f, 1f, 0f);

    }

    private void Update() {
        direction = Player.Instance.getActiveDir();
    }

    // private void Start() {
    //     StartCoroutine("StartBasicAttack");
    // }

    // IEnumerator StartBasicAttack() {
        // while (GameManager.Instance.GetPlaying()) {
            // float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            // GameObject attack = Instantiate(basicAttack, transform.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));

            // GameObject attack = Instantiate(basicAttack, transform.position, Quaternion.identity);
            // BasicAttack b_attack = attack.GetComponent<BasicAttack>();
            // b_attack.SetDirection(direction);
            //yield return new WaitForSeconds(basicAttackSpeed);
            // yield return new WaitForSeconds(attack);
    //     }
    // }
}
