using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.CredentialManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Claims.Controllers
{
    [ApiController]
    [Route("/api/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly ILogger<ClaimsController> _logger;
        private readonly IAmazonDynamoDB _dynamoClient;

        public ClaimsController(ILogger<ClaimsController> logger, IAmazonDynamoDB dynamoDB)
        {
            _logger = logger;
            _dynamoClient = dynamoDB;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] ItemClaim item)
        {
            item.Date = DateTime.Now;

            try 
            {
                var record = new Dictionary<string, AttributeValue>
                {
                    { "username", new AttributeValue { S = item.Username } },
                    { "item", new AttributeValue { S = item.Item } },
                    { "summary", new AttributeValue { S = item.Summary } },
                    { "date", new AttributeValue { S = item.Date.ToString("dd.MM.yyyy HH:mm:ss") } }
                };

                var table = await _dynamoClient.PutItemAsync("claims", record);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong with DynamoDB call 911");
            }

            return Ok("Got it!");
        }
    }
}
