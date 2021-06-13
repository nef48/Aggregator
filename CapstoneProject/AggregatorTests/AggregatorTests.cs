using DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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

            List<Topic> actualTopics = AggregatorWorkflow.AggregatorUtility.GetAllTopics();

            Assert.AreEqual(expectedTopics.Count, actualTopics.Count);
            CollectionAssert.AreEqual(expectedTopics, actualTopics);
        }

        [TestMethod]
        public void TestGetTopic()
        {
            Topic expectedTopic = new Topic() { TopicID = 1, TopicName = "Politics" };
            Topic actualTopic = AggregatorWorkflow.AggregatorUtility.GetTopic(1);

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
                DateCreated = System.DateTime.Now,
                LastLogin = System.DateTime.Now
            };

            bool addResult = AggregatorWorkflow.AggregatorUtility.AddUserToDatabase(newUser);

            Assert.IsTrue(addResult);
        }

        [TestMethod]
        public void TestGetUser()
        {
            Userdata newUser = new Userdata()
            {
                Username = "SampleUser",
                Password = "password",
                DateCreated = System.DateTime.Now,
                LastLogin = System.DateTime.Now
            };

            Userdata existingUser = AggregatorWorkflow.AggregatorUtility.GetUser(1);

            Assert.IsNotNull(existingUser);
            Assert.AreEqual(newUser.Username, existingUser.Username);

        }

        [TestMethod]
        public void TestAddTopicToUser()
        {
            Userdata user = AggregatorWorkflow.AggregatorUtility.GetUser(1);
            Topic topic = AggregatorWorkflow.AggregatorUtility.GetTopic(1);

            bool result = AggregatorWorkflow.AggregatorUtility.AddTopicToUser(topic.TopicID, user.UserID);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestGetTopicsForUser()
        {
            List<Topic> expectedTopics = new List<Topic>() { AggregatorWorkflow.AggregatorUtility.GetTopic(1) };
            List<Topic> actualTopics = AggregatorWorkflow.AggregatorUtility.GetTopicsForUser(1);

            CollectionAssert.AreEqual(expectedTopics, actualTopics);
        }
    }
}
