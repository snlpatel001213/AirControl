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
            DB_Transactions outputSchema = connection.Table<DB_Transactions>().Where(x => x.MsgType == "Transcation").FirstOrDefault();
            // get all the thing in the table
            string output = JsonConvert.SerializeObject(outputSchema);
            return output;
        }

        // /// <summary>
        // /// Direct trigger handles all those action which does not require to be put in update loop
        // /// </summary>
        // public static void  TriggerSendMessage(SQLiteConnection connection)
        // {
        //     DB_Transactions outputSchema = connection.Table<DB_Transactions>().Where(x => x.MsgType == "Transcation").FirstOrDefault();
        //     // get all the thing in the table
        //     string output = JsonConvert.SerializeObject(outputSchema);
                    
        // }
    }

}
