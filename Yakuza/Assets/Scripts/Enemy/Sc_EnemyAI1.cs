using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_EnemyAI1 : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;

    [SerializeField] private float chasingRange;
    [SerializeField] private float shootingRange;

    [SerializeField] private GameObject playerTransform;
    [SerializeField] private GameObject aimGunEndPoint;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Sc_Cover[] availableCovers;

    private Rigidbody2D rb2d;
    private Material _material;
    private Transform bestCoverPoint;

    private NavMeshAgent agent;

    public GameObject bloodSplat1;
    public GameObject bloodSplat2;
    public GameObject bloodSplat3;
    public GameObject bloodSplat4;
    public GameObject bloodSplat5;
    private Sc_DialogueTrigger cutsceneDialogue;
    private Sc_Node topNode;
    private float velocityTimer;
    private float _currentHealth;

    private Vector3 targetPosition;
    private Vector3 aimDirection;

    public event EventHandler<ShootingEventArgs> Shooting;

    public class ShootingEventArgs : EventArgs
    {
        public Vector3 gunPointPosition;
        public Vector3 shootPosition;
    }

    public float currentHealth { get { return _currentHealth; } set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); } }

    void Awake()
    {
        cutsceneDialogue = GameObject.Find("cutscene2").GetComponent<Sc_DialogueTrigger>();
        agent = this.GetComponent<NavMeshAgent>();
        _material = GetComponent<SpriteRenderer>().material;
        rb2d = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        _currentHealth = startingHealth;
        ConstructBehaviorTree();
    }

    private void ConstructBehaviorTree()
    {
        Sc_IsCoverAvailableNode coverAvailableNode = new Sc_IsCoverAvailableNode(availableCovers, playerTransform.transform, this);
        Sc_GoToCoverNode goToCoverNode = new Sc_GoToCoverNode(agent, this);
        Sc_HealthNode healthNode = new Sc_HealthNode(this, lowHealthThreshold);
        Sc_IsCoveredNode isCoveredNode = new Sc_IsCoveredNode(playerTransform.transform, transform);
        Sc_ChaseNode chaseNode = new Sc_ChaseNode(playerTransform.transform, agent, this);
        Sc_RangeNode chasingRangeNode = new Sc_RangeNode(chasingRange, playerTransform.transform, transform);
        Sc_RangeNode shootingRangeNode = new Sc_RangeNode(shootingRange, playerTransform.transform, transform);
        Sc_ShootNode shootNode = new Sc_ShootNode(agent, this, playerTransform.transform);

        Sc_Sequence chaseSequence = new Sc_Sequence(new List<Sc_Node> { chasingRangeNode, chaseNode });
        Sc_Sequence shootSequence = new Sc_Sequence(new List<Sc_Node> { shootingRangeNode, shootNode });

        Sc_Sequence goToCoverSequence = new Sc_Sequence(new List<Sc_Node> { coverAvailableNode, goToCoverNode });
        Sc_Selector findCoverSelector = new Sc_Selector(new List<Sc_Node> { goToCoverSequence, chaseSequence });
        Sc_Selector tryToTakeCoverSelector = new Sc_Selector(new List<Sc_Node> { isCoveredNode, findCoverSelector });
        Sc_Sequence mainCoverSequence = new Sc_Sequence(new List<Sc_Node> { healthNode, tryToTakeCoverSelector });

        topNode = new Sc_Selector(new List<Sc_Node> { mainCoverSequence, shootSequence, chaseSequence });
    }

    void Update()
    {
        if (!InCutscene())
        {
            topNode.Evaluate();
            if (topNode.nodeState == NodeState.FAILURE)
            {
                Debug.Log("Bruh");
            }

            currentHealth += Time.deltaTime * healthRestoreRate;
            velocityTimer -= Time.deltaTime;
            if (velocityTimer <= 0)
                rb2d.velocity = Vector2.zero;
            ;
            CheckIfDead();
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void SetColor(Color color)
    {
        _material.color = color;
    }

    public void SetBestCoverPoint(Transform bestCoverPoint)
    {
        this.bestCoverPoint = bestCoverPoint;
    }

    public Transform GetBestCoverPoint()
    {
        return bestCoverPoint;
    }

    public void EnemyHandleShooting()
    {
        if (Sc_ShootNode.canShoot)
        {
            aimDirection = (playerTransform.transform.position - transform.position).normalized;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(aimGunEndPoint.transform.position, aimDirection, Vector3.Distance(aimGunEndPoint.transform.position, playerTransform.transform.position), _layerMask);
            if (raycastHit2D.collider == null)
            {
                targetPosition = playerTransform.transform.position;
            }
            else
            {
                targetPosition = raycastHit2D.point;
            }
            if (Shooting != null)
                Shooting(this,
                    new ShootingEventArgs
                    {
                        gunPointPosition = aimGunEndPoint.transform.position,
                        shootPosition = targetPosition,
                    });
        }
    }

    public void GetDamaged(int damage)
    {
        spawnRandomBloodsplat();
        Knockback();
        currentHealth -= damage;
        //Anim
        //Knockback
        //Possible sound
    }

    public void spawnRandomBloodsplat() {
        switch (UnityEngine.Random.Range(0,4)) {
            case 0:  Instantiate(bloodSplat1, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
                break;
            case 1:
                Instantiate(bloodSplat2, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
                break;
            case 2:
                Instantiate(bloodSplat3, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
                break;
            case 3:
                Instantiate(bloodSplat4, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
                break;
            case 4:
                Instantiate(bloodSplat5, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.identity);
                break;
        }
    }

    public void Knockback()
    {
        Vector2 direction = (playerTransform.transform.position - transform.position).normalized;
        int knockbackDistance = 100;
        rb2d.AddForce(-direction * knockbackDistance);
        velocityTimer = 0.25f;
    }

    public void CheckIfDead()
    {
        if (currentHealth <= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    private bool InCutscene()
    {
        if (cutsceneDialogue != null)
            return cutsceneDialogue.DialogueActive();
        else
            return false;
    }
}
