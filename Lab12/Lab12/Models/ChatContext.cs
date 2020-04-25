using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lab12.Models
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base("ChatContext")
        {
        }

        public static ChatContext Create()
        {
            return new ChatContext();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}