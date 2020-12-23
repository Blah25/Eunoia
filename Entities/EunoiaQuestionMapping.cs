using System;
using System.Collections.Generic;

namespace Eunoia
{
    public partial class EunoiaQuestionMapping
    {
        public int QuestionId { get; set; }
        public string QuestionColId { get; set; }

        public virtual EunoiaQuestion Question { get; set; }
    }
}
