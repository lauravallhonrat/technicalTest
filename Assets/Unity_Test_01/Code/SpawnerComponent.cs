using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tangelo.Test.Components
{
    public interface ISpawnerComponent
    {
        void Spawn(PrimitiveType primitiveType, Transform transform);
        void Recycle(PrimitiveType primitiveType, GameObject element);
    }

    public class SpawnerComponent : MonoBehaviour, ISpawnerComponent
    {
        // QUESTION 3: We want to implement a Spawn system using a Pool. Objects should be recycled
        // Re-implement this method to use a Pool instead of creation on-demand.

        //POOLSYSTEM

        //get the reference to gameManager
        Tangelo.Test.Managers.GameManager gm;

        //we create a static dictionary that will contain the pool type kind and a list of objects, also allows to add more primitiveTypes, not only cubes :)
        private Dictionary<PrimitiveType, List<GameObject>> pooling = new Dictionary<PrimitiveType, List<GameObject>>();

       public Texture2D spriteTexture;
       private Sprite mySprite;
       private SpriteRenderer sr;


        private void Initialize()
        {
            gm = FindObjectOfType<Tangelo.Test.Managers.GameManager>();

            sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;

            //Adding each kind of pooltype by creating a new list
            pooling.Add(PrimitiveType.Cube, new List<GameObject>());
            pooling.Add(PrimitiveType.Quad, new List<GameObject>());
            pooling.Add(PrimitiveType.Sphere, new List<GameObject>());
            pooling.Add(PrimitiveType.Capsule, new List<GameObject>());
        }

        private GameObject GetElement(PrimitiveType primitiveType)
        {
            //if it's executing the list for first time--> Initializes
            if (pooling.Count == 0)
                Initialize();

            //if there's an element in the list--> checks, sets to active and returns the object
            if (pooling[primitiveType].Count > 0)
            {
                GameObject element = pooling[primitiveType][0];
                pooling[primitiveType].Remove(element);
                element.SetActive(true);
                return element;
            }
            return null;
        }


        public void Spawn(PrimitiveType primitiveType, Transform transform)
        {

            //searching the object kind in the pool
            GameObject spawnedObject = GetElement(primitiveType);

            //if there's no object in the pool we create it
            if (spawnedObject == null)
            {
                //2D
                if (primitiveType == PrimitiveType.Quad)
                {
                   spawnedObject = GameObject.CreatePrimitive(primitiveType);
                   mySprite = Sprite.Create(spriteTexture, new Rect(0.0f, 0.0f, spriteTexture.width, spriteTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
                   sr.sprite = mySprite;
                }
                else 
                    spawnedObject = GameObject.CreatePrimitive(primitiveType);
                
                spawnedObject.AddComponent<SpinnerComponent>();
            }

            //positioning & activating
            spawnedObject.transform.position = transform.position;
            spawnedObject.SetActive(true);

            NotifyPoolStatus(true);
        }

        // QUESTION 5: Implement here the recycling of the object. Once you called this method, the object should be added to Pool.
        // If the object is in the pool, it should be invisible
        public void Recycle(PrimitiveType primitiveType, GameObject element)
        {
            //Adding the object to the pool and deactivates it
            pooling[primitiveType].Add(element);
            element.SetActive(false);

            NotifyPoolStatus(false);
        }

        // QUESTION 9: We want to notify how many objects are spawned and the number of items in the pool.
        // SendMessage or Static Methods to notify the status to the GameManager, is not allowed.
        private void NotifyPoolStatus(bool spawn)
        {
            if (spawn)
                gm.spawned++;
            else
                gm.recicled++;

        }
    }
}