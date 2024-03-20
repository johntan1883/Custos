using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowTarget : MonoBehaviour
{
    private GameObject target;
    private Transform BlindBoy;
    public float speed;

    private float distance;
    //public bool _isFollowingTarget;

    // Start is called before the first frame update
    void Start()
    {
        BlindBoy = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("FollowObject");

        if (target == null)
        { 
            return;
        }
        //Vector3 direction = target.transform.position - transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90f;

        BlindBoy.position = Vector2.MoveTowards(BlindBoy.position, target.transform.position, speed * Time.deltaTime);
    }
}
