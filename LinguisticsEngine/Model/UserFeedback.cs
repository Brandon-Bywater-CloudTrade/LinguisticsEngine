using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticsEngine.Model
{
    public class UserFeedback
    {
        public User User { get; set; } = new();
        public List<Feedback> Feedbacks { get; set; } = new();
    }
}
