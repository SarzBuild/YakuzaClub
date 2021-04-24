﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class Sc_DialogueHolder : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(DialogueSequence());
        }
        private IEnumerator DialogueSequence()
        {
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(() => transform.GetChild(i).GetComponent<Sc_DialogueLine>().dialogueFinished);
            }
            gameObject.SetActive(false);
        }

        private void Deactivate()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}