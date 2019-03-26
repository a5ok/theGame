using Assets._3D4amb_LIB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the triggers on Penalty, 
/// meaning that it decides when the PenaltyInfo is increased during the scene
/// (of course if it's not set on Static (<see cref="GamePolicy.IncreaseType")/>
/// </summary>
/// <remarks>If you have no need to increase the penalty during the game
/// in the same scene, just disable the whole GameObject</remarks>
public class PenaltyTrigger : MonoBehaviour
{
    /// <summary>
    /// The type of trigger, if it increases in time or by 
    /// event happenings
    /// </summary>
    public enum TriggerType
    {
        TIME, EVENT
    }

    /// <summary>
    /// If <see cref="TriggerType"/> is set on "EVENT", use
    /// some of this EventTypes already implemented or define
    /// your own custom event that will trigger a PenaltyInfo increase
    /// </summary>
    public enum EventType
    {
        NO_ENEMIES, NO_GOOD_ITEMS,
        CUSTOM
    }

    /// <summary>
    /// <see cref="TriggerType"/>
    /// </summary>
    public TriggerType triggerType;
    /// <summary>
    /// <see cref="EventType"/>
    /// </summary>
    public EventType eventType;

    private PenaltyManager PM;

    public int InitialDelay = 0;

    /// <summary>
    /// This is the time passed between a check of event and another
    /// </summary>
    public int SecondsBetweenCheck = 1;

    /// <summary>
    /// This is used only if TriggerType is set to Time.
    /// This will increase the penaltyInfoNow every tot.
    /// </summary>
    public int SecondsBetweenIncrease = 1;

    /// <summary>
    /// This bool indicates if I should read the events (stop the TimeGoing during pauses
    /// or other moments you don't want to spend time for penalty)
    /// </summary>
    public bool TimeGoing = true;

    void Start()
    {
        PM = gameObject.GetComponent<PenaltyManager>();
        if (PM.PolicyIncreaseType == GamePolicy.IncreaseType.DYNAMIC)
        {
            switch (triggerType)
            {
                case TriggerType.TIME: InvokeRepeating("TimeTrigger", InitialDelay, SecondsBetweenIncrease); break;
                case TriggerType.EVENT: InvokeRepeating("EventTriggerIncrease", InitialDelay, SecondsBetweenCheck); break;
                default: Debug.Log("Unknown TriggerType"); break;
            }
        }
        else Destroy(this);
    }

    /// <summary>
    /// This method is used if <see cref="TriggerType.TIME"/> is set
    /// </summary>
    void TimeTrigger()
    {
        if(TimeGoing)
        {
            //Debug.Log("Time is going");
            PM.IncreasePenaltyNow();
        }
        else
        {
            //Debug.Log("Time is stopped");
        }
    }

    public void EventDeath()
    {
        PM.ResetPenalty();
    }

    /// <summary>
    /// This method is used if <see cref="TriggerType.EVENT"/> is set
    /// </summary>
    void EventTriggerIncrease()
    {
        switch(eventType)
        {
            case (EventType.NO_ENEMIES):
                {
                    GameObject[] gos = GameObject.FindGameObjectsWithTag("EnemyTag");
                    if(gos.Length == 0)
                    {
                        PM.IncreasePenaltyNow();
                    }
                    break;
                }
            case (EventType.NO_GOOD_ITEMS):
                {
                    GameObject[] gos = GameObject.FindGameObjectsWithTag("GoodItemTag");
                    if (gos.Length == 0)
                    {
                        PM.IncreasePenaltyNow();
                    }
                    break;
                }
            default:
                {
                    if (Condition())
                    {
                        //Debug.Log("Custom Condition reached");
                        PM.IncreasePenaltyNow();
                    }
                    break;
                }
        }
    }

    /// <summary>
    /// Override this is your son of PenaltyTrigger and write
    /// your own condition. When this is true, the PenaltyManager will increase the penalty
    /// </summary>
    /// <returns>True if the condition is reached</returns>
    protected virtual bool Condition()
    {
        Debug.Log("Condition not implemented. Are you calling the wrong one?");
        return false;
    }
   
}
