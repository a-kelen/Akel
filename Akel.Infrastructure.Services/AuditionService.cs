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
    public class AuditionService : iAuditionService
    {
        private readonly UnitOfWork _context;
        public AuditionService()
        {
            _context = new UnitOfWork();
        }
        public async Task<Audition> AddPhoto(Guid id, string path)
        {
            Audition a = await _context.Auditions.Get(id);
            a.Photo = path;
            await _context.Auditions.Update(a);
            await _context.Save();
            return a;
        }

        public async Task<Audition> Create(Audition audition)
        {
            await _context.Auditions.Create(audition);
            await _context.Save();
            Chat chat = new Chat();
            chat.AuditionId = audition.Id;
            await _context.Chats.Create(chat);
            await _context.Save();
            return audition;
        }

        public async Task<IEnumerable<Audition>> Get()
        {
            var res = await _context.Auditions.GetAll();
            return res;
        }

        public async Task<Audition> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Audition>> GetByOwner(Guid id)
        {
            var res = (await _context.Auditions.GetAll()).Where(x => x.UserProfileId == id);
            return res;
        }

        public async Task<Subscriber> Subscribe(Guid id, Guid userId)
        {
            Subscriber subscriber = (await _context.Subscribers.GetAll()).FirstOrDefault(x => x.AuditionId == id && x.UserProfileId == userId);
            if (subscriber == null)
            {
                subscriber = new Subscriber { AuditionId = id, UserProfileId = userId };
                await _context.Subscribers.Create(subscriber);
                await _context.Save();
                return subscriber;
            }
            else
            {
                await _context.Subscribers.Delete(subscriber.Id);
                await _context.Save();
                return null;
            }
        }

        public async Task Update(Audition audition)
        {
            await _context.Auditions.Update(audition);
        }
        public async Task Save()
        {
            await _context.Save();
        }

        public async Task<Audition> Delete(Guid id)
        {
            var audition = await _context.Auditions.Get(id);
            await _context.Auditions.Delete(audition.Id);
            await _context.Save();
            return audition;
        }
    }
}
