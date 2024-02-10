using SomeBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeBot.Services
{
    public interface IStorage
    {
        Session GetSession(long chatId);
    }
}
