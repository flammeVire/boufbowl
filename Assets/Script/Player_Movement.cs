using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Movement : MonoBehaviour
{
    
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private GameObject DesiredPosition;
    [SerializeField] private int NBMovement;
    [SerializeField] private int MaxMovement;
    public List<Vector3Int> MovementList;
    public Transform RedGoalLine;
    public Transform BlueGoalLine;
    public bool IsSelected = false;
    public bool ResetDesiredPosition = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        MovementList = new List<Vector3Int>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (NBMovement < MaxMovement && IsSelected)
        {
            if(ResetDesiredPosition)
            {
                DesiredPosition.transform.position = PlayerTransform.position;
                ResetDesiredPosition = false;
            }
            
            //Deplacement de du déplacement désiré (flèche)
            if (Input.GetKeyDown(KeyCode.D)) { // Right
                DesiredPosition.transform.position += new Vector3Int(1, 0, 0);
                MovementList.Add(new Vector3Int(1, 0, 0));
            } else if (Input.GetKeyDown(KeyCode.W)) { // Up
                DesiredPosition.transform.position += new Vector3Int(0, 0, 1);
                MovementList.Add(new Vector3Int(0, 0, 1));
            } else if (Input.GetKeyDown(KeyCode.A)) { // Left
                DesiredPosition.transform.position += new Vector3Int(-1, 0, 0);
                MovementList.Add(new Vector3Int(-1, 0, 0));
            } else if (Input.GetKeyDown(KeyCode.S)) { // Down
                DesiredPosition.transform.position += new Vector3Int(0, 0, -1);
                MovementList.Add( new Vector3Int(0, 0, -1));
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                for (int i = 0; i < MovementList.Count; i++)
                {
                    PlayerTransform.position += MovementList[i];
                    VerifPlayerToDesired();
                    
                }
                GetComponent<CharactereSelection>().AllMovementList.Add(MovementList);
                MovementList = new List<Vector3Int>();
            }
            
        }
        
        GoingThroughGoal();
        
    }
    

    bool VerifPlayerToDesired()
    {
        bool xOK = Mathf.Abs(PlayerTransform.position.x - DesiredPosition.transform.position.x) < 0.1f;
        bool yOK = Mathf.Abs(PlayerTransform.position.y - DesiredPosition.transform.position.y) < 0.1f;

        if (xOK && yOK)
        {
            IsSelected = false;
            FindObjectOfType<CharactereSelection>().CharacterSelected = false;
            return true;
        }
        return false;
    }
    
    
    
    //private IEnumerator MovePlayer()
    //{
    //    for (int i = 0; i < MovementList.Count; i++)
    //    {
    //        PlayerTransform.position += MovementList[i];
     //       if (VerifPlayerToDesired())
     //       {
     //           break;
     //       }
     //       else
      //      {
     //           PlayerTransform.position = MovementList[i];
     //       }
     //   }
     //           
     //   MovementList = new List<Vector3Int>();
     //   yield return new WaitUntil(() => PlayerTransform == DesiredPosition.transform);
   // }

   void GoingThroughGoal()
   {
       if (this.gameObject.CompareTag("Red Team") && PlayerTransform.position.x >= BlueGoalLine.position.x)
       {
           Debug.Log("But de l'equipe rouge");
       }
       else if (this.gameObject.CompareTag("Blue Team") && PlayerTransform.position.x >= RedGoalLine.position.x)
       {
           Debug.Log("But de l'equipe Bleue");
       }
   }
}
