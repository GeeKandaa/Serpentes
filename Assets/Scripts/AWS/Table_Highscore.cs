using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace Assets.Scripts
{
    [DynamoDBTable("ShitSnake_Highscore")]
    public class Table_Highscore
    {
        [DynamoDBHashKey] //HashKey
        public string Player_ID { get; set; }

        [DynamoDBProperty]
        public int Highscore { get; set; }

        [DynamoDBProperty]
        public string Username { get; set; }
    }
}