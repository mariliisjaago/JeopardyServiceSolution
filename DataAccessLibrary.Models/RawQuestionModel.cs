﻿using System;

namespace DataAccessLibrary.Models
{
    public class RawQuestionModel
    {
        public Guid Id { get; set; }
        public int ShowNumber { get; set; }
        public string Round { get; set; }
        public string Category { get; set; }
        public int Value { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
