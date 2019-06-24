using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurtleEnglish.Models
{
    public class StatisticsClass
    {
        public class LevelStudyStatistics
        {
            public string level;
            public int sum;
            public int percent;
            public string color;
        }

        public class VocabularyLevelStatistics
        {
            public string level;
            public int sum;
            public int percent;
        }

        public class FeedbackStatistics
        {
            public string feedbackName;
            public int sum;
        }
    }
}