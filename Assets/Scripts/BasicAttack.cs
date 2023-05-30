using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public  float moveSpeed;
    public Vector3 dir;
    public void SetDirection(Vector3 dir) {
        this.dir = dir;
        setRotation();
    }

    private void setRotation() {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public float getMoveSpeed() {
        return moveSpeed;
    }

    public void setMoveSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
