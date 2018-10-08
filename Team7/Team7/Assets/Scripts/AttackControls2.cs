using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControls2 : MonoBehaviour {

    [SerializeField] AudioClip soundFireball, soundIce, soundLightning;

    [SerializeField] private GameObject _fireballPrefab;
    [SerializeField] private GameObject _icePrefab;
    [SerializeField] private GameObject _lightningPrefab;
    [SerializeField] private GameObject _parent;

    [SerializeField] private RectTransform fB;
    [SerializeField] private RectTransform iB;
    [SerializeField] private RectTransform lB;

    [SerializeField] private bool disableAllSounds;

    private GameObject _fireball;
    private GameObject _ice;
    private GameObject _lightning;

    private float cooldownIce;
    private float cooldownFire;
    private float cooldownLightning;



    // Use this for initialization
    void Start () {

    }

    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > cooldownFire)
        {
            cooldownFire = Time.time + 2;
            _fireball = Instantiate(_fireballPrefab) as GameObject;
            _fireball.transform.position = transform.TransformPoint(Vector3.forward);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundFireball);
            }

            if (_parent.name == "cloud")
            {
                    if(Random.Range(-10.0f, 10.0f) > 0)
                {
                    _fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.up * 14);
                }
                else
                {
                    _fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.up * 38);
                }
                _fireball.transform.eulerAngles = new Vector3(
                    _fireball.transform.eulerAngles.x,
                    _fireball.transform.eulerAngles.y,
                    _fireball.transform.eulerAngles.z);
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            else
            {
                if (Random.Range(-10.0f, 10.0f) > 0)
                {
                    _fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.up * -14);
                }
                else
                {
                    _fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.up * -38);
                }
                _fireball.transform.eulerAngles = new Vector3(
                    _fireball.transform.eulerAngles.x + 180,
                    _fireball.transform.eulerAngles.y,
                    _fireball.transform.eulerAngles.z);

            }
            

        }
        else if(Time.time < cooldownFire)
        {
            fB.sizeDelta = new Vector2(
                    30,
                    fB.sizeDelta.y);
        }
        else
        {
            fB.sizeDelta = new Vector2(
        -160,
        fB.sizeDelta.y);
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time > cooldownIce)
        {
            cooldownIce = Time.time + 3;

            _ice = Instantiate(_icePrefab) as GameObject;
            _ice.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundIce);
            }

            if (_parent.name == "cloud")
            {
                _ice.GetComponent<Rigidbody2D>().velocity = (_ice.transform.up * 7);
                _ice.transform.eulerAngles = new Vector3(
                    _ice.transform.eulerAngles.x,
                    _ice.transform.eulerAngles.y,
                    _ice.transform.eulerAngles.z);
            }
            else
            {
                _ice.GetComponent<Rigidbody2D>().velocity = (_ice.transform.up * -7);
                _ice.transform.eulerAngles = new Vector3(
                    _ice.transform.eulerAngles.x,
                    _ice.transform.eulerAngles.y + 180,
                    _ice.transform.eulerAngles.z);

            }

        }
        else if (Time.time < cooldownIce)
        {
            iB.sizeDelta = new Vector2(
                    30,
                    iB.sizeDelta.y);
        }
        else
        {
            iB.sizeDelta = new Vector2(
        -160,
        iB.sizeDelta.y);
        }

        if (Input.GetKeyDown(KeyCode.F) && Time.time > cooldownLightning)
        {
            cooldownLightning = Time.time + 0;

            _lightning = Instantiate(_lightningPrefab) as GameObject;
            _lightning.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundLightning);
            }

            if (_parent.name == "cloud")
            {
                _lightning.GetComponent<Rigidbody2D>().velocity = _lightning.transform.up * 150;
                _lightning.transform.eulerAngles = new Vector3(
                    _lightning.transform.eulerAngles.x,
                    _lightning.transform.eulerAngles.y,
                    _lightning.transform.eulerAngles.z);
            }
            else
            {
                _lightning.GetComponent<Rigidbody2D>().velocity = _lightning.transform.up * -150;
                _lightning.transform.eulerAngles = new Vector3(
                    _lightning.transform.eulerAngles.x,
                    _lightning.transform.eulerAngles.y + 180,
                    _lightning.transform.eulerAngles.z);

            }

        }
        else if (Time.time < cooldownLightning)
        {
            lB.sizeDelta = new Vector2(
                    30,
                    lB.sizeDelta.y);
        }
        else
        {
            lB.sizeDelta = new Vector2(
        -160,
        lB.sizeDelta.y);
        }


    }
}
