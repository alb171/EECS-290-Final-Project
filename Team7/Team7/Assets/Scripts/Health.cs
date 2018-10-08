using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour {
    [SerializeField] public int maxHealth;
    public int currentHealth;
    public RectTransform healthBar;
    public int health = 160;
	public Color damageColor = Color.red;

    public void TakeDamage(int amount)
    {
        int sub = 160 / 10;
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (maxHealth == 100)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //Application.LoadLevel(Application.loadedLevel);
            }
            Debug.Log("Friendly Down");
        }
		gameObject.GetComponent<Renderer> ().material.color = Color.red;
		StartCoroutine ("Delay");
        healthBar.sizeDelta = new Vector2(
        currentHealth * 1.6f,
        healthBar.sizeDelta.y);
    }

	IEnumerator Delay(){
		yield return new WaitForSeconds (0.5f);
		gameObject.GetComponent<Renderer> ().material.color = Color.white;
	}

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

    }
}
