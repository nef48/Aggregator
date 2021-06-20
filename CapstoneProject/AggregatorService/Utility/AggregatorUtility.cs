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
        private const string CONNECT_STRING_NAME = "MySQLConnectionString";

        #region Topic Methods

        public static List<Topic> GetAllTopics()
        {
            List<Topic> allTopics = new List<Topic>();
            //string connectionString = "Server=localhost;Port=3307;Database=Aggregator;Uid=root;Pwd=password;charset=utf8;";//ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;
            //string connectionString = config.GetConnectionString(CONNECT_STRING_NAME);
            //string connectionString = ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB())
                {
                    allTopics = (from topics in db.Topics select topics).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Topic>();
            }

            return allTopics;
        }

        public static List<Topic> GetTopicsForUser(int userID)
        {
            List<Topic> topicsForUser = new List<Topic>();
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB())
                {
                    topicsForUser = (from topics in db.Topics
                                     join userTopics in db.Usertopics on topics.TopicID equals userTopics.TopicID
                                     where userTopics.UserID == userID
                                     select topics).ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Topic>();
            }

            return topicsForUser;
        }

        public static Topic GetTopic(int topicID)
        {
            Topic topic = null;
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB(connectionString))
                {
                    topic = db.Topics.Where(x => x.TopicID == topicID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return topic;
        }

        public static bool AddTopicToUser(int topicID, int userID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB(connectionString))
                {
                    Usertopic userTopic = new Usertopic()
                    {
                        UserID = userID,
                        TopicID = topicID
                    };

                    db.InsertWithInt32IdentityAsync(userTopic);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region User Methods

        public static bool AddUserToDatabase(Userdata userData)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB(connectionString))
                {
                    db.InsertWithInt32Identity(userData);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static Userdata GetUser(int userID)
        {
            Userdata user = null;
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB(connectionString))
                {
                    user = db.Userdatas.Where(x => x.UserID == userID).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return user;
        }

        #endregion
    }
}
