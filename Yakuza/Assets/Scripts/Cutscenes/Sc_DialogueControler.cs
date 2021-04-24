using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace DialogueSystem
{
    public class Sc_DialogueControler : MonoBehaviour
    {
        public bool dialogueFinished { get; private set; }
        protected IEnumerator WriteText(string input, TextMeshProUGUI textHolder, float delay)
        {
            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButton(0));
            dialogueFinished = true;
        }

    }
}

