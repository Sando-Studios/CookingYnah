using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoatMajorEnemy : MajorEnemy
{
    [Header("Charge")]
    [SerializeField] private float maxChargeDistance = 7.0f;
    private Vector3 chargeEndPoint;
    private bool isCharging = false;
    [SerializeField] private GameObject chargeIndicator;
    [SerializeField] private float windUpTime = 2.0f;

    // Start is called before the first frame update
    protected void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);

        if (other.CompareTag("Player") && !other.isTrigger && GetComponent<CapsuleCollider>().isTrigger)
        {
            DamageHandler.ApplyDamage(other.GetComponent<Player>(), bossDataInstance.BasicAttackDamage);
        }
    }

    public override void ExecuteBasicAttack()
    {
        if (!isCharging)
        {
            Vector3 direction = targetUnit.transform.position - transform.position;
            direction.y = 0f;
            direction.Normalize();

            Vector3 destination = transform.position + direction * (bossDataInstance.AttackRange * 2);

            NavMesh.SamplePosition(destination, out NavMeshHit point, 3.0f, NavMesh.AllAreas);

            chargeEndPoint = point.position;
            agent.enabled = false;
            GetComponent<CapsuleCollider>().isTrigger = true;

            StartCoroutine(WindUp());

            PlayAudioClip(GetAudioClipName("Stampede"));
            AddToAttackCount(1);
        }
    }

    public override void ExecuteSpecialAttack()
    {
        PlayAudioClip(GetAudioClipName("Roar"));
        AttackTimer(bossDataInstance.SpecialAttackSpeed);
    }


    public void StartWave()
    {
        GetComponent<AudioSource>().Stop();
        PlayAudioClip(GetAudioClipName("Stomp"));
        GetComponent<AudioSource>().Play();
        GroundWaveAbility groundWave = GetComponent<GroundWaveAbility>();
        groundWave.SpawnRocks(3, bossDataInstance.SpecialAttackDamage);
    }

    protected override void Update()
    {

        if (isCharging)
        {
            Vector3 direction = chargeEndPoint - transform.position;
            direction.Normalize();
            Vector3 movement = direction * bossDataInstance.RunSpeed;

            spriteTransform.rotation = Quaternion.Euler(new Vector3(0f, direction.x >= 0.08 ? -180f : 0f, 0f));

            float distanceToTarget = Vector3.Distance(transform.position, chargeEndPoint);

            if (distanceToTarget <= 0.1f)
            {
                isCharging = false;
                GetComponent<CapsuleCollider>().isTrigger = false;
                agent.enabled = true;
                AttackTimer(bossDataInstance.BasicAttackSpeed);
                SetIsAttackDone(true);
            }
            else
            {
                transform.position += movement * Time.deltaTime;
            }
        }
        base.Update();
    }

    private IEnumerator WindUp()
    {
        chargeIndicator.SetActive(true);
        yield return new WaitForSeconds(windUpTime);
        chargeIndicator.SetActive(false);
        isCharging = true;
    }
}
