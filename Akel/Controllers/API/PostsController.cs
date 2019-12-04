using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Akel.Domain.Core;
using Akel.Infrastructure.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Akel.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly UnitOfWork _context;
        private IHostingEnvironment _appEnvironment;
        public PostsController(ApplContext context, IHostingEnvironment appEnvironment)
        {
            _context = new UnitOfWork();
            _appEnvironment = appEnvironment;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return Ok( await _context.Posts.GetAll());
        }
        [HttpGet("byuser/{id}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts(Guid id)
        {
            var subs = (await _context.Subscribers.GetAll()).Where(x => x.UserProfileId == id).Select(x => x.AuditionId);
            var res = (await _context.Posts.GetAll()).Where(x =>  subs.Any(t => t == x.AuditionId));
            return Ok(res);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            var post = await _context.Posts.Get(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }
        [HttpGet("photo/{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(Guid id)
        {
            var photo = await _context.Photos.Get(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(Guid id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            await _context.Posts.Update(post);

            try
            {
                await _context.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        public class CreatePostVM
        {
            public Guid AuditionId { get; set; }
            public Guid PhotoId { get; set; }
            public string Text { get; set; }
            public int Likes { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(CreatePostVM vm)
        {


            Post post = new Post
            {
                Text = vm.Text,
                AuditionId = vm.AuditionId,
                LikesCount = vm.Likes,
                PhotoId = vm.PhotoId
            };
            await _context.Posts.Create(post);
            await _context.Save();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }
        public class LikeVM
        {
            public Guid UserProfileId { get; set; }
            public Guid PostId { get; set; }
        }
        [HttpPost("like")]
        public async Task<ActionResult> PostLike(LikeVM l)
        {
            Like vm = new Like { PostId = l.PostId, UserProfileId = l.UserProfileId };
            var ex = (await _context.Likes.GetAll()).FirstOrDefault(x => x.UserProfileId == vm.UserProfileId && x.PostId == vm.PostId);
            Post post = (await _context.Posts.GetAll()).FirstOrDefault(x => x.Id == l.PostId);
            if(ex == null)
            {
                post.LikesCount++;
                await _context.Likes.Create(vm);
                await _context.Posts.Update(post);
                await _context.Save();
                return Ok( new { result = true , id = vm.PostId , likes = post.LikesCount });
            } else
            {
                post.LikesCount--;
                await _context.Likes.Delete(ex.Id);
                await _context.Posts.Update(post);
                await _context.Save();
                return Ok(new { result = false, id = vm.PostId , likes = post.LikesCount });
            }
            

          
        }
        [HttpPost("addphoto")]
        public async Task<ActionResult<Photo>> AddPhoto(IFormFile file)
        {

                string path = "/photos/" + Guid.NewGuid().ToString()+"_"+ Request.Form.Files[0].FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await Request.Form.Files[0].CopyToAsync(fileStream);
                }
                Photo photo = new Photo { Name = Request.Form.Files[0].FileName, Path = path };
                await _context.Photos.Create(photo);
                await _context.Save();

            return CreatedAtAction("GetPhoto", new { id = photo.Id }, photo);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> DeletePost(Guid id)
        {
            var post = await _context.Posts.Get(id);
            if (post == null)
            {
                return NotFound();
            }

            await _context.Posts.Delete(post.Id);
            await _context.Save();

            return post;
        }

        private bool PostExists(Guid id)
        {
            return _context.Posts.Get(id) == null;
        }
    }
}
