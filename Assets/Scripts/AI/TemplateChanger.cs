using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemplateChanger : MonoBehaviour
{
    public AITemplate template;
    public AIBeahvior behavior;

    void Start()
    {
        if (GetComponent<Toggle>())
            ToggleBehavior(GetComponent<Toggle>().isOn);
    }

    public void ToggleBehavior(bool value)
    {
        if (value)
            AddBehavior(behavior);
        else
            RemoveBehavior(behavior);
    }

    public void RemoveBehavior(AIBeahvior behavior)
    {
        for (int i = 0; i < template.behaviors.Length; i++)
        {
            if (template.behaviors[i] == behavior)
            {
                template.behaviors[i] = null;
            }
        }
    }

    public void AddBehavior(AIBeahvior behavior)
    {
        int freeIndex = -1;

        for (int i = 0; i < template.behaviors.Length; i++)
        {
            if (template.behaviors[i] == behavior)
            {
                Debug.LogWarning("This template already has this behavior");
                return;
            }
            else if (freeIndex == -1 && template.behaviors[i] == null)
                freeIndex = i;
        }

        // Expanding the array. Shouldn't matter since it's never going to be more than 5 elements
        if (freeIndex == -1)
        {
            AIBeahvior[] newBehaviors = new AIBeahvior[template.behaviors.Length + 1];
            for (int i = 0; i < template.behaviors.Length; i++)
            {
                newBehaviors[i] = template.behaviors[i];
            }
            template.behaviors = newBehaviors;
            freeIndex = newBehaviors.Length - 1;
        }

        template.behaviors[freeIndex] = behavior;
    }

    public void SetSpeed(float value)
    {
        template.maxSpeed = value;
    }

    public void SetForce(float value)
    {
        template.maxForce = value;
    }
}
