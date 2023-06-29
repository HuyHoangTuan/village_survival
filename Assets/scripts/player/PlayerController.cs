using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 0.0f;
    public float maxSpeed = 0.0f;
    public float minSpeed = 0.0f;
    
    private float _speed = 0.0f;
    private Vector2 _input;
    private Animator _animator;
    protected void Start()
    {
        this._speed = this.minSpeed;
        this._animator = GetComponent<Animator>();
    }
    
    protected void Update()
    {
        this._ProcessMoving();
    }

    protected void OnMove(InputValue inputValue)
    {
        this._input = inputValue.Get<Vector2>();
    }

    private void _ProcessMoving()
    {
        bool _IsMoving = false;
        
        if (this._input != Vector2.zero)
        {
            this._speed = this._speed + this.acceleration * Time.deltaTime;
            this._speed = Mathf.Clamp(this._speed, this.minSpeed, this.maxSpeed);
            Vector3 direction = new Vector3(this._input.x, 0.0f, this._input.y);
            
            this._Rotate(direction);
            
            Vector3 delta = direction * this._speed * Time.deltaTime;
            
            if (delta != Vector3.zero)
            {
                this._Move(delta);
                _IsMoving = true;
            }
        }
        else
        {
            this._speed = this.minSpeed;
        }
        
        this._animator.SetFloat("Speed_f", (this._speed - this.minSpeed) / (this.maxSpeed - this.minSpeed));
        this._animator.SetBool("Moving", _IsMoving);
        
    }
    
    private void _Rotate(Vector3 direction)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f );
    }
    private void _Move(Vector3 delta)
    {
        transform.Translate(delta, Space.World);
    }
    
}
