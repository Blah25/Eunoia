using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaAssessmentAnswer
    {
        public string AnsDetailId { get; set; }
        public int AssessmentId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionScore { get; set; }

        public virtual EunoiaAssessment Assessment { get; set; }
        public virtual EunoiaQuestion Question { get; set; }
    }
}
