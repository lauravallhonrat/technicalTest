using System.Collections;
using System.Collections.Generic;
using Tangelo.Test.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Tangelo.Test.Managers
{
    // QUESTION 4: Add a new status called "Spawning" that should be called from a key press (for example Space) or from a button click
    public enum GameStatus
    {
        Loading,
        Playing,
        Spawning
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        PrimitiveType m_type = PrimitiveType.Cube;

        [SerializeField]
        [Range(200, 1000)]
        private int m_MaxMassiveSpawningObjects; // Minimum number: 200

        [SerializeField]
        [Range(200, 1000)]
        private int m_MaxRecycleObjects; // Minimum number: 200

        public Button m_spawnButton, m_recycleButton;

        [SerializeField]
        Slider slider;

        private GameStatus m_CurrentStatus = GameStatus.Loading;
        private GameStatus m_Status;

        private ISpawnerComponent spawnerComponent;

        public int spawned = 0;
        public int recicled = 0;


        private void Awake()
        {
            m_CurrentStatus = GameStatus.Loading;

            //UI BUTTON ONCLICK
            m_spawnButton.onClick.AddListener(MassiveSpawn);
            m_recycleButton.onClick.AddListener(MassiveRecycle);
            //reference spawn and recycle
            spawnerComponent = GetComponent<SpawnerComponent>();
           
        }

        private void Update()
        {
            //change status spawning when space is pressed or mouse clicked
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                m_Status = GameStatus.Spawning;

            if (m_CurrentStatus == m_Status)
                return;

            m_CurrentStatus = m_Status;

        }


        // QUESTION 8: Implement here a loading process (using a UI Slider) to show how the Pool is being populated.
        private void PreparePools(float value)
        {
            slider.value = value;
        }

        // QUESTION 6: Implement here a massive spawning of objects.
        // FPS Can't drop below 50fps when MassiveSpawning is being executed.
        // If there are not enough objects in the Pool, increment the size. During the increment of the Pool size,
        // the FPS can't drop below 30fps
        // Implement a UI Button to execute this action
        private void MassiveSpawn()
        {
            StartCoroutine(SpawnFpsController());
        }

        IEnumerator SpawnFpsController()
        {
            //to know current frames
            float currentFPS;
            float i = 0;
            
            while ( i < m_MaxMassiveSpawningObjects)
            {
                currentFPS = 1 / Time.deltaTime;
                if (currentFPS >= 60)
                {
                    spawnerComponent.Spawn(m_type, transform);
                    i++;
                    //calculate percentage slider
                    PreparePools(i / m_MaxMassiveSpawningObjects);
                }
                else
                {
                    //waits 1 frame
                    yield return null;
                }
            }
        }


        // QUESTION 7: Implement here a massive recycling of the objects currently visible in the screen
        // FPS Can't drop below 50fps when MassiveRecycle is being executed
        // Implement a UI Button to execute this action
        private void MassiveRecycle()
        {
            StartCoroutine(RecycleFPSController());
        }

        IEnumerator RecycleFPSController()
        {
            SpinnerComponent[] allCubes = FindObjectsOfType<SpinnerComponent>();
            float currentFPS;
            int i = 0;
            while (i < m_MaxRecycleObjects && allCubes.Length > i )
            {
                currentFPS = 1 / Time.deltaTime;
                if (currentFPS >= 60 )
                {
                    spawnerComponent.Recycle(m_type, allCubes[i].gameObject);
                    i++;
                    PreparePools((float)i / m_MaxRecycleObjects);//convert i to float only in this section to know percentage
                }
                else
                {
                    //waits 1 frame
                    yield return null;
                }
            }
        }
    }
}