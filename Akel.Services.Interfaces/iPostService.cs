using Akel.Domain.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Akel.Services.Interfaces
{
    public interface iPostService
    {
        Task<IEnumerable<Post>> Get();
        Task<IEnumerable<Post>> GetByUser(Guid id);
        Task<Post> GetById(Guid id);
        Task<Post> Create(Post post);
        Task<Photo> AddPhoto(Photo photo);
        Task<Post> Like(Like like);
        Task Update(Post post);
        Task<Post> Delete(Guid id);
        Task Save();
    }
}
