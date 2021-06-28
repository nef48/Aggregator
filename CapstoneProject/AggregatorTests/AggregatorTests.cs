using AggregatorController.DataAccess;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace AggregatorTests
{
    [TestClass]
    public class AggregatorTests
    {
        [TestMethod]
        public void TestGetAllTopics()
        {
            List<Topic> expectedTopics = new List<Topic>(){
                new Topic(){ TopicName = "Politics", TopicID = 1},
                new Topic(){ TopicName = "Economy", TopicID = 2},
                new Topic(){ TopicName = "Sports", TopicID = 3},
                new Topic(){ TopicName = "Entertainment", TopicID = 4},
                new Topic(){ TopicName = "Health", TopicID = 5},
                new Topic(){ TopicName = "Religion", TopicID = 6},
                new Topic(){ TopicName = "Weather", TopicID = 7},
                new Topic(){ TopicName = "Technology", TopicID = 8},
                new Topic(){ TopicName = "Travel", TopicID = 9},
                new Topic(){ TopicName = "Food & Drug", TopicID = 10},
                new Topic(){ TopicName = "Education", TopicID = 11},
                new Topic(){ TopicName = "Shopping", TopicID = 12},
                new Topic(){ TopicName = "National Events", TopicID = 13},
                new Topic(){ TopicName = "World Events", TopicID = 14}
            };

            var optionsBuilder = new DbContextOptionsBuilder<ResultsContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;user id=root;password=password;database=Aggregator",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 25), ServerType.MySql));

            List<Topic> actualTopics = AggregatorController.AggregatorUtility.GetAllTopics(new ResultsContext(optionsBuilder.Options));

            Assert.AreEqual(expectedTopics.Count, actualTopics.Count);
        }

        [TestMethod]
        public void TestGetTopic()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ResultsContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;user id=root;password=password;database=Aggregator",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 25), ServerType.MySql));

            Topic expectedTopic = new Topic() { TopicID = 1, TopicName = "Politics" };
            Topic actualTopic = AggregatorController.AggregatorUtility.GetTopic(new ResultsContext(optionsBuilder.Options), 1);

            Assert.IsNotNull(actualTopic);
            Assert.AreEqual(expectedTopic.TopicName, actualTopic.TopicName);
        }

        [TestMethod]
        public void TestAddUserToDatabase()
        {
            Userdata newUser = new Userdata()
            {
                Username = "SampleUser",
                Password = "password",
                DateCreated = DateTime.Now,
                LastLogin = DateTime.Now
            };

            var optionsBuilder = new DbContextOptionsBuilder<ResultsContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;user id=root;password=password;database=Aggregator",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 25), ServerType.MySql));

            bool addResult = AggregatorController.AggregatorUtility.AddUserToDatabase(new ResultsContext(optionsBuilder.Options), newUser);

            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void TestGetUser()
        {
            Userdata newUser = new Userdata()
            {
                Username = "SampleUser",
                Password = "password",
                DateCreated = DateTime.Now,
                LastLogin = DateTime.Now
            };

            var optionsBuilder = new DbContextOptionsBuilder<ResultsContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;user id=root;password=password;database=Aggregator",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 25), ServerType.MySql));

            Userdata existingUser = AggregatorController.AggregatorUtility.GetUser(new ResultsContext(optionsBuilder.Options), 1);

            Assert.IsNotNull(existingUser);
            Assert.AreEqual(newUser.Username, existingUser.Username);
        }

        [TestMethod]
        public void TestAddTopicToUser()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ResultsContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;user id=root;password=password;database=Aggregator",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 25), ServerType.MySql));

            Userdata user = AggregatorController.AggregatorUtility.GetUser(new ResultsContext(optionsBuilder.Options), 1);
            Topic topic = AggregatorController.AggregatorUtility.GetTopic(new ResultsContext(optionsBuilder.Options), 1);

            bool result = AggregatorController.AggregatorUtility.AddTopicToUser(new ResultsContext(optionsBuilder.Options), topic.TopicID, user.UserID);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGetTopicsForUser()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ResultsContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;user id=root;password=password;database=Aggregator",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 25), ServerType.MySql));

            List<Topic> expectedTopics = new List<Topic>() { AggregatorController.AggregatorUtility.GetTopic(new ResultsContext(optionsBuilder.Options), 1) };
            List<Topic> actualTopics = AggregatorController.AggregatorUtility.GetTopicsForUser(new ResultsContext(optionsBuilder.Options), 1);

            Assert.AreEqual(expectedTopics.Count, actualTopics.Count);
        }

        [TestMethod]
        public void TestGetUserAndTopics()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ResultsContext>();
            optionsBuilder.UseMySql("server=localhost;port=3307;user id=root;password=password;database=Aggregator",
                    mySqlOptions => mySqlOptions.ServerVersion(new Version(8, 0, 25), ServerType.MySql));

            Userdata expectedUser = AggregatorController.AggregatorUtility.GetUser(new ResultsContext(optionsBuilder.Options), 1);
            List<Topic> expectedTopics = new List<Topic>() { AggregatorController.AggregatorUtility.GetTopic(new ResultsContext(optionsBuilder.Options), 1) };

            LoginObject actualLoginObject = AggregatorController.AggregatorUtility.GetUserAndTopics(new ResultsContext(optionsBuilder.Options), "SampleUser", "password");

            Assert.AreEqual(expectedUser.Username, actualLoginObject.User.Username);
            Assert.AreEqual(expectedTopics.Count, actualLoginObject.Topics.Count);
        }
    }
}
