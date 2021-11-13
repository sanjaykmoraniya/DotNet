﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SkmWebApi.Model
{
    public class TodoItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}