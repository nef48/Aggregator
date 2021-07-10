using AggregatorController.DataAccess;
using DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace AggregatorController
{
    [ApiController]
    [Route("[controller]")]
    public class AggregatorController : ControllerBase
    {
        private ResultsContext _resultsContext;

        public AggregatorController(ResultsContext context)
        {
            _resultsContext = context;
        }

        #region Topic Methods

        [HttpGet("GetAllTopics")]
        public List<Topic> GetAllTopics()
        {
            List<Topic> topics = new List<Topic>();

            try
            {
                topics = AggregatorUtility.GetAllTopics(_resultsContext);
                //topics = _restultsContext.Topic.Where(x => true).ToList();
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
                topics = AggregatorUtility.GetTopicsForUser(_resultsContext, userId);
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
                topic = AggregatorUtility.GetTopic(_resultsContext, topicId);
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
                result = AggregatorUtility.AddTopicToUser(_resultsContext, topicID, userID);
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
        public bool AddUserToDatabase(string username, string password)
        {
            bool result = false;

            try
            {
                Userdata userData = new Userdata()
                { 
                    Username = username,
                    Password = password,
                    DateCreated = DateTime.Now,
                    LastLogin = DateTime.Now
                };

                result = AggregatorUtility.AddUserToDatabase(_resultsContext, userData);
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
                user = AggregatorUtility.GetUser(_resultsContext, userID);
            }
            catch (Exception ex)
            {
                user = null;
            }

            return user;
        }
 
        [HttpGet("UserLogin")]
        public LoginObject UserLogin(string username, string password)
        {
            LoginObject userAndTopics = new LoginObject();

            try
            {
                userAndTopics = AggregatorUtility.GetUserAndTopics(_resultsContext, username, password);
            }
            catch (Exception ex)
            {
                userAndTopics = null;
            }

            return userAndTopics;
        }

        #endregion

        #region Article Methods

        [HttpGet("GetArticles")]
        public List<Article> GetArticles(Topic topic)
        {
            List<Article> articles = new List<Article>();

            try
            {
                articles = AggregatorUtility.GetNewsFeed(_resultsContext, topic).Select(x => new Article()
                {
                    ArticleAuthor = x.creator,
                    ArticleDescription = x.description,
                    ArticleLink = x.link,
                    DatePublished = DateTime.Parse(x.pubDate),
                    ArticleTitle = x.title,
                    AdditionalDescription = x.description1
                }).ToList();
            }
            catch (Exception ex)
            {
                articles = null;
            }

            return articles;
        }

        #endregion
    }
}
