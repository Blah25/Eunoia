using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaSeverity
    {
        public EunoiaSeverity()
        {
            EunoiaInterpretation = new HashSet<EunoiaInterpretation>();
        }

        public int SeverityId { get; set; }
        public string SeverityName { get; set; }

        public virtual ICollection<EunoiaInterpretation> EunoiaInterpretation { get; set; }
    }
}
