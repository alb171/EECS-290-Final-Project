using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackHoleControls : MonoBehaviour
{
    [SerializeField] private Main mainCharacter;

    [SerializeField] private GameObject _tornadoPrefab;         // Black Hole Attack
    [SerializeField] private AudioClip _tornadoPrefabSound;

    [SerializeField] private GameObject _wavePrefab;            // Random Attack 1
    [SerializeField] private AudioClip _wavePrefabSound;

    [SerializeField] private GameObject _randPrefab1;           // Random Attack 2
    [SerializeField] private AudioClip _randPrefab1Sound;

    [SerializeField] private GameObject _randPrefab2;           // Random Attack 3
    [SerializeField] private AudioClip _randPrefab2Sound;

    [SerializeField] private GameObject _randPrefab3;           // Random Attack 4
    [SerializeField] private AudioClip _randPrefab3Sound;

    [SerializeField] private GameObject _lightningPrefab;
    [SerializeField] private GameObject _parent;

    [SerializeField] private RectTransform tB;
    [SerializeField] private RectTransform wB;
    [SerializeField] private RectTransform lB;

    [SerializeField] private Text tBText;
    [SerializeField] private Text wBText;
    [SerializeField] private Text lBText;

    [SerializeField] private float eAttackCooldown;
    [SerializeField] private float qAttackCooldown;
    [SerializeField] private float fAttackCooldown;

    [SerializeField] private float maxDistancePulled;
    [SerializeField] private float maxDistancePulledMainCharacter;

    [SerializeField] private bool disableAllSounds;

    private GameObject _tornado;
    private GameObject _wave;
    private GameObject _lightning;
    private GameObject _rand1;
    private GameObject _rand2;
    private GameObject _rand3;


    private float cooldownWave;
    private float cooldownTornado;
    private float cooldownLightning;



    // Use this for initialization
    void Start()
    {
        tBText.text = "E";
        wBText.text = "Q";
        lBText.text = "F";
    }

    private void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && Time.time > cooldownTornado)
        {
            cooldownTornado = Time.time + eAttackCooldown;
            _tornado = Instantiate(_tornadoPrefab) as GameObject;
            _tornado.transform.position = transform.TransformPoint(Vector3.forward);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(_tornadoPrefabSound);
            }

            if (_parent.name == "Bottom Black Hole")
            {

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    float distance = Mathf.Sqrt((enemy.transform.position.x - this.transform.position.x) * (enemy.transform.position.x - this.transform.position.x)
                        + (enemy.transform.position.y - this.transform.position.y) * (enemy.transform.position.y - this.transform.position.y));
                    if (distance <= maxDistancePulled)
                    {
                        //StartCoroutine(blackHoleAttack2(enemy));
                        //enemy.GetComponent<FollowTarget>().enabled = true;
                        enemy.GetComponent<FollowTarget>().SetTarget(transform, true);
                        StartCoroutine(ResetTarget(enemy.GetComponent<FollowTarget>()));

                    }
                }
                float distanceMain = Mathf.Sqrt((mainCharacter.transform.position.x - this.transform.position.x) * (mainCharacter.transform.position.x - this.transform.position.x)
                        + (mainCharacter.transform.position.y - this.transform.position.y) * (mainCharacter.transform.position.y - this.transform.position.y));
                if (distanceMain <= maxDistancePulledMainCharacter)
                {
                    mainCharacter.SetTargetBlackHole(_parent.transform);

                }

            }
            else
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    float distance = Mathf.Sqrt((enemy.transform.position.x - this.transform.position.x) * (enemy.transform.position.x - this.transform.position.x)
                        + (enemy.transform.position.y - this.transform.position.y) * (enemy.transform.position.y - this.transform.position.y));
                    if (distance <= maxDistancePulled)
                    {
                        //StartCoroutine(blackHoleAttack2(enemy));
                        //enemy.GetComponent<FollowTarget>().enabled = true;
                        enemy.GetComponent<FollowTarget>().SetTarget(transform, true);
                        StartCoroutine(ResetTarget(enemy.GetComponent<FollowTarget>()));
                    }
                }
                float distanceMain = Mathf.Sqrt((mainCharacter.transform.position.x - this.transform.position.x) * (mainCharacter.transform.position.x - this.transform.position.x)
                        + (mainCharacter.transform.position.y - this.transform.position.y) * (mainCharacter.transform.position.y - this.transform.position.y));
                if (distanceMain <= maxDistancePulledMainCharacter)
                {
                    mainCharacter.SetTargetBlackHole(_parent.transform);

                }
                _tornado.transform.eulerAngles = new Vector3(
                 _tornado.transform.eulerAngles.x + 180,
                _tornado.transform.eulerAngles.y + 180,
                _tornado.transform.eulerAngles.z);
            }

            StartCoroutine(CooldownTimer(eAttackCooldown, tBText, "E"));
        }
        else if (Time.time < cooldownTornado)
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
            cooldownWave = Time.time + qAttackCooldown;
            var rand = UnityEngine.Random.Range(-10.0f, 10.0f);
            if (rand < -5)
            {
                _wave = Instantiate(_wavePrefab) as GameObject;
                _wave.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * 10);
                if (!disableAllSounds)
                {
                    SoundManager.instance.Play(_wavePrefabSound);
                }
            }
            else if ( rand > -5 && rand < 0)
            {
                _wave = Instantiate(_randPrefab1) as GameObject;
                _wave.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * 10);
                if (!disableAllSounds)
                {
                    SoundManager.instance.Play(_randPrefab1Sound);
                }
            }
            else if (rand > 0 && rand < 5)
            {
                _wave = Instantiate(_randPrefab2) as GameObject;
                _wave.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * 10);
                if (!disableAllSounds)
                {
                    SoundManager.instance.Play(_randPrefab2Sound);
                }
            }
            else
            {
                _wave = Instantiate(_randPrefab3) as GameObject;
                _wave.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * 10);
                if (!disableAllSounds)
                {
                    SoundManager.instance.Play(_randPrefab3Sound);
                }
            }


            if (_parent.name == "Bottom Black Hole")
            {
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * 14);
                _wave.transform.eulerAngles = new Vector3(
                    _wave.transform.eulerAngles.x,
                    _wave.transform.eulerAngles.y,
                    _wave.transform.eulerAngles.z);
                var main = _wave.GetComponent<ParticleSystem>().main;
                main.gravityModifier = 2;
            }
            else
            {
                _wave.GetComponent<Rigidbody2D>().velocity = (_wave.transform.up * -14);
                _wave.transform.eulerAngles = new Vector3(
                    _wave.transform.eulerAngles.x + 180,
                    _wave.transform.eulerAngles.y + 180,
                    _wave.transform.eulerAngles.z);

            }

            StartCoroutine(CooldownTimer(qAttackCooldown, wBText, "Q"));

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

        /**if (Input.GetKeyDown(KeyCode.F) && Time.time > cooldownLightning)
        {
            cooldownLightning = Time.time + fAttackCooldown;

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

            StartCoroutine(CooldownTimer(fAttackCooldown, lBText, "F"));

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

    **/
    }

    private IEnumerator ResetTarget(FollowTarget enemy)
    {
        yield return new WaitForSeconds(3f);
        enemy.GetComponent<FollowTarget>().SetTarget(mainCharacter.transform, false);
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
