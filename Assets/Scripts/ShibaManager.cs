using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShibaManager : MonoBehaviour
{
    private NavMeshAgent _agent;

    [SerializeField] private Transform _destination;

    private AudioSource _audioSource;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        _agent.SetDestination(_destination.position);
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_agent.remainingDistance < 0.1f && _agent.remainingDistance != 0)
        {
            _audioSource.Play();
            _animator.SetBool("isRunning", false);
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
            _animator.SetBool("isRunning", true);
        }

        _agent.SetDestination(_destination.position);
        
    }

    public void ToggleNoddingHeadDog()
    {
       if(_animator.GetBool("isNodding"))
        {
            _animator.SetBool("isNodding", false);
        }
        else
        {
            _animator.SetBool("isNodding", true);
        }
    }
}
