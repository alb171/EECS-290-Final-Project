using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunMoonControls : MonoBehaviour {

    [SerializeField] AudioClip soundFireball, soundIce, soundLightning;

    [SerializeField] private GameObject _fireballPrefab;
	[SerializeField] private GameObject _icePrefab;
	[SerializeField] private GameObject _lightningPrefab;
	[SerializeField] private GameObject _parent; 

	[SerializeField] private RectTransform fB;
	[SerializeField] private RectTransform iB;
	[SerializeField] private RectTransform lB;

    [SerializeField] private Text fBText;
    [SerializeField] private Text iBText;
    [SerializeField] private Text lBText;

    [SerializeField] private float fireIceAttackCooldown;
    [SerializeField] private float stormAttackCooldown;
    [SerializeField] private float dualLightningAttackCooldown;

    [SerializeField] private bool disableAllSounds;

    private GameObject _fireball;
	private GameObject _ice;
	private GameObject _lightning;

	private float cooldownIce;
	private float cooldownFire;
	private float cooldownLightning;


	// Use this for initialization
	void Start () {
        fBText.text = "E";
        iBText.text = "Q";
        lBText.text = "F";
    }

	private void FixedUpdate()
	{
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E) && Time.time > cooldownFire)
		{
			cooldownFire = Time.time + fireIceAttackCooldown;
			_fireball = Instantiate(_fireballPrefab) as GameObject;
			_fireball.transform.position = transform.TransformPoint(Vector3.forward);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundFireball);
            }

            if (_parent.name == "moon")
			{
				if(Random.Range(-10.0f, 10.0f) > 0)
				{
					_fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.right * 30);
				}
				else
				{
					_fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.right * 30);
				}
				_fireball.transform.eulerAngles = new Vector3(
					_fireball.transform.eulerAngles.x,
					_fireball.transform.eulerAngles.y,
					_fireball.transform.eulerAngles.z - 90);
				transform.Translate(Vector3.forward * Time.deltaTime);
			}
			else
			{
				if (Random.Range(-10.0f, 10.0f) > 0)
				{
					_fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.right * -30);
				}
				else
				{
					_fireball.GetComponent<Rigidbody2D>().velocity = (_fireball.transform.right * -30);
				}
				_fireball.transform.eulerAngles = new Vector3(
					_fireball.transform.eulerAngles.x,
					_fireball.transform.eulerAngles.y,
					_fireball.transform.eulerAngles.z + 90);

			}

            StartCoroutine(CooldownTimer(fireIceAttackCooldown, fBText, "E"));
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
			cooldownIce = Time.time + stormAttackCooldown;

			_ice = Instantiate(_icePrefab) as GameObject;
			_ice.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundIce);
            }

            if (_parent.name == "moon")
			{
				_ice.GetComponent<Rigidbody2D>().velocity = (_ice.transform.right * 7);
				_ice.transform.eulerAngles = new Vector3(
					_ice.transform.eulerAngles.x,
					_ice.transform.eulerAngles.y,
					_ice.transform.eulerAngles.z-90);
			}
			else
			{
				_ice.GetComponent<Rigidbody2D>().velocity = (_ice.transform.right * -7);
				_ice.transform.eulerAngles = new Vector3(
					_ice.transform.eulerAngles.x,
					_ice.transform.eulerAngles.y,
					_ice.transform.eulerAngles.z);

			}
            StartCoroutine(CooldownTimer(stormAttackCooldown, iBText, "Q"));

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
			cooldownLightning = Time.time + dualLightningAttackCooldown;

			_lightning = Instantiate(_lightningPrefab) as GameObject;
			_lightning.transform.position = transform.TransformPoint(Vector3.forward * 2.0f);

            if (!disableAllSounds)
            {
                SoundManager.instance.Play(soundLightning);
            }

            if (_parent.name == "moon")
			{
				_lightning.GetComponent<Rigidbody2D>().velocity = _lightning.transform.right * 150;
				_lightning.transform.eulerAngles = new Vector3(
					_lightning.transform.eulerAngles.x,
					_lightning.transform.eulerAngles.y,
					_lightning.transform.eulerAngles.z - 90);
			}
			else
			{
				_lightning.GetComponent<Rigidbody2D>().velocity = _lightning.transform.right * -150;
				_lightning.transform.eulerAngles = new Vector3(
					_lightning.transform.eulerAngles.x,
					_lightning.transform.eulerAngles.y,
					_lightning.transform.eulerAngles.z + 90);

			}

            StartCoroutine(CooldownTimer(dualLightningAttackCooldown, lBText, "F"));
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

    private IEnumerator CooldownTimer(float timeCooldown, Text text, System.String button)
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
