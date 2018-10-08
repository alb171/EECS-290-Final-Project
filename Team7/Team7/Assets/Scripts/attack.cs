using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    [SerializeField]
    private GameObject _ExplosionPrefab;
    private GameObject _Explosion;

    [SerializeField] AudioClip explosionSound;

    [SerializeField] private bool disableAllSounds;

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (health != null)
        {
            if (GameObject.Find("DarkCollision"))
            {
                health.TakeDamage(5);
            }
            else if ((hit.name.Contains("lightingRing") && gameObject.name.Contains("Character")) || (hit.name.Contains("Character") && gameObject.name.Contains("lightingRing")))
            {

            }
            else
            {
                health.TakeDamage(10);
            }
        }

        if (_ExplosionPrefab.name == "TornadoExplosion")
        {

            Destroy(gameObject, 8f);
            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;
            _Explosion.transform.position = new Vector3(
                                gameObject.transform.position.x,
                                gameObject.transform.position.y,
                                gameObject.transform.position.z - 5);

            if (!disableAllSounds)
            {
                SoundManager.instance.PlaySoundEffect(explosionSound);
            }

        }
        else if (_ExplosionPrefab.name == "FireKill" && hit.name.Contains("IceStorm"))
        {

            Destroy(gameObject, 10f);
            //_Explosion = Instantiate(GameObject.Find("DarkCollision")) as GameObject;
            //_Explosion.transform.position = new Vector3(
             //                   collision.transform.position.x,
            //                    collision.transform.position.y,
               //                 collision.transform.position.z - 5);

            if (!disableAllSounds)
            {
                SoundManager.instance.PlaySoundEffect(explosionSound);
            }

            Destroy(gameObject);
            Destroy(hit);

        }
        else if (hit.name.Contains("TidalExplosion") && _ExplosionPrefab.name.Contains("Enemy"))
        {
            if (gameObject.name.Contains("mainPrefab"))
            {
                gameObject.GetComponent<FollowTarget>().enabled = false;

            }
            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;

            if (!disableAllSounds)
            {
                SoundManager.instance.PlaySoundEffect(explosionSound);
            }

            _Explosion.transform.position = new Vector3(
                                collision.transform.position.x,
                                collision.transform.position.y,
                                collision.transform.position.z - 5);

            yield return new WaitForSeconds(2);

            gameObject.GetComponent<FollowTarget>().enabled = true;

        }
        else if ((_ExplosionPrefab.name == "FireKill" || _ExplosionPrefab.name == "IceKill") && hit.name == "Character")
        {
            Destroy(gameObject, 1.5f);
            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;
            _Explosion.transform.position = new Vector3(
                                collision.transform.position.x,
                                collision.transform.position.y,
                                collision.transform.position.z - 5);
        }
        else if (_ExplosionPrefab.name == "FireKill" || _ExplosionPrefab.name == "IceKill")
        {

            Destroy(gameObject, 10f);
            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;
            _Explosion.transform.position = new Vector3(
                                collision.transform.position.x,
                                collision.transform.position.y,
                                collision.transform.position.z - 5);

        }


        else if (_ExplosionPrefab.name == "lightningRing")
        {
            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;
            _Explosion.transform.position = new Vector3(
                                collision.transform.position.x,
                                collision.transform.position.y,
                                collision.transform.position.z - 5);

            if (!disableAllSounds)
            {
                SoundManager.instance.PlaySoundEffect(explosionSound);
            }

            Destroy(gameObject);
            Destroy(_Explosion, 1f);
        }

        else if (hit.name.Contains("IceSpell") && !gameObject.name.Contains("IceSpell"))
        {
            gameObject.GetComponent<FollowTarget>().enabled = false;

            yield return new WaitForSeconds(3);

            gameObject.GetComponent<FollowTarget>().enabled = true;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<FollowTarget>().enabled = true;
            }
            }
        else if (gameObject.name.Contains("IceSpell"))
        {
            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;
            _Explosion.transform.position = new Vector3(
                                gameObject.transform.position.x,
                                gameObject.transform.position.y,
                                gameObject.transform.position.z - 5);
            Destroy(gameObject);
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                float distance = Mathf.Sqrt((enemy.transform.position.x - this.transform.position.x) * (enemy.transform.position.x - this.transform.position.x)
                    + (enemy.transform.position.y - this.transform.position.y) * (enemy.transform.position.y - this.transform.position.y));
                if (distance <= 10)
                {
                    //StartCoroutine(blackHoleAttack2(enemy));
                    //enemy.GetComponent<FollowTarget>().enabled = true;
                    StartCoroutine(pause(enemy));

                    //enemy.GetComponent<FollowTarget>().SetTarget(transform, true);
                }


            }

        }
        else
        {
            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;
            _Explosion.transform.position = new Vector3(
                                gameObject.transform.position.x,
                                gameObject.transform.position.y,
                                gameObject.transform.position.z - 5);

            if (!disableAllSounds)
            {
                SoundManager.instance.PlaySoundEffect(explosionSound);
            }

            Destroy(gameObject);
            Destroy(_Explosion, 3f);


        }
    }

    IEnumerator pause(GameObject enemy)
    {
        enemy.GetComponent<FollowTarget>().enabled = false;
        yield return new WaitForSeconds(3);
        enemy.GetComponent<FollowTarget>().enabled = true;

    }

    private void OnDestroy()
    {
        if (_ExplosionPrefab.name == "TornadoExplosion")
        {


            _Explosion = Instantiate(_ExplosionPrefab) as GameObject;

            if (!disableAllSounds)
            {
                SoundManager.instance.PlaySoundEffect(explosionSound);
            }

            _Explosion.transform.position = new Vector3(
                                gameObject.transform.position.x,
                                gameObject.transform.position.y,
                                gameObject.transform.position.z - 5);
        }
    }

}
