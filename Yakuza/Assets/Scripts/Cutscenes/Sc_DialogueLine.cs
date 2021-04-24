using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class Sc_DialogueLine : Sc_DialogueControler
    {
        public TextMeshProUGUI textHolder;

        [Header("Text Options")]
        [SerializeField] private string input;

        [Header("Time Parameters")]
        [SerializeField] private float delay;


        [Header("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private SpriteRenderer imageHolder;

        private void Awake()
        {
            
            textHolder = GetComponent<TextMeshProUGUI>();
            imageHolder.sprite = characterSprite;
            textHolder.text = "";
        }
        private void Start()
        {
            StartCoroutine(WriteText(input, textHolder, delay));
        }

    }

}

