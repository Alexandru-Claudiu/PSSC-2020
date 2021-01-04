using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionReadContext
    {
        public ICollection<CreateQuestionCommand> Questions { get; }
        public QuestionReadContext(ICollection<CreateQuestionCommand> questions)
        {
            Questions = questions;
        }
    }
}
