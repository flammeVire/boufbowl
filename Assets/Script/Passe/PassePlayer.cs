using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassePlayer : MonoBehaviour
{
    //public
    public GameObject[] PositionAlly;
    public int AllySelected;
    public int indexCurrentAlly = 0;
    public bool choixPasse = false;
    public Transform transformBall;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (choixPasse == true)
        {
            PositionAlly = GameObject.FindGameObjectsWithTag("ally");
        }
    }
    
    public void SelectedAlly() {
        //deplacer la selection vers la droite
        if (indexCurrentAlly > PositionAlly.Length) {
            indexCurrentAlly = 0;
            if (Input.GetKeyDown("Right")) {
                indexCurrentAlly ++;
                Debug.Log(indexCurrentAlly);
            }
        }
        //deplacer la selection par la gauche
        if (indexCurrentAlly < 0) {
            indexCurrentAlly = PositionAlly.Length - 1;
            if (Input.GetKeyDown("Left"))  { 
                indexCurrentAlly--;
            }
        }
        if (Input.GetKeyDown("Jump")) {
            AllySelected = indexCurrentAlly;
        }
        transformBall.localPosition = PositionAlly[AllySelected].transform.localPosition;
    }
}
