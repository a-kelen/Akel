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
    public class SubscriberService : iSubscriberService
    {
        private readonly iUnitOfWork _context;
        public SubscriberService(iUnitOfWork unit)
        {
            _context = unit;
        }

        public async Task<Subscriber> Create(Subscriber subscriber)
        {
            //await _context.Subscribers.Create(subscriber);
            return  subscriber;
        }

        public async Task<Subscriber> Delete(Guid id)
        {
            var subscriber = await _context.Subscribers.Get(id);
            if (subscriber == null)
            {
                return null;
            }

            await _context.Subscribers.Delete(subscriber.Id);
            return subscriber;
        }

        public async Task<IEnumerable<Subscriber>> Get()
        {
            return await _context.Subscribers.GetAll();
        }

        public async Task<Subscriber> GetById(Guid id)
        {
            return await _context.Subscribers.Get(id);
        }

        public async Task<IEnumerable<Subscriber>> GetByUser(Guid id)
        {
            var res = (await _context.Subscribers.GetAll()).Where(x => x.UserProfileId == id);
            return res;
        }

        public async  Task Save()
        {
            await _context.Save();
        }

        public async  Task Update(Subscriber subscriber)
        {
            await _context.Subscribers.Update(subscriber);
        }
    }
}
