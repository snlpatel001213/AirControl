using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using SqliteDB;

namespace AirControl
{
    public interface IAirplaneUI
    {
        #region Variables
        #endregion

        #region Builtin Methods
        #endregion

        #region Custom Methods
        void HandleAirplaneUI(SQLiteConnection connection);
        #endregion
    }

}
