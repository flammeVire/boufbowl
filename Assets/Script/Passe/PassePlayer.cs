using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassePlayer : MonoBehaviour
{
    //public
    public GameObject[] PositionAlly;
    public int AllySelected = -1;
    public int indexCurrentAlly = 0;
    public bool choixPasse = false;
    public Transform transformBall;
    public float passSpeed = 10f;
    public StatCharacter statCharacter;
    
    
    //priver
    private Vector3 targetPosition;
    private bool isBallMoving = false;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            choixPasse = true;
        }
        //si le choix de la passe est validé
        if (choixPasse == true)
        {
            if (PositionAlly == null || PositionAlly.Length == 0)
            {
                PositionAlly = GameObject.FindGameObjectsWithTag("Blue Team");
                Debug.Log("Allies : " + PositionAlly);
                if (PositionAlly.Length > 0)
                {
                    indexCurrentAlly = 0;
                }
            } 
            if (!isBallMoving)
            {
                SelectedAlly();
            }
        }
        if (isBallMoving)
        {
            LancerBallUpdate();
        }
    }
    public void SelectedAlly() {
        if (PositionAlly.Length == 0)
        {
            Debug.Log("No Ally");
            return;
        }
        if (Input.GetKeyDown(KeyCode.D)) 
        {
            Debug.Log("D");
            indexCurrentAlly++;
            if (indexCurrentAlly >= PositionAlly.Length) 
            {
                indexCurrentAlly = 0;
            }
            Debug.Log("Sélection allié : " + indexCurrentAlly);
        }
        if (Input.GetKeyDown(KeyCode.Q))  
        { 
            Debug.Log("Q");
            indexCurrentAlly--;
            if (indexCurrentAlly < 0) 
            {
                indexCurrentAlly = PositionAlly.Length - 1;
            }
            Debug.Log("Sélection allié : " + indexCurrentAlly);
        }
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            AllySelected = indexCurrentAlly;
            Debug.Log("Allié CONFIRMÉ : " + AllySelected);
            InitialiserPasse();
        }
    }
    public void InitialiserPasse()
    {
        if (AllySelected != -1 && PositionAlly.Length > AllySelected)
        {
            targetPosition = PositionAlly[AllySelected].transform.position;
            isBallMoving = true;
            choixPasse = false;
        }
    }

    public void LancerBallUpdate()
    {
        if (transformBall == null)
        {
            return;
        }
        transformBall.position = Vector3.MoveTowards(
            transformBall.position, 
            targetPosition, 
            passSpeed * Time.deltaTime
        );
        if (transformBall.position == targetPosition)
        {
            isBallMoving = false;
            PositionAlly[AllySelected].GetComponent<PassePlayer>().statCharacter.isHaveBall = true;
            Debug.Log(PositionAlly[AllySelected].GetComponent<PassePlayer>().statCharacter.isHaveBall);
            AllySelected = -1;
            statCharacter.isHaveBall = false;
            Debug.Log(statCharacter.isHaveBall);
            Debug.Log("Passe terminée.");
        }
    }
}