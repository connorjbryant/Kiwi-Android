using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player_controller
{

    public class KiwiMove : MonoBehaviour
    {
        public float Speed;
        public Animator animator;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            if (VirtualInputManager.Instance.MoveRight && VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
                return;
            }

            if (!VirtualInputManager.Instance.MoveRight && !VirtualInputManager.Instance.MoveLeft)
            {
                animator.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
            }

            if (VirtualInputManager.Instance.MoveRight)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
            }

            if (VirtualInputManager.Instance.MoveLeft)
            {
                this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
                this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                animator.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
            }

            if (VirtualInputManager.Instance.Jump)
            {
                this.gameObject.transform.Translate(Speed * Vector3.up * Time.deltaTime);
                animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
            }
        }
    }
}
