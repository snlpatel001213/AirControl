using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Communicator;

namespace AirControl
{
    /// <summary>
    /// UI element controller
    /// </summary>
    public class AC_AirplaneUI_Controller : MonoBehaviour
    {
        #region Variables
        public List<IAirplaneUI> instruments =  new List<IAirplaneUI>();
        private bool currentVisibility = true;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            instruments = transform.GetComponentsInChildren<IAirplaneUI>().ToList<IAirplaneUI>();
        }

        // Update is called once per frame
        void Update()
        {
            #region IOSwitch
            bool showUIElements = StaticUISchema.ShowUIElements;
            bool isActive  =  StaticUISchema.IsActive;
            if(isActive)
            {
                transform.gameObject.SetActive(showUIElements);
            }
            #endregion

            if(instruments.Count>0)
            {
                foreach(IAirplaneUI instrument in instruments){
                    instrument.HandleAirplaneUI();
                }
            }
        }
        #endregion

        #region Custom Methods
        #endregion
    }

    
}
