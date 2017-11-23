using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   
    public bool Activated = false;

    private Animator thisAnimator;

    public static GameObject[] CheckPointsList;


    /// Get position of the last activated checkpoint
    public static Vector3 GetActiveCheckPointPosition()
    {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = new Vector3(-328.5f, 8.4f, -528.5f);

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<CheckPoint>().Activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }

        return result;
    }

    private void ActivateCheckPoint()
    {
        // We deactive all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<CheckPoint>().Activated = false;
            cp.GetComponent<Animator>().SetBool("Active", false);
        }

        // We activated the current checkpoint
        Activated = true;
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
