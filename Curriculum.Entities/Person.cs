using DevPack.Data;
using System;

namespace Curriculum.Entities
{
    public class Person : Entity
    {
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
