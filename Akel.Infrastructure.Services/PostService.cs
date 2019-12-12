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
    public class PostService : iPostService
    {
        private readonly UnitOfWork _context;
        public PostService()
        {
            _context = new UnitOfWork();
        }

        public async Task<Photo> AddPhoto(Photo photo)
        {
            await _context.Photos.Create(photo);
            await _context.Save();
            return photo;
        }

        public async Task<Post> Create(Post post)
        {
            await _context.Posts.Create(post);
            await _context.Save();
            return post;
        }

        public async Task<Post> Delete(Guid id)
        {
            var post = await _context.Posts.Get(id);
          

            await _context.Posts.Delete(post.Id);
            await _context.Save();
            return post;
        }

        public async Task<IEnumerable<Post>> Get()
        {
            return await _context.Posts.GetAll();
        }

        public async Task<Post> GetById(Guid id)
        {
            return await _context.Posts.Get(id);
        }

        public async Task<IEnumerable<Post>> GetByUser(Guid id)
        {
            var subs = (await _context.Subscribers.GetAll()).Where(x => x.UserProfileId == id).Select(x => x.AuditionId);
            var res = (await _context.Posts.GetAll()).Where(x => subs.Any(t => t == x.AuditionId));
            return res;
        }

        public async Task<Post> Like(Like vm)
        {
            var ex = (await _context.Likes.GetAll()).FirstOrDefault(x => x.UserProfileId == vm.UserProfileId && x.PostId == vm.PostId);
            Post post = (await _context.Posts.GetAll()).FirstOrDefault(x => x.Id == vm.PostId);

            return post;
        }

        public async Task Save()
        {
            await _context.Save();
        }


        public async Task Update(Post post)
        {
            await _context.Posts.Update(post);
        }
    }
}
