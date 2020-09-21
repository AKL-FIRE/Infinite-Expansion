﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting.APIUpdating;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    public int hp = 150;
    private int totalHp;
    private Slider hpSlider;
    private Transform[] positions;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        positions = Waypoints.positions;
        hpSlider = GetComponentInChildren<Slider>();
        totalHp = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (index > positions.Length - 1)
        {
            return;
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //TODO 调用前进动画
        transform.forward = positions[index].position - transform.position;
        if(Time.deltaTime * speed >= Vector3.Distance (transform.position, positions[index].position)){
            index++;
        }
        else if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination()
    {
        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;    
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0)
        {
            return;
        }
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;
        if (hp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        //TODO 调用死亡动画
        Destroy(this.gameObject);
    }
}
