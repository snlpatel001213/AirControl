using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SQLite4Unity3d;
using SqliteDB;

namespace AirControl
{
    public class AC_AirplaneUI_Controller : MonoBehaviour
    {
        #region Variables
        public List<IAirplaneUI> instruments =  new List<IAirplaneUI>();
        SQLiteConnection connection = DB_Init.GetConnection();
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
            if(instruments.Count>0)
            {
                foreach(IAirplaneUI instrument in instruments){
                    instrument.HandleAirplaneUI(connection);
                }
            }
        }
        #endregion

        #region Custom Methods
        #endregion
    }

    
}
