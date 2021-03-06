﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Access.Primitives.IO;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.EF.Models;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestion;
using LanguageExt;
using Access.Primitives.EFCore;

namespace StackUnderflow.API.AspNetCore.Controllers
{
    [Route("questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
        }

        [HttpPost("createQuestion")]
        public async Task<ActionResult> CreateQuestion([FromBody] CreateQuestionCommand cmd)
        {

            var posts = _dbContext.Post.ToList();
            QuestionWriteContext questionWriteContext = new QuestionWriteContext(
                                                        new EFList<Post>(_dbContext.Post));

            var expr = from questionResult in QuestionDomain.CreateQuestion(cmd.QuestionId, cmd.Title, cmd.Tags)
                       select questionResult;

            var result = await _interpreter.Interpret(expr, questionWriteContext, new object());

            return result.Match(
               created => Ok(created),
               notCreated => BadRequest(notCreated),
               invalidRequest => ValidationProblem());
        }
    }
}
