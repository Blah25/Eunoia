using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaInterpretation
    {
        public EunoiaInterpretation()
        {
            EunoiaAssessmentResult = new HashSet<EunoiaAssessmentResult>();
        }

        public string InterpreId { get; set; }
        public int MoodId { get; set; }
        public int? ScoreFrom { get; set; }
        public int? ScoreTo { get; set; }
        public int SeverityId { get; set; }
        public string Description { get; set; }
        public string Tips { get; set; }
        public string Quote { get; set; }

        public virtual EunoiaMood Mood { get; set; }
        public virtual EunoiaSeverity Severity { get; set; }
        public virtual ICollection<EunoiaAssessmentResult> EunoiaAssessmentResult { get; set; }
    }
}
