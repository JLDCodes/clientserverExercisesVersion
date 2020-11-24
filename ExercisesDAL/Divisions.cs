using System;
using System.Collections.Generic;

#nullable disable

namespace ExercisesDAL
{
    public partial class Divisions : SchoolEntity
    {
        public Divisions()
        {
            Students = new HashSet<Students>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Timer { get; set; }

        public virtual ICollection<Students> Students { get; set; }
    }
}
