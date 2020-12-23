using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaAssessmentResult
    {
        public string ResultId { get; set; }
        public int AssessmentId { get; set; }
        public int MoodId { get; set; }
        public int TotalScore { get; set; }
        public string InterpreId { get; set; }

        public virtual EunoiaAssessment Assessment { get; set; }
        public virtual EunoiaInterpretation Interpre { get; set; }
        public virtual EunoiaMood Mood { get; set; }
    }
}
