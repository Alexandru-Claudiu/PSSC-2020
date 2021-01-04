using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion
{
    public class CreateQuestionCommand
    {
        [Required]
        public int QuestionId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Tags { get; set; }
        public CreateQuestionCommand() { }
        public CreateQuestionCommand(int questionId, string title, string tags)
        {
            QuestionId = questionId;
            Title = title;
            Tags = tags;
        }
    }
}
