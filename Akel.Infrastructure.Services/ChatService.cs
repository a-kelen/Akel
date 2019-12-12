using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using Akel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Infrastructure.Services
{
    public class ChatService : iChatService
    {
        private readonly UnitOfWork _context;
        public ChatService()
        {
            _context = new UnitOfWork();
        }

        public async Task<Chat> Create(Chat chat)
        {
            await _context.Chats.Create(chat);
            await _context.Save();
            return chat;
        }

        public async Task<Chat> Delete(Guid id)
        {
            var chat = await _context.Chats.Get(id);
            

            await _context.Chats.Delete(chat.Id);
            await _context.Save();

            return chat;
        }

        public async Task<IEnumerable<Chat>> Get()
        {
            var res = await _context.Chats.GetAll();
            return res;
        }

        public async Task<Chat> GetById(Guid id)
        {
            var chat = await _context.Chats.Get(id);
            return chat;
        }

        public async Task Save()
        {
            await _context.Save();
        }

        public async Task Update(Chat chat)
        {
            await _context.Chats.Update(chat);
        }
    }
}
