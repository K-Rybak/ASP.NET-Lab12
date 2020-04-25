using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab12.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int SenderId { get; set; }
        public int ReciverId { get; set; }
        public string TitleMessage { get; set; }
        public string TextMessage { get; set; }
        public DateTime CreateAt { get; set; }
    }
}