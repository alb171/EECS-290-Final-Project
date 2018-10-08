using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackControls1 : MonoBehaviour {

    [SerializeField] AudioClip soundFireball, soundIce, soundLightning;

    [SerializeField] private GameObject _tornadoPrefab;
    [SerializeField] private GameObject _wavePrefab;
    [SerializeField] private GameObject _lightningPrefab;
    [SerializeField] private GameObject _parent;

    [SerializeField] private RectTransform tB;
    [SerializeField] private RectTransform wB;
    [SerializeField] private RectTransform lB;

    [SerializeField] private Text tBText;
    [SerializeField] private Text wBText;
    [SerializeField] private Text lBText;

    [SerializeField] private float tornadoAttackCooldown;
    [SerializeField] private float waveAttackCooldown;
    [SerializeField] private float lightningAttackCooldown;

    [SerializeField] private bool disableAllSounds;

    private GameObject _tornado;
    private GameObject _wave;
    private GameObject _lightning;

    private float cooldownWave;
    private float cooldownTornado;
    private float cooldownLightning;



    // Use this for initialization
    void Start () {
        tBText.text = "E";
        wBText.text = "Q";
        lBText.text = "F";
    }

    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > cooldownTornado)
        {
            cooldownTornado = Time.time + tornadoAttackCooldown;
            _tornado = Instantiate(_tornadoPrefab) as GameObject;
            _tornado.transform.position = transform.TransformPoint(Vector3.forward);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundFireball);
            }

            if (_parent.name == "cloud")
            {
                    if(UnityEngine.Random.Range(-10.0f, 10.0f) > 0)
                {
                    _tornado.GetComponent<Rigidbody2D>().velocity = (_tornado.transform.up * 10);
                }
                else
                {
                    _tornado.GetComponent<Rigidbody2D>().velocity = (_tornado.transform.up * 10);
                }
                _tornado.transform.eulerAngles = new Vector3(
                    _tornado.transform.eulerAngles.x,
                    _tornado.transform.eulerAngles.y,
                    _tornado.transform.eulerAngles.z);
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            else
            {
                if (UnityEngine.Random.Range(-10.0f, 10.0f) > 0)
                {
                    _tornado.GetComponent<Rigidbody2D>().velocity = (_tornado.transform.up * -10);
                }
                else
                {
                    _tornado.GetComponent<Rigidbody2D>().velocity = (_tornado.transform.up * -10);
                }
                _tornado.transform.eulerAngles = new Vector3(
                    _tornado.transform.eulerAngles.x + 180,
                    _tornado.transform.eulerAngles.y,
                    _tornado.transform.eulerAngles.z);

            }

            StartCoroutine(CooldownTimer(tornadoAttackCooldown, tBText, "E"));
        }
        else if(Time.time < cooldownTornado)
        {
            tB.sizeDelta = new Vector2(
                    30,
                    tB.sizeDelta.y);  
        }
        else
        {
            tB.sizeDelta = new Vector2(
        -160,
        tB.sizeDelta.y);
        }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time > cooldownWave)
        {
            cooldownWave = Time.time + waveAttackCooldown;

            _wave = Instantiate(_wavePrefab) as GameObject;
            _wave.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundIce);
            }

            if (_parent.name == "cloud")
            {
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * 7);
                _wave.transform.eulerAngles = new Vector3(
                    _wave.transform.eulerAngles.x,
                    _wave.transform.eulerAngles.y,
                    _wave.transform.eulerAngles.z);
                var main = _wave.GetComponent<ParticleSystem>().main;
                main.gravityModifier = 2;
            }
            else
            {
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * -7);
                _wave.transform.eulerAngles = new Vector3(
                    _wave.transform.eulerAngles.x + 180,
                    _wave.transform.eulerAngles.y + 180,
                    _wave.transform.eulerAngles.z);

            }

            StartCoroutine(CooldownTimer(waveAttackCooldown, wBText, "Q"));

        }
        else if (Time.time < cooldownWave)
        {
            wB.sizeDelta = new Vector2(
                    30,
                    wB.sizeDelta.y);
        }
        else
        {
            wB.sizeDelta = new Vector2(
        -160,
        wB.sizeDelta.y);
        }

        if (Input.GetKeyDown(KeyCode.F) && Time.time > cooldownLightning)
        {
            cooldownLightning = Time.time + lightningAttackCooldown;

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

            StartCoroutine(CooldownTimer(lightningAttackCooldown, lBText, "F"));

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

    private IEnumerator CooldownTimer(float timeCooldown, Text text, String button)
    {
        text.text = Mathf.Ceil(timeCooldown).ToString();
        yield return new WaitForSeconds(timeCooldown - Mathf.Floor(timeCooldown));
        text.text = Mathf.Floor(timeCooldown).ToString();
        for (int i = 1; i <= Mathf.Floor(timeCooldown); i++)
        {
            yield return new WaitForSeconds(1f);
            text.text = (Mathf.Floor(timeCooldown) - i).ToString();
        }
        text.text = button;
    }
}
