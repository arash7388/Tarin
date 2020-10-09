using System;
using System.Linq;
using System.Web.UI.WebControls;
using Common;
using Repository.DAL;
using Repository.Entity.Domain;

namespace AdminPanel
{
    public partial class Post : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    PostRepository repo = new PostRepository();
                    var tobeEditedPost = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    txtCode.Text = tobeEditedPost.Code;
                    txtTitle.Text = tobeEditedPost.Title;
                    RadEditorContext.Content = tobeEditedPost.Context;
                    imgPost.ImageUrl = Utility.GetImgSource(tobeEditedPost.Image);
                    BindTreeView(Request.QueryString["Id"].ToSafeInt());
                }
                else
                {
                    BindTreeView(-1);
                }
            }
        }

        private void BindTreeView(int postId)
        {
            var source = new TagRepository().GetAllTagsForPost(postId);

            foreach (TagRepository.TagHelper item in source)
            {
                var node = new TreeNode(item.Name);

                if (item.PostId.ToSafeString() != string.Empty)
                    node.Checked = true;

                treeViewTags.Nodes.Add(node);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txtCode.Text)) throw new LocalException("Code is empty", "کد محتوا را وارد نمایید");
                if (string.IsNullOrEmpty(txtTitle.Text)) throw new LocalException("Title is empty", "عنوان محتوا را وارد نمایید");
                if (RadEditorContext.Text.ToSafeString()=="") throw new LocalException("Context is empty", "متن محتوا را وارد نمایید");

                if (fileUploadControl.FileBytes.Length>0 && !ImageIsValid()) return;

                UnitOfWork uow = new UnitOfWork();

                ActionResult postTagResult = new ActionResult();

                if (Request.QueryString["Id"] == null)
                {
                    var newPost = new Repository.Entity.Domain.Post();

                    newPost.Code = txtCode.Text;
                    newPost.Title = txtTitle.Text;
                    newPost.Image = fileUploadControl.FileBytes;
                    newPost.Context = RadEditorContext.Text;
                    newPost.UserId = ((User)Session["UserName"]).Id; //attention : session time should be long
                    uow.Posts.Create(newPost);

                    if (uow.SaveChanges().IsSuccess)
                    {
                        if (SavePostTags(newPost.Id, treeViewTags.Nodes, uow).IsSuccess)
                            if (uow.SaveChanges().IsSuccess)
                            {
                                lblResult.Text = "اطلاعات با موفقیت ذخیره شد";
                                ClearControls();
                            }
                    }
                }
                else
                {
                    var repo = uow.Posts;
                    var tobeEditedPost = repo.GetById(Request.QueryString["Id"].ToSafeInt());

                    tobeEditedPost.Code = txtCode.Text;
                    tobeEditedPost.Title = txtTitle.Text;
                    tobeEditedPost.Image = fileUploadControl.FileBytes;
                    tobeEditedPost.Context = RadEditorContext.Content;
                    postTagResult = SavePostTags(Request.QueryString["Id"].ToSafeInt(), treeViewTags.Nodes, uow);
                  
                    if (postTagResult.IsSuccess && uow.SaveChanges().IsSuccess)
                    {
                        lblResult.Text = "اطلاعات با موفقیت ذخیره شد";
                        ClearControls();
                    }
                    
                }
            }
            catch (LocalException ex)
            {
                lblResult.Text = ex.ResultMessage;
            }
        }

        public ActionResult SavePostTags(int postId, TreeNodeCollection treeNodeCollection,UnitOfWork uow)
        {
            ActionResult result = new ActionResult();

            try
            {
                var thisPostTags = new TagRepository().GetAllTagsForPost(postId);
                var allTags = new TagRepository().GetAll();

                foreach (TreeNode node in treeNodeCollection)
                {
                    var tag = allTags.SingleOrDefault(a => a.Name == node.Text);

                    TagRepository.TagHelper relatedItem = null;

                    if (thisPostTags.Any())
                        relatedItem = thisPostTags.SingleOrDefault(a => a.PostId == postId && a.TagId == tag.Id);

                    if (node.Checked)
                    {
                        if (relatedItem == null)
                            uow.TagPosts.Create(new TagPost()
                            {
                                PostId = postId,
                                TagId = tag.Id
                            });
                    }
                    else
                    {
                        if (thisPostTags.Any())
                        {
                            var tobeDeleted = thisPostTags.SingleOrDefault(a => a.PostId == postId && a.TagId == tag.Id);

                            if (relatedItem != null)
                                uow.TagPosts.Delete(a=>a.TagId==tobeDeleted.TagId && a.PostId==tobeDeleted.PostId);
                        }
                    }
                }

                result.IsSuccess = true;
            }
            catch (LocalException exception)
            {
                //result.IsSuccess = false;
                //result.Message = exception.ResultMessage;
                //Debuging.Warning(exception, "SavePostTags Method");
                lblResult.Text = exception.Message;
            }
            catch (Exception exception)
            {
                //result.IsSuccess = false;
                //result.Message = MessageText.UNKNOWN_ERROR;
                //Debuging.Error(exception, "SavePostTags Method");
                lblResult.Text = exception.Message;
            }

            return result;
        }

        private void ClearControls()
        {
            txtCode.Text = "";
            txtTitle.Text = "";
            RadEditorContext.Content = "";
            foreach (TreeNode treeNode in treeViewTags.Nodes)
            {
                treeNode.Checked = false;
            }
        }

        private bool ImageIsValid()
        {
            bool result = false;

            if (fileUploadControl.HasFile)
            {
                try
                {
                    if (fileUploadControl.PostedFile.ContentType == "image/jpeg" || fileUploadControl.PostedFile.ContentType == "image/png")
                    {
                        if (fileUploadControl.PostedFile.ContentLength < 52000)
                        {
                            //string filename = Path.GetFileName(FileUploadControl.FileName);
                            //FileUploadControl.SaveAs(Server.MapPath("~/") + filename);
                            //imgPath = StatusLabel.Text = filename;
                            result = true;
                        }
                        else
                            statusLabel.Text = "حجم تصویر باید کمتر از 50 کیلوبایت باشد ";
                    }
                    else
                        statusLabel.Text = "فرمت عکس باید Jpeg/png باشد";
                }
                catch (Exception ex)
                {
                    statusLabel.Text = "خطا در  ذخیره تصویر";
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

    }
}