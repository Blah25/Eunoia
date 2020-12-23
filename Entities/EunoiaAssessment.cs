using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaAssessment
    {
        public EunoiaAssessment()
        {
            EunoiaAssessmentAnswer = new HashSet<EunoiaAssessmentAnswer>();
            EunoiaAssessmentResult = new HashSet<EunoiaAssessmentResult>();
        }

        public int AssessmentId { get; set; }
        public string RespondentId { get; set; }
        public DateTime AssessmentDate { get; set; }

        public virtual EunoiaRespondent Respondent { get; set; }
        public virtual ICollection<EunoiaAssessmentAnswer> EunoiaAssessmentAnswer { get; set; }
        public virtual ICollection<EunoiaAssessmentResult> EunoiaAssessmentResult { get; set; }
    }
}
