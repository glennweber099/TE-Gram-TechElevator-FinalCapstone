using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApi.Models
{
    /// <summary>
    /// models a "like"
    /// </summary>
    public class Like
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PhotoId { get; set; }
    }
}
