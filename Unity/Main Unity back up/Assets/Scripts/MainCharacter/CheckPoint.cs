using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   
    public bool ActivatedPoint = false;

    private Animator thisAnimator;

    public static GameObject[] CheckPointsList;


    /// Get position of the last activated checkpoint
    public static Vector3 GetActiveCheckPointPosition()
    {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = new Vector3(83f, -33.19f, -826.1f);

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<CheckPoint>().ActivatedPoint)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }

        return result;
    }

    public void ActivateCheckPoint()
    {
        // We deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<CheckPoint>().ActivatedPoint = false;
            cp.GetComponent<Animator>().SetBool("Active", false);
        }

        // We activated the current checkpoint
        ActivatedPoint = true;
        thisAnimator.SetBool("Active", true);
    }

   void Start()
    {
        thisAnimator = GetComponent<Animator>();

        // We search all the checkpoints in the current scene
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
    }

    void OnTriggerEnter(Collider other)
    {
        // If the player passes through the checkpoint, we activate it
        if (other.tag == "Player")
        {
            ActivateCheckPoint();
        }
    }
}
