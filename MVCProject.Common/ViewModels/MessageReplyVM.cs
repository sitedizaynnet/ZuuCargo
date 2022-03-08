using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCProject.Common.ViewModels
{
    public class MessageReplyVM
    {
        public string UserId { get; set; }

        private List<MessageReply> _replies = new List<MessageReply>();
        public ReplyVM Reply { get; set; }

        public MessageVM Message { get; set; }

        public List<MessageReply> Replies
        {
            get { return _replies; }
            set { _replies = value; }
        }

        public int OrderId { get; set; }
        public PagedList.IPagedList<MessageVM> Messages { get; set; }

        public class MessageReply
        {
            public int Id { get; set; }
            public int MessageId { get; set; }
            public string MessageDetails { get; set; }
            public string ReplyFrom { get; set; }

            public string ReplyMessage { get; set; }
            public DateTime ReplyDateTime { get; set; }
        }


    }
}
