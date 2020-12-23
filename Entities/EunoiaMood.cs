using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaMood
    {
        public EunoiaMood()
        {
            EunoiaAssessmentResult = new HashSet<EunoiaAssessmentResult>();
            EunoiaInterpretation = new HashSet<EunoiaInterpretation>();
            EunoiaQuestion = new HashSet<EunoiaQuestion>();
        }

        public int MoodId { get; set; }
        public string MoodName { get; set; }

        public virtual ICollection<EunoiaAssessmentResult> EunoiaAssessmentResult { get; set; }
        public virtual ICollection<EunoiaInterpretation> EunoiaInterpretation { get; set; }
        public virtual ICollection<EunoiaQuestion> EunoiaQuestion { get; set; }
    }
}
