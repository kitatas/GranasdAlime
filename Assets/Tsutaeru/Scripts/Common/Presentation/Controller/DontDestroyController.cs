using UnityEngine;

namespace Tsutaeru.Common.Presentation.Controller
{
    public sealed class DontDestroyController : MonoBehaviour
    {
        private static DontDestroyController _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}