using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenalizationManager : MonoBehaviour
{
    private bool triggered;
    private bool firstTime;
    public LayerMask whatIsPlayer;
    private Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        firstTime = true;
        myCollider = GetComponent<Collider2D>();

    }

    void Update()
    {
        triggered = Physics2D.IsTouchingLayers(myCollider, whatIsPlayer);

        if (triggered && firstTime)
        {
            PenalizeLeft.triggerCounter = PenalizeLeft.triggerCounter + 1;

            Debug.Log(PenalizeLeft.triggerCounter);
            if (PenalizeLeft.triggerCounter == 1)
            {
                if (EyeSelection.eye == "SINISTRO")
                {
                    PenalizeRight.opacity = 0.7f;
                    PenalizeRight.penalize = true;
                }
                else
                {
                    PenalizeLeft.opacity = 0.7f;
                    PenalizeLeft.penalize = true;
                }
            }

            if (PenalizeLeft.triggerCounter == 2)
            {
                if (EyeSelection.eye == "SINISTRO")
                    PenalizeRight.opacity = 0.2f;
                else
                    PenalizeLeft.opacity = 0.2f;

                
            }

            if (PenalizeLeft.triggerCounter == 3)
            {
                if (EyeSelection.eye == "SINISTRO")
                    PenalizeRight.opacity = 0f;
                else
                    PenalizeLeft.opacity = 0f;

                
            }

            firstTime = false;
        }
    }


}
