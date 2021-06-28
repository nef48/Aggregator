using AggregatorController.DataAccess;
using DataModels;
using LinqToDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorController
{
    public static class AggregatorUtility
    {
        #region Topic Methods

        public static List<Topic> GetAllTopics(ResultsContext dbContext)
        {
            List<Topic> allTopics = new List<Topic>();

            try
            {
                //using (AggregatorDB db = new AggregatorDB())
                //{
                //    allTopics = (from topics in db.Topics select topics).ToList();
                //}
                allTopics = dbContext.Topic.ToList();
            }
            catch (Exception ex)
            {
                return new List<Topic>();
            }

            return allTopics;
        }

        public static List<Topic> GetTopicsForUser(ResultsContext dbContext, int userID)
        {
            List<Topic> topicsForUser = new List<Topic>();

            try
            {
                topicsForUser = dbContext.Usertopic.Where(x => x.UserID == userID).Select(x => x.Topic).ToList();
            }
            catch (Exception ex)
            {
                return new List<Topic>();
            }

            return topicsForUser;
        }

        public static Topic GetTopic(ResultsContext dbContext, int topicID)
        {
            Topic topic = null;

            try
            {
                topic = dbContext.Topic.Where(x => x.TopicID == topicID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }

            return topic;
        }

        public static bool AddTopicToUser(ResultsContext dbContext, int topicID, int userID)
        {

            try
            {
                Usertopic userTopic = new Usertopic()
                {
                    UserID = userID,
                    TopicID = topicID
                };

                dbContext.Usertopic.Add(userTopic);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region User Methods

        public static bool AddUserToDatabase(ResultsContext dbContext, Userdata userData)
        {

            try
            {
                dbContext.Userdata.Add(userData);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static Userdata GetUser(ResultsContext dbContext, int userID)
        {
            Userdata user = null;

            try
            {
                user = dbContext.Userdata.Where(x => x.UserID == userID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }

            return user;
        }

        public static LoginObject GetUserAndTopics(ResultsContext dbContext, string username, string password)
        {
            LoginObject userAndTopics = new LoginObject();

            try
            {
                Userdata user = dbContext.Userdata
                    .Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();

                userAndTopics.User = user;
                userAndTopics.Topics = dbContext.Usertopic.Where(x => x.UserID == user.UserID).Select(x => x.Topic).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

            return userAndTopics;
        }

        #endregion
    }
}
