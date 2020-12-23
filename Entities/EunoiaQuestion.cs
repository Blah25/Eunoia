using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaQuestion
    {
        public EunoiaQuestion()
        {
            EunoiaAssessmentAnswer = new HashSet<EunoiaAssessmentAnswer>();
        }

        public int QuestionId { get; set; }
        public int MoodId { get; set; }
        public string QuestionDesc { get; set; }

        public virtual EunoiaMood Mood { get; set; }
        public virtual EunoiaQuestionMapping EunoiaQuestionMapping { get; set; }
        public virtual ICollection<EunoiaAssessmentAnswer> EunoiaAssessmentAnswer { get; set; }
    }
}
