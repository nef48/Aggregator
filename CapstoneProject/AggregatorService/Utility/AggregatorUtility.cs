using AggregatorController.DataAccess;
using AggregatorController.DataAccess.DataModels;
using DataModels;
using LinqToDB;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

        private static Userdata GetUser(ResultsContext dbContext, string username, string password)
        {
            Userdata user = null;

            try
            {
                user = dbContext.Userdata.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
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
                Userdata user = GetUser(dbContext, username, password);

                user.LastLogin = DateTime.Now;
                dbContext.SaveChanges();

                userAndTopics.User = user;
                userAndTopics.Topics = dbContext.Usertopic.Where(x => x.UserID == user.UserID).Select(x => x.Topic).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

            return userAndTopics;
        }

        public static string ResetPassword(ResultsContext dbContext, string username, string oldPassword, string newPassword)
        {
            try
            {
                Userdata user = GetUser(dbContext, username, oldPassword);

                if (user == null)
                {
                    return "Old password is incorrect";
                }

                user.Password = newPassword;
                dbContext.SaveChanges();

                return "Password successfully changed";
            }
            catch (Exception ex)
            {
                return "Error occurred while trying to update password.";
            }
        }
        
        #endregion

        #region News Feed Methods

        public static List<rssChannelItem> GetNewsFeed(ResultsContext dbContext, Topic topic)
        {
            List<string> searchTopics = dbContext.Topicmap.Where(x => x.MapFrom.ToLower() == topic.TopicName.ToLower())
                                                .Select(x => x.MapTo).ToList();

            List<rssChannelItem> items = new List<rssChannelItem>();

            foreach (string topicName in searchTopics)
            {
                string urlString = $"https://rss.nytimes.com/services/xml/rss/nyt/{topicName}.xml";

                using (XmlTextReader reader = new XmlTextReader(urlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(rss));
                    rss feed = (rss)serializer.Deserialize(reader);
                    items.AddRange(feed.channel.item);
                }
            }

            return items;
        }

        public static bool AddArticleToFavorites(ResultsContext dbContext, int userID, Article article)
        {
            Userarticle articleToAdd = new Userarticle()
            {
                UserID = userID,
                Article = article,
                IsFavorited = true
            };

            try
            {
                dbContext.Userarticle.Add(articleToAdd);
                dbContext.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static List<Article> GetFavoriteArticles(ResultsContext dbContext, int userID)
        {
            List<Article> articleList = new List<Article>();

            try
            {
                articleList = dbContext.Userarticle.Where(x => x.UserID == userID).Select(x => x.Article).ToList();
            }
            catch
            {
                return null;
            }

            return articleList;
        }

        #endregion
    }
}
