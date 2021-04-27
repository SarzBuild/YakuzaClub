using System.Collections;
using UnityEngine;

namespace DialogueSystem
{
    
    public class Sc_DialogueHolder : MonoBehaviour
    {
        public GameObject hitbox;
        public GameObject point1;
        public GameObject point2;
        public GameObject point3;
        public GameObject point4;
        public GameObject point5;
        public GameObject point6;
        public GameObject point7;
        public GameObject point8;
        public GameObject point9;
        public GameObject point10;
        public GameObject point11;
        public GameObject point12;
        public GameObject point13;
        public GameObject point14;
        public GameObject point15;
        public GameObject point16;
        public GameObject point17;
        public GameObject point18;
        public GameObject copEnemy;

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
            SpawnCops();
            Destroy(hitbox);
            Destroy(transform.gameObject);

        }

        private void SpawnCops()
        {
            Instantiate(copEnemy, new Vector3(point1.transform.position.x, point1.transform.position.y, point1.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point2.transform.position.x, point2.transform.position.y, point2.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point3.transform.position.x, point3.transform.position.y, point3.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point4.transform.position.x, point4.transform.position.y, point4.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point5.transform.position.x, point5.transform.position.y, point5.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point6.transform.position.x, point6.transform.position.y, point6.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point7.transform.position.x, point7.transform.position.y, point7.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point8.transform.position.x, point8.transform.position.y, point8.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point9.transform.position.x, point9.transform.position.y, point9.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point10.transform.position.x, point10.transform.position.y, point10.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point11.transform.position.x, point11.transform.position.y, point11.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point12.transform.position.x, point12.transform.position.y, point12.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point13.transform.position.x, point13.transform.position.y, point13.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point14.transform.position.x, point14.transform.position.y, point14.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point15.transform.position.x, point15.transform.position.y, point15.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point16.transform.position.x, point16.transform.position.y, point16.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point17.transform.position.x, point17.transform.position.y, point17.transform.position.z), Quaternion.identity);
            Instantiate(copEnemy, new Vector3(point18.transform.position.x, point18.transform.position.y, point18.transform.position.z), Quaternion.identity);
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
