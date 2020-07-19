using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Assets.Scripts;

public class AWS_Setup : MonoBehaviour
{
    public Button load_from_db;
    public Button save_to_db;
    public Text resultText;
    public Text Username;
    public Text User_Highscore;
    public string cognitoID_Pool_String;

    private CognitoAWSCredentials credentials;
    private IAmazonDynamoDB _client;
    private DynamoDBContext _context;

    private List<Table_Highscore> _highscores = new List<Table_Highscore>();
    private int currentPlayerIndex;

    private DynamoDBContext Context
    {
        get
        {
            if (_context == null)
            {
                _context = new DynamoDBContext(_client);
            }
            return _context;
        }
    }

    private void LoadScores(Table_Highscore highscore)
    {
            // Fill in UI for User information
    }

    private void RecieveScoresFromAWS()
    {
        resultText.text = "\n***LoadTable***";
        Table.LoadTableAsync(_client, "ShitSnake_Highscore", (loadTableResult) =>
        {
            if (loadTableResult.Exception != null)
            {
                resultText.text += "\n failed to load table";
            }
            else
            {
                try
                {
                    var context = Context;
                    var search = context.ScanAsync<Table_Highscore>(new ScanCondition("Highscore", ScanOperator.GreaterThan, 0));
                    search.GetRemainingAsync(result =>
                    {
                        if (result.Exception == null)
                        {
                            _highscores = result.Result;
                            if (_highscores.Count > 0) LoadScores(_highscores.First());
                        }
                        else
                        {
                            Debug.LogError("Failed to get async table scan results: " + result.Exception.Message);
                        }
                    }, null);
                }
                catch (AmazonDynamoDBException exception)
                {
                    Debug.Log(string.Concat("Exception fetching characters from table: {0}", exception.Message));
                    Debug.Log(string.Concat("Error code: {0}, error type: {1}", exception.ErrorCode, exception.ErrorType));
                }
            }
        });
    }

    private void PushScoresToTable()
    {
        var new_highscores = new Table_Highscore
        {
            Player_ID = Guid.NewGuid().ToString(),
            Highscore = int.Parse(User_Highscore.text),
            Username = Username.text
        };
        Context.SaveAsync(new_highscores, (result) =>
        {
            if (result.Exception == null)
            {
                resultText.text += @"highscores saved";
            }
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;
        save_to_db.onClick.AddListener(PushScoresToTable);
        load_from_db.onClick.AddListener(RecieveScoresFromAWS);
        credentials = new CognitoAWSCredentials(cognitoID_Pool_String, RegionEndpoint.USEast2);
        credentials.GetIdentityIdAsync(delegate (AmazonCognitoIdentityResult<string> result)
        {
            if (result.Exception != null)
            {
                Debug.LogError("exception hit: " + result.Exception.Message);
            }

            var ddbClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast2);

            resultText.text += ("\n*** Retrieving table information *** \n");

            var request = new DescribeTableRequest
            {
                TableName = @"ShitSnake_Highscore"
            };

            ddbClient.DescribeTableAsync(request, (ddbresult) =>
            {
                if (result.Exception != null)
                {
                    resultText.text += result.Exception.Message;
                    Debug.Log(result.Exception);
                    return;
                }

                var response = ddbresult.Response;
                TableDescription description = response.Table;
                resultText.text += ("Name: " + description.TableName + "\n");
                resultText.text += ("# of items: " + description.ItemCount + "\n");
                resultText.text += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.ReadCapacityUnits + "\n");
                resultText.text += ("Provision Throughput (reads/sec): " + description.ProvisionedThroughput.WriteCapacityUnits + "\n");

            }, null);

            _client = ddbClient;

            RecieveScoresFromAWS();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
