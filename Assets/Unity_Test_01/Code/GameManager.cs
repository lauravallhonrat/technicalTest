using System.Collections;
using System.Collections.Generic;
using Tangelo.Test.Components;
using UnityEngine;

namespace Tangelo.Test.Managers
{
    // QUESTION 4: Add a new status called "Spawning" that should be called from a key press (for example Space) or from a button click
    public enum GameStatus { Loading, Playing }

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private int m_MaxMassiveSpawningObjects; // Minimum number: 200

        [SerializeField]
        private int m_MaxRecycleObjects; // Minimum number: 200

        private GameStatus m_CurrentStatus = GameStatus.Loading;
        private GameStatus m_Status;

        private void Awake()
        {
            m_CurrentStatus = GameStatus.Loading;
        }

        private void Update()
        {
            if (m_CurrentStatus == m_Status)
                return;

            m_CurrentStatus = m_Status;
        }

        // QUESTION 8: Implement here a loading process (using a UI Slider) to show how the Pool is being populated.
        private void PreparePools()
        {

        }

        // QUESTION 6: Implement here a massive spawning of objects.
        // FPS Can't drop below 50fps when MassiveSpawning is being executed.
        // If there are not enough objects in the Pool, increment the size. During the increment of the Pool size,
        // the FPS can't drop below 30fps
        // Implement a UI Button to execute this action
        private void MassiveSpawn()
        {

        }

        // QUESTION 7: Implement here a massive recycling of the objects currently visible in the screen
        // FPS Can't drop below 50fps when MassiveRecycle is being executed
        // Implement a UI Button to execute this action
        private void MassiveRecycle()
        {

        }
    }
}