using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaRespondent
    {
        public EunoiaRespondent()
        {
            EunoiaAssessment = new HashSet<EunoiaAssessment>();
        }

        public string RespondentId { get; set; }
        public string RespondentFullName { get; set; }
        public string RespondentAge { get; set; }
        public string RespondentPassHash { get; set; }
        public string RespondentEmail { get; set; }
        public string OrganizationName { get; set; }
        public string RespondentGender { get; set; }
        public string RespondentDepartment { get; set; }
        public string RespondentPosition { get; set; }

        public virtual ICollection<EunoiaAssessment> EunoiaAssessment { get; set; }
    }
}
