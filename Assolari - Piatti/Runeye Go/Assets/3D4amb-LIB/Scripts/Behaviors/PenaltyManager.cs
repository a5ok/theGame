using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Dweiss;
using System;
using Assets._3D4amb_LIB.Scripts.Utils;

namespace Assets._3D4amb_LIB
{
    /// <summary>
    /// This class contains most of the fields and methods used to manage the penalty
    /// in the whole library
    /// </summary>
    /// <remarks>Read the documentation (thesis document) about this class</remarks>
    public class PenaltyManager : MonoBehaviour
    {
        /// <summary>
        /// The actual Difficulty that the game is using
        /// </summary>
        public GameDifficulty Difficulty;
        /// <summary>
        /// The Eye that must be penalized
        /// </summary>
        public Eye HealthyEye;
        /// <summary>
        /// This is how many steps the game do between the min to the max penalty
        /// (for each difficulty)
        /// </summary>
        /// <remarks>If the game is procedural and can go beyond the game go up in difficulty
        /// to the max of max difficulty</remarks>
        public int Steps;
        
        [Header("--- Game Policies ---")]
        /// <summary>
        /// <see cref="GamePolicy.DifficultyProgression"/>
        /// </summary>
        public GamePolicy.DifficultyProgression PolicyDifficultyProgression;
        /// <summary>
        /// <see cref="GamePolicy.IncreasePenalty"/>
        /// </summary>
        public GamePolicy.IncreasePenalty PolicyIncreasePenalty;
        /// <summary>
        /// <see cref="GamePolicy.IncreaseType"/>
        /// </summary>
        public GamePolicy.IncreaseType PolicyIncreaseType;
        /// <summary>
        /// <see cref="GamePolicy.IncreaseResetOnDeath"/>
        /// </summary>
        public GamePolicy.IncreaseResetOnDeath PolicyResetOnDeath;
        /// <summary>
        /// <see cref="GamePolicy.DifficultyBounds"/>
        /// </summary>
        /// <remarks>This is set on behalf of the PrefManager one</remarks>
        [HideInInspector]
        public GamePolicy.DifficultyBounds PolicyDifficultyBounds;

        [Header("--- Static Penalty---")]
        /// <summary>
        /// This value is used in case of static penalty
        /// for the trasparency
        /// </summary>
        /// <remarks>Set this in the inspector and this will never change</remarks>
        public float StaticTrasp;
        /// <summary>
        /// This value is used in case of static penalty
        /// for the eyePatch
        /// </summary>
        /// <remarks>Set this in the inspector and this will never change</remarks>
        public float StaticEyePatch;
        /// <summary>
        /// The PenaltyInfo that is being used right now
        /// </summary>
        public PenaltyInfo PenaltyInfoNow;
        /// <summary>
        /// The last PenaltyInfo (used to reset at previous 
        /// in some GamePolicies)
        /// </summary>
        public PenaltyInfo PenaltyInfoLast;
        /// <summary>
        /// This Dictionary loads the StartPenaltie values from
        /// the file Settings
        /// </summary>
        public Dictionary<GameDifficulty, PenaltyInfo> StartPenalties;
        /// <summary>
        /// This Dictionary loads the EndPenaltyie values from
        /// the file Settings
        /// </summary>
        /// <remarks>Set the maximum a little more than 1 (1.01f works). This because of roundings</remarks>
        public Dictionary<GameDifficulty, PenaltyInfo> EndPenalties;
        
        /// <summary>
        /// TODO: comment me!
        /// </summary>
        public String[] TagToPenalize;

        void Awake()
        {
            Settings s = Settings.Instance;
            PrefManager prefManager = GameObject.Find("PrefManager").GetComponent<PrefManager>();
            PlayerSettings actualPlayerSettings = prefManager.actualPlayerSettings;
            HealthyEye = actualPlayerSettings.healthyEye;
            Difficulty = actualPlayerSettings.difficultyStart;
            PolicyDifficultyBounds = prefManager.PolicyDifficultyBounds;

            switch (PolicyIncreaseType)
            {
                case (GamePolicy.IncreaseType.STATIC):
                    {
                        AwakeStatic();
                        break;
                    }
                case (GamePolicy.IncreaseType.DYNAMIC):
                    {
                        AwakeDynamic();
                        break;
                    }
            }
        }

        protected void SetDifficultyBoundsFromFile()
        {
            Settings s = Settings.Instance;
            StartPenalties = new Dictionary<GameDifficulty, PenaltyInfo>()
            {
                {GameDifficulty.EASY,   new PenaltyInfo(HealthyEye,
                                        s.easyStartTransparency,
                                        s.easyStartEyePatch)},
                {GameDifficulty.MEDIUM, new PenaltyInfo(HealthyEye,
                                        s.medStartTrasparency,
                                        s.medStartEyePatch)},
                {GameDifficulty.HARD,   new PenaltyInfo(HealthyEye,
                                        s.hardStartTrasparency,
                                        s.hardStartEyePatch)   }
            };

            EndPenalties = new Dictionary<GameDifficulty, PenaltyInfo>
            {
                {GameDifficulty.EASY,   new PenaltyInfo(HealthyEye,
                                        s.easyEndTransparency,
                                        s.easyEndEyePatch)},
                {GameDifficulty.MEDIUM, new PenaltyInfo(HealthyEye,
                                        s.medEndTransparency,
                                        s.medEndEyePatch)},
                {GameDifficulty.HARD,   new PenaltyInfo(HealthyEye,
                                        s.hardEndTransparency,
                                        s.hardEndEyePatch)},
            };
            PenaltyInfoNow = new PenaltyInfo(GetStartPenalty());
        }

        protected void SetDifficultyBoundsFromPlayer()
        {
            PrefManager prefManager = GameObject.Find("PrefManager").GetComponent<PrefManager>();
            StartPenalties = new Dictionary<GameDifficulty, PenaltyInfo>()
            {
                {GameDifficulty.EASY,   prefManager.LoadPlayerPIstart()}
            };
            EndPenalties = new Dictionary<GameDifficulty, PenaltyInfo>()
            {
                {GameDifficulty.EASY,   prefManager.LoadPlayerPIend()}
            };
            Difficulty = GameDifficulty.EASY;
            PolicyDifficultyProgression = GamePolicy.DifficultyProgression.FIXED;   //if the Dev forgets this: so it stays in "easy" (single) difficulty
            PenaltyInfoNow = GetStartPenalty();
        }

        protected void AwakeDynamic()
        {
            switch(PolicyDifficultyBounds)
            {
                case GamePolicy.DifficultyBounds.FROM_FILE:
                    //Debug.Log("Loading bounds from file");
                    SetDifficultyBoundsFromFile();
                    break;
                case GamePolicy.DifficultyBounds.FROM_PLAYER:
                    //Debug.Log("Loading bounds from player");
                    SetDifficultyBoundsFromPlayer();
                    break;
            }
        }

        protected void AwakeStatic()
        {
            PenaltyInfoNow = new PenaltyInfo(HealthyEye, StaticTrasp, StaticEyePatch);
        }

        /// <summary>
        /// Reset the penalty
        /// </summary>
        /// <remarks>This method has different behaviours depending on 
        /// GamePolicy.IncreaseResetOnDeath
        /// <see cref="GamePolicy.IncreaseResetOnDeath"/>
        /// </remarks>
        public void ResetPenalty()
        {
            switch(PolicyResetOnDeath)
            {
                case (GamePolicy.IncreaseResetOnDeath.TO_ZERO):
                    {
                        ResetPenaltyToZero();
                        break;
                    }
                case (GamePolicy.IncreaseResetOnDeath.LAST):
                    {
                        ResetPenaltyNowTo(PenaltyInfoLast);
                        break;
                    }
                case (GamePolicy.IncreaseResetOnDeath.NO_RESET):
                    {
                        break;
                    }
            }
            //Debug.Log("Penalty reset to " + PenaltyInfoNow);
            PenaltyInfoLast = new PenaltyInfo(PenaltyInfoNow);
            //Debug.Log("PenaltyInfoLast: " + PenaltyInfoLast);
        }

        /// <summary>
        /// This reset the penalty back to 0
        /// </summary>
        /// <remarks>This will NOT set penalty to the starting one, just 0</remarks>
        protected void ResetPenaltyToZero()
        {
            PenaltyInfoNow.PenaltyTransparency = 0f;
            PenaltyInfoNow.PenaltyEyePatch = 0f;
        }

        /// <summary>
        /// This resets the penalty now to the value passed as parameter
        /// </summary>
        /// <param name="reset">The PenaltyInfo which you want to go back</param>
        protected void ResetPenaltyNowTo(PenaltyInfo reset)
        {
            PenaltyInfoNow = new PenaltyInfo(reset);
            CheckAcceptableValues();
        }

        /// <summary>
        /// Get the Penalty of the start, based on the current difficulty
        /// </summary>
        /// <returns>The PenaltyInfo to start with</returns>
        public PenaltyInfo GetStartPenalty()
        {
            return StartPenalties[Difficulty];
        }
        /// <summary>
        /// Get the Penalty of the end, based on the current difficulty
        /// </summary>
        /// <returns>The PenaltyInfo to end with</returns>
        public PenaltyInfo GetEndPenalty()
        {
            return EndPenalties[Difficulty];
        }
        /// <summary>
        /// Increase the current PenaltyInfo
        /// </summary>
        /// <remarks>This method has different behaviours based on <see cref="GamePolicy.IncreasePenalty"/></remarks>
        public void IncreasePenaltyNow()
        {
            PenaltyInfoLast = new PenaltyInfo(PenaltyInfoNow);
            //Debug.Log("PenaltyInfoLast: " + PenaltyInfoLast.ToString());
            switch(PolicyIncreasePenalty)
            {
                case GamePolicy.IncreasePenalty.BY_STEPS: IncreasePenaltyNowBySteps(); break;
                case GamePolicy.IncreasePenalty.MANUAL: IncreasePenaltyNowManual(); break;
            }
        }

        /// <summary>
        /// Dev: override this in your son class as you prefer
        /// </summary>
        /// <remarks>You can use the other methods overloadings of IncrasePenaltyNow</remarks>
        protected virtual void IncreasePenaltyNowManual()
        {
            Debug.Log("IncreasePenaltyNowManual not implemented (maybe you are calling the wrong one)");
        }

        /// <summary>
        /// This increase the penalty of a float number based on the number of steps
        /// </summary>
        /// <remarks>Be sure that you set a number of steps. Also, the max value for both PenaltyInfo values is 1, if 
        /// your setting goes beyond for some reason it's set to 1</remarks>
        private void IncreasePenaltyNowBySteps()
        {
            float stepTrasp = (GetEndPenalty().PenaltyTransparency - GetStartPenalty().PenaltyTransparency) / Steps;
            float stepEyePatch = (GetEndPenalty().PenaltyEyePatch - GetStartPenalty().PenaltyEyePatch) / Steps;

            float finalTrasp = PenaltyInfoNow.PenaltyTransparency + stepTrasp;
            float finalEyePatch = PenaltyInfoNow.PenaltyEyePatch + stepEyePatch;

            //Debug.Log("stepTrasp " + stepTrasp
            //+"\nstepEyePatch " + stepEyePatch);

            if (PolicyDifficultyProgression == GamePolicy.DifficultyProgression.INFINITE)
            {
                PenaltyInfoNow.PenaltyTransparency = finalTrasp;
                PenaltyInfoNow.PenaltyEyePatch = finalEyePatch;
                if(PenaltyInfoNow.PenaltyTransparency >= EndPenalties[Difficulty].PenaltyTransparency
                    && PenaltyInfoNow.PenaltyEyePatch >= EndPenalties[Difficulty].PenaltyEyePatch)
                {
                    Difficulty = DifficultyManager.GetNext(Difficulty);
                }
            }
            else
            {
                if (finalTrasp <= GetEndPenalty().PenaltyTransparency)
                {
                    PenaltyInfoNow.PenaltyTransparency = finalTrasp;
                }
                else Debug.Log("Cant' increase transparency because of the difficulty");
                if (finalEyePatch <= GetEndPenalty().PenaltyEyePatch)
                {
                    PenaltyInfoNow.PenaltyEyePatch = finalEyePatch;
                }
                else Debug.Log("Cant' increase eyepatch because of the difficulty");
            }
            CheckAcceptableValues();
            //Debug.Log("Policies.PolicyDifficultyProgression :" + PolicyDifficultyProgression);
            //Debug.Log("PenaltyInfoNow: " + PenaltyInfoNow.ToString());
        }

        /// <summary>
        /// This increase the penaly from the start of the current difficulty for a
        /// given number X on total steps. <see cref="GamePolicy.IncreasePenalty.BY_STEPS"/>
        /// </summary>
        /// <remarks>This uses the logic of the number of steps so be sure to have set those.
        /// Also if X==0 it returs the StartPenalty for the difficult set.
        /// Also if you do more steps than the one you set, you should probably set the <see cref="GamePolicy.DifficultyProgression.INFINITE"/>
        /// </remarks>
        /// <example>It can be used to have the penalty for level X of difficulty D</example>
        /// <param name="X">The number of steps that you want to make</param>
        public void IncreasePenaltyNow(int X)
        {
            for(int i=0;i<X;i++)
            {
                IncreasePenaltyNow();
            }
        }

        /// <summary>
        /// this method check and set the current values
        /// of PenaltyInfoNow so that they are 0 or above and less than 1 or 1.
        /// </summary>
        /// <example>If the PenaltyInfoNow has trasparency at -0.4f it sets it to 0, if 
        /// it's at 1.00002 it sets it to 1</example>
        public void CheckAcceptableValues()
        {
            //Not below 0
            if (PenaltyInfoNow.PenaltyTransparency < 0) PenaltyInfoNow.PenaltyTransparency = 0;
            if (PenaltyInfoNow.PenaltyEyePatch < 0) PenaltyInfoNow.PenaltyEyePatch = 0;
            //Not "far" beyond 1
            if (PenaltyInfoNow.PenaltyTransparency > 1) PenaltyInfoNow.PenaltyTransparency = 1;
            if (PenaltyInfoNow.PenaltyEyePatch > 1) PenaltyInfoNow.PenaltyEyePatch = 1;
        }

        /// <summary>
        /// This manually increase the PenaltyInfoNow without regards of 
        /// start and end penalty
        /// </summary>
        /// <remarks>Use this only if you don't want to increase of a fixed amount
        /// every step (you should use this always after you used this once)</remarks>
        /// <param name="added">This PenaltyInfo contains values added to the PenaltyInfoNow</param>
        /// <example>
        /// (pseudocode)
        /// PenaltyInfoNow = [0.2, 0];
        /// IncreasePenaltyNow(new PenaltyInfo(0.3, 0.5));
        /// => Now PenaltyInfo = [0.5, 0.5]
        /// </example>
        public void IncreasePenaltyNow(PenaltyInfo added)
        {
            PenaltyInfoNow.PenaltyTransparency += added.PenaltyTransparency;
            PenaltyInfoNow.PenaltyEyePatch += added.PenaltyEyePatch;
            CheckAcceptableValues();
        }

        /// <summary>
        /// This manually decrease the PenaltyInfoNow without regards of 
        /// start and end penalty
        /// </summary>
        /// <remarks>This can't go below zero (if it does it's set to 0)</remarks>
        /// <param name="subbed">This PenaltyInfo contains values subbed to the PenaltyInfoNow</param>
        /// /// <example>
        /// (pseudocode)
        /// PenaltyInfoNow = [0.2, 0.5];
        /// SubPenaltyNow(new PenaltyInfo(0.1, 0.3));
        /// => Now PenaltyInfo = [0.1, 0.2]
        /// </example>
        public void SubPenaltyNow(PenaltyInfo subbed)
        {
            PenaltyInfoNow.PenaltyTransparency -= subbed.PenaltyTransparency;
            PenaltyInfoNow.PenaltyEyePatch -= subbed.PenaltyEyePatch;
            CheckAcceptableValues();
        }
    }
}
