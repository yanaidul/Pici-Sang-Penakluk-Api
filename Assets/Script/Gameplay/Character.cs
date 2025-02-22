using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 v3Movement;
    [SerializeField] FixedJoystick _fixedJoystick;

    [SerializeField] Transform transModel;
    [SerializeField] Transform transItemPos;

    [Space]
    [SerializeField] Animator animModel;

    [Space]
    [SerializeField] AudioSource sfxStep;

    [Space]
    [SerializeField] string strAnimBoolWalk = "Bool Walk";

    [Space]
    [SerializeField] float fltSpeed = 1f;
    [SerializeField] float fltRotateSpeed = 10f;
    [SerializeField] float fltRotateThreshold = 0.1f;
    private float fltMoveH;
    private float fltMoveV;

    public bool IsAparTaken {  get; private set; }
    public bool BoolItem { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 3)
        {
            _fixedJoystick.gameObject.SetActive(false);
            IsAparTaken = false;
        }
    }

    private void Update()
    {
        ProcessInput();
        Move();
        Rotate();
    }

    public void MoveH(float _fltValue)
    {
        fltMoveH = _fltValue;
    }

    public void MoveV(float _fltValue)
    {
        fltMoveV = _fltValue;
    }

    private void ProcessInput()
    {
        //fltMoveH = Input.GetAxis("Horizontal");
        //fltMoveV = Input.GetAxis("Vertical");

        v3Movement = new Vector3(-_fixedJoystick.Vertical, 0f, _fixedJoystick.Horizontal);
    }

    private void Move()
    {
        Vector3 moveVelocity = v3Movement * fltSpeed;
        moveVelocity.y = rb.velocity.y;

        rb.velocity = moveVelocity;

        animModel.SetBool(strAnimBoolWalk, moveVelocity != Vector3.zero);
        sfxStep.mute = moveVelocity == Vector3.zero;
    }

    private void Rotate()
    {
        if (v3Movement.magnitude > fltRotateThreshold)
        {
            Quaternion targetRotation = Quaternion.LookRotation(v3Movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, fltRotateSpeed * Time.deltaTime);
        }
    }

    public void SetItem(GameObject _goItem)
    {
        _fixedJoystick.gameObject.SetActive(true);
        _goItem.transform.SetParent(transItemPos);
        _goItem.transform.localPosition = Vector3.zero;
        _goItem.GetComponent<Animator>().enabled = false;
        IsAparTaken = true;
        BoolItem = true;
    }
}
