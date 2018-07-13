using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tangelo.Test.Components
{
    public interface ISpawnerComponent
    {
        void Spawn(Transform position);
        void Recycle();
    }

    public class SpawnerComponent : MonoBehaviour, ISpawnerComponent
    {
        // QUESTION 3: We want to implement a Spawn system using a Pool. Objects should be recycled
        // Re-implement this method to use a Pool instead of creation on-demand.
        public void Spawn(Transform position)
        {
            GameObject spawnedObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            spawnedObject.AddComponent<SpinnerComponent>();

            NotifyPoolStatus();
        }

        // QUESTION 5: Implement here the recycling of the object. Once you called this method, the object should be added to Pool.
        // If the object is in the pool, it should be invisible
        public void Recycle()
        {
            NotifyPoolStatus();
        }

        // QUESTION 9: We want to notify how many objects are spawned and the number of items in the pool.
        // SendMessage or Static Methods to notify the status to the GameManager, is not allowed.
        private void NotifyPoolStatus()
        {

        }
    }
}