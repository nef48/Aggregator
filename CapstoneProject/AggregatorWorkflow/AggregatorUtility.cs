using DataModels;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregatorWorkflow
{
    public static class AggregatorUtility
    {
        private const string CONNECT_STRING_NAME = "AggregatorDatabase";

        #region Topic Methods

        public static List<Topic> GetAllTopics()
        {
            List<Topic> allTopics = new List<Topic>();
            string connectionString = "Server=localhost;Port=3307;Database=Aggregator;Uid=root;Pwd=password;charset=utf8;";//ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB(connectionString))
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

        public static bool AddTopicToUser(int topicID, int UserID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[CONNECT_STRING_NAME].ConnectionString;

            try
            {
                using (AggregatorDB db = new AggregatorDB(connectionString))
                {
                    Usertopic userTopic = new Usertopic()
                    {
                        UserID = UserID,
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
