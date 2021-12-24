using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using SqliteDB;
using Newtonsoft.Json;

namespace Communicator
{
    public class OutputHandle:MonoBehaviour
    {
        #region Builtin Methods
        private SQLiteConnection connection;
        #endregion
        
        public string  ParseOutput()
        {
            connection = DB_Init.GetConnection();
            DB_EternalOutput outputSchema = connection.Table<DB_EternalOutput>().Where(x => x.MsgType == "Outgoing").FirstOrDefault();
            // get all the thing in the table
            string output = JsonConvert.SerializeObject(outputSchema);
            return output;
        }
    }

}
