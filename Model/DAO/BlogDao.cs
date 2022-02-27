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
        public Blog ViewDetail(long id)
        {
            return db.Blogs.Find(id);
        }

        public long Insert(Blog blog)
        {
            if (string.IsNullOrEmpty(blog.MetaTitle))
            {
                blog.MetaTitle = StringHelper.ToUnsignString(blog.Name);
            }
            blog.CreatedDate = DateTime.Now;
            blog.ViewCount = 0;
            blog.Status = false;
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
            IQueryable<Blog> model = db.Blogs;
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
    }
}
