using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationTransitionSetter : MonoBehaviour
{
    public Hashtable stateConditionTable = new Hashtable();

    private void Start()
    {
        Dictionary<string, float> dict = new Dictionary<string, float>();
        
        addStateConditions(dict);

        stateConditionTable = new Hashtable(dict);
    }

    protected virtual void addStateConditions(Dictionary<string, float> dict)
    {
        dict.Add("idle", 0); //ANIMATION_IDLE = 0;
        dict.Add("walk", 1); //ANIMATION_WALK = 1;
        dict.Add("jump", 2); //ANIMATION_JUMPUP = 2;
        dict.Add("jump up", 2); //ANIMATION_JUMPUP = 2;
        dict.Add("jumpup", 2); //ANIMATION_JUMPUP = 2;
        dict.Add("fall", 3); //ANIMATION_FALL = 3;
        dict.Add("hitted", 4); //ANIMATION_HITTED = 4;
        dict.Add("attack", 5); //ANIMATION_ATTACK = 5;
        dict.Add("death", 6); //ANIMATION_DEATH = 6;

        dict.Add("jump kick", 7); //ANIMATION_JUMP_KICK = 7;
        dict.Add("jumpkick", 7); //ANIMATION_JUMP_KICK = 7;
        dict.Add("kick attack", 8); //ANIMATION_KICK_ATTACK = 8;
        dict.Add("kickattack", 8); //ANIMATION_KICK_ATTACK = 8;
        dict.Add("attack2", 9); //ANIMATION_PUNCH_002 = 9;
        dict.Add("attack3", 10); //ANIMATION_PUNCH_003 = 10;
        dict.Add("knocked down", 11); //ANIMATION_KNOCKED_DOWN = 11;
        dict.Add("get up", 12); //ANIMATION_GET_UP = 12;

        dict.Add("walk backwards", 13); //ANIMATION_WALK_BACKWARDS = 13;
        dict.Add("run", 14); //ANIMATION_RUN = 14;
    }

    // Use this for initialization
    private void Update()
    {
        foreach (Object obj in Selection.objects)
        {
            if (obj.GetType() == typeof(AnimatorStateMachine))
            {
                foreach (ChildAnimatorState state in (obj as AnimatorStateMachine).states)
                {
                    Debug.Log(state.state.name);
                    foreach (AnimatorStateTransition stateTransition in state.state.transitions)
                    {
                        setTransition(stateTransition);
                    }
                }
            }
            /*
            if (obj.GetType() == typeof(AnimatorStateTransition))
            {
                setTransition(obj as AnimatorStateTransition);
            }
            */
        }
    }

    protected virtual void setTransition(AnimatorStateTransition transition)
    {
        Undo.RecordObject(transition, "Set Exit Time Transition");
        transition.hasExitTime = false;
        transition.exitTime = 0;
        transition.hasFixedDuration = true;
        transition.duration = 0;
        transition.offset = 0;
        
        transition.conditions = new AnimatorCondition[1] { getAnimatorCondition(transition.destinationState.name) };
    }

    protected virtual AnimatorCondition getAnimatorCondition(string destinationStateName)
    {
        AnimatorCondition condition = new AnimatorCondition();
        condition.mode = AnimatorConditionMode.Equals;
        condition.parameter = "State";
        condition.threshold = (float)stateConditionTable[destinationStateName.ToLower()];
        return condition;
    }
}
