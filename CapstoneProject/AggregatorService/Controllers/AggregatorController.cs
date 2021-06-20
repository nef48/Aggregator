using AggregatorController.DataAccess;
using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorController
{
    [ApiController]
    [Route("[controller]")]
    public class AggregatorController : ControllerBase
    {
        private ResultsContext _restultsContext;

        public AggregatorController(ResultsContext context)
        {
            _restultsContext = context;
            //string connectionString = ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
        }

        #region Topic Methods

        [HttpGet("GetAllTopics")]
        public List<Topic> GetAllTopics()
        {
            List<Topic> topics = new List<Topic>();

            try
            {
                //topics = AggregatorUtility.GetAllTopics();
                topics = _restultsContext.Topics.ToList();
            }
            catch (Exception ex)
            {
                topics = null;
            }

            return topics;
        }

        [HttpGet("GetTopics")]
        public List<Topic> GetTopics(int userId)
        {
            List<Topic> topics = new List<Topic>();

            try
            {
                topics = AggregatorUtility.GetTopicsForUser(userId);
            }
            catch (Exception ex)
            {
                topics = null;
            }

            return topics;
        }

        [HttpGet("GetTopic")]
        public Topic GetTopic(int topicId)
        {
            Topic topic = new Topic();

            try
            {
                topic = AggregatorUtility.GetTopic(topicId);
            }
            catch (Exception ex)
            {
                topic = null;
            }

            return topic;
        }

        [HttpPost("AddTopicToUser")]
        public bool AddTopicToUser(int topicID, int userID)
        {
            bool result = false;

            try
            {
                result = AggregatorUtility.AddTopicToUser(topicID, userID);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        #endregion

        #region User Methods

        [HttpPost("AddUserToDatabase")]
        public bool AddUserToDatabase(Userdata userData)
        {
            bool result = false;

            try
            {
                result = AggregatorUtility.AddUserToDatabase(userData);
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        [HttpGet("GetUser")]
        public Userdata GetUser(int userID)
        {
            Userdata user = new Userdata();

            try
            {
                user = AggregatorUtility.GetUser(userID);
            }
            catch (Exception ex)
            {
                user = null;
            }

            return user;
        }

        #endregion
    }
}
