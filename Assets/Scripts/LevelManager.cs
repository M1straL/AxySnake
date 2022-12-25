using UnityEngine;
using UnityEngine.Pool;

namespace DefaultNamespace
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager instance = null;
        private PooledObject<Food> _foodPool; 

        private void Awake () {

            if (instance == null) {
                instance = this;
            } else if(instance == this){
                Destroy(gameObject); 
            }
        
            DontDestroyOnLoad(gameObject);

            InitializeLevel();
        }

        private void InitializeLevel()
        {
            //Instantiate Enemies
            FillTheLevelWithFood();
            //Instantiate Blocks
        }

        private void FillTheLevelWithFood()
        {
            
        }
    }
}