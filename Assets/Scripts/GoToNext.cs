using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToNext : MonoBehaviour
{
  public GameObject player;

  public void Start()
  {
    
  }

  public void Update()
  {

  }

  private void OnTriggerEnter(Collider other)
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //Set hasKey to false so it is not true in the next scene
    GameObject.Find("Player").GetComponent<PlayerInventory>().hasKey = false;
  }
}
