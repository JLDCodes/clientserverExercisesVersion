﻿using System;
using System.Collections.Generic;

namespace ExercisesDAL
{
    public partial class Grades : SchoolEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int Mark { get; set; }
        public string Comments { get; set; }
        public byte[] Timer { get; set; }

        public virtual Courses Course { get; set; }
        public virtual Students Student { get; set; }
    }
}
