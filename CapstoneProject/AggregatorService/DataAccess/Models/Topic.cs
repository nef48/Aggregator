using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AggregatorService.DataAccess.Models
{
    public class Topic
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }

        public Topic()
        {
            TopicName = string.Empty;
        }

        public Topic(Topic topic)
        {
            TopicID = topic.TopicID;
            TopicName = topic.TopicName;
        }
    }
}
