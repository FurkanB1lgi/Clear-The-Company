using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float shootRange;
    [SerializeField] private LayerMask shootLayer;
    private Transform aimTransform;
    private Attack attack;
    private bool isReloaded = false;

    private bool canMoveRight;

    private void Awake()
    {
        attack = GetComponent<Attack>();
        aimTransform = attack.GetFireTransform;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckCanMoveRight();

        MoveToWards();
        EnemyAttack();


    }
    private void Reload()
    {
        attack.GetAmmo = attack.GetClipSize;
        isReloaded = false;
    }
    private void EnemyAttack()
    {
        if (attack.GetAmmo <= 0 && isReloaded == false)
        {
            Invoke(nameof(Reload), 5f);
            isReloaded = true;
        }
        if (attack.GetCurrentFireRate <= 0 && attack.GetAmmo > 0 && Aim())
        {
            attack.Fire();
        }
    }
    private bool Aim()
    {
        if (aimTransform == null)
        {
            aimTransform = attack.GetFireTransform;
        }
        bool hit = Physics.Raycast(aimTransform.position, -transform.forward, shootRange, shootLayer);
        Debug.DrawRay(aimTransform.position, -transform.forward * shootRange, Color.red);
        return hit;
    }
    private void MoveToWards()
    {
        if (Aim() && attack.GetAmmo > 0)
        {
            return;
        }


        if (!canMoveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[1].position);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[1].position.z), speed * Time.deltaTime);
            LookAtTheTarget(movePoints[0].position);
        }
    }
    private void CheckCanMoveRight()
    {
        if (Vector3.Distance(transform.position, new Vector3(movePoints[0].position.x, transform.position.y, movePoints[0].position.z)) <= 0.1f)
        {
            canMoveRight = true;
        }
        else if (Vector3.Distance(transform.position, new Vector3(movePoints[1].position.x, transform.position.y, movePoints[0].position.z)) <= 0.1f)
        {
            canMoveRight = false;

        }

    }
    private void LookAtTheTarget(Vector3 newtarget)
    {
        Vector3 newLookPosition = new Vector3(newtarget.x, transform.position.y, newtarget.z);
        Quaternion targetRotation = Quaternion.LookRotation(newLookPosition - transform.position);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, speed * Time.deltaTime);

    }
}
