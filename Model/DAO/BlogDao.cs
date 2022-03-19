using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class BlogDao
    {
        WebDbContext db = null;
        public BlogDao()
        {
            db = new WebDbContext();
        }

        public bool ChangeStatus(long id)
        {
            var user = db.Blogs.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public Blog ViewDetail(long id)
        {
            return db.Blogs.Find(id);
        }
        public BlogSave ViewDetailBlogSave(long blogid,long userid)
        {
            return db.BlogSaves.Where(x=>x.UserID==userid && x.BlogID==blogid).SingleOrDefault();
        }
        public List<Blog> RecentBlog(int id)
        {
            return db.Blogs.Where(x=>x.Status==true).OrderByDescending(x=>x.CreatedDate).Take(id).ToList();
        }
        public List<Blog> ListAllByTen(string id)
        {
            return db.Blogs.Where(x => x.CreatedBy == id).ToList();
        }

        public long Insert(Blog blog)
        {
            if (string.IsNullOrEmpty(blog.MetaTitle))
            {
                blog.MetaTitle = StringHelper.ToUnsignString(blog.Name);
            }
            blog.CreatedDate = DateTime.Now;
            blog.ViewCount = 0;
            db.Blogs.Add(blog);
            db.SaveChanges();

            //Xử lý tag
            if (!string.IsNullOrEmpty(blog.Tag))
            {
                string[] tags = blog.Tag.Split(',');
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    var existedTag = this.CheckTag(tagId);

                    //insert to to tag table
                    if (!existedTag)
                    {
                        this.InsertTag(tagId, tag);
                    }

                    //insert to content tag
                    this.InsertContentTag(blog.ID, tagId);

                }
            }

            return blog.ID;
        }
        public bool ChangeBlogSave(long blogid,long userid)
        {
            var user = db.BlogSaves.Where(x=>x.BlogID==blogid&&x.UserID==userid).SingleOrDefault();
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
        public bool AddBlogSave(BlogSave entity)
        {
            db.BlogSaves.Add(entity);
            db.SaveChanges();
            return true;
        }


        public bool Update(Blog entity)
        {
            try
            {
                
                var content = db.Blogs.Find(entity.ID);
                content.Name = entity.Name;
                if (string.IsNullOrEmpty(content.MetaTitle=entity.MetaTitle))
                {
                    content.MetaTitle = StringHelper.ToUnsignString(content.Name);
                }
                else
                {
                    content.MetaTitle = entity.MetaTitle;
                }
                content.CategoryBlogID = entity.CategoryBlogID;
                content.Image = entity.Image;
                content.Description = entity.Description;
                content.Detail = entity.Detail;
                content.ModifiedDate = DateTime.Now;
                content.Status = entity.Status;
                if (!string.IsNullOrEmpty(content.Tag = entity.Tag))
                {
                    this.RemoveAllContentTag(content.ID);
                    string[] tags = content.Tag.Split(',');
                    foreach (var tag in tags)
                    {
                        var tagId = StringHelper.ToUnsignString(tag);
                        var existedTag = this.CheckTag(tagId);

                        //insert to to tag table
                        if (!existedTag)
                        {
                            this.InsertTag(tagId, tag);
                        }

                        //insert to content tag
                        this.InsertContentTag(content.ID, tagId);

                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<Blog> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Blog> model = db.Blogs.Where(x=>x.Status==true);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public IEnumerable<Blog> ListAllPagingFalse(string searchString, int page, int pageSize)
        {
            IQueryable<Blog> model = db.Blogs.Where(x => x.Status == false);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ID.ToString().Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }



        public bool Delete(int id)
        {
            try
            {
                var category = db.Blogs.Find(id);
                db.Blogs.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void RemoveAllContentTag(long contentId)
        {
            db.BlogTags.RemoveRange(db.BlogTags.Where(x => x.ContentID == contentId));
            db.SaveChanges();
        }

        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
        }

        public void InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new BlogTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.BlogTags.Add(contentTag);
            db.SaveChanges();
        }
        public bool CheckTag(string id)
        {
            return db.Tags.Count(x => x.ID == id) > 0;
        }

        public List<Tag> ListAllTag()
        {
            return db.Tags.Take(10).ToList();
        }

        public IEnumerable<Blog> ListAllByTag(string tag, int page, int pageSize)
        {
            var model = (from a in db.Blogs
                         join b in db.BlogTags
                         on a.ID equals b.ContentID
                         where b.TagID == tag
                         select new
                         {
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Image = a.Image,
                             CategoryBlogID=a.CategoryBlogID,
                             Description = a.Description,
                             CreatedDate = a.CreatedDate,
                             CreatedBy = a.CreatedBy,
                             ID = a.ID,
                             Status=a.Status

                         }).AsEnumerable().Select(x => new Blog()
                         {
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Image = x.Image,
                             CategoryBlogID=x.CategoryBlogID,
                             Description = x.Description,
                             CreatedDate = x.CreatedDate,
                             CreatedBy = x.CreatedBy,
                             ID = x.ID,
                             Status=x.Status
                         });
            return model.Where(x=>x.Status==true).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public Tag GetTag(string id)
        {
            return db.Tags.Find(id);
        }


        public Blog GetByID(long id)
        {
            var model = db.Blogs.Find(id);
            model.ViewCount++;
            db.SaveChanges();
            return model;
        }

        public List<Blog> ListRelatedContents(long contentId, int top)
        {
            var content = db.Blogs.Find(contentId);
            return db.Blogs.Where(x => x.ID != contentId && x.CategoryBlogID == content.CategoryBlogID&&x.Status==true).Take(top).ToList();
        }

        public List<Tag> ListTag(long contentId)
        {
            var model = (from a in db.Tags
                         join b in db.BlogTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag()
                         {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }
    }
}
