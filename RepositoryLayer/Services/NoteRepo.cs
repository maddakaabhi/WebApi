using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRepo : INoteRepo
    {
        private readonly IConfiguration config;
        private readonly NotesDBContext notesDBContext;
        public NoteRepo(NotesDBContext notesDBContext,IConfiguration config)
        {
            this.notesDBContext = notesDBContext;
            this.config = config;
        }

        public NoteEntity AddNotes(NotesModel noteModel, int UserId)
        {
            NoteEntity newNote =new NoteEntity();
            newNote.Title = noteModel.Title;
            newNote.Description = noteModel.Description;
            newNote.CreatedAt = DateTime.Now;
            newNote.LastUpdatedAt = DateTime.Now;
            newNote.UserId = UserId;
            notesDBContext.Notebook.Add(newNote);
            notesDBContext.SaveChanges();
            return newNote;

        }

        public List<NoteEntity> GetAllNotes(int Userid)
        {
            List<NoteEntity> list = notesDBContext.Notebook.ToList().FindAll(x=>x.UserId==Userid);
            if(list != null)
            {
                return list;
            }
            return null;
        }

        public NoteEntity UpdateNote(int noteid,NotesModel notesModel,int Userid)
        {
            var result=notesDBContext.Notebook.ToList().Find(x=>x.Id==noteid&&x.UserId==Userid);
            if(result != null)
            {
                result.Title = notesModel.Title;
                result.Description=notesModel.Description;
                result.LastUpdatedAt = DateTime.Now;
                notesDBContext.SaveChanges();
                return result;
            }
            else { return null; }
            
        }

        public bool IsPinOrNot(int noteid, int Userid)
        {

            var result = notesDBContext.Notebook.ToList().Find(x => x.Id == noteid && x.UserId == Userid);
            if (result.Pin == false)
            {
                result.Pin = true;
                notesDBContext.SaveChanges();
                return true;
            }
            else
            {
                result.Pin = false;
                notesDBContext.SaveChanges();
                return false;
            }



        }
        public bool IsArchieveOrNot(int noteid, int Userid)
        {
            var result = notesDBContext.Notebook.ToList().Find(x => x.Id == noteid&&x.UserId==Userid);
            if (result.Archieve == false)
            {
                result.Archieve = true;
                notesDBContext.SaveChanges();
                return true;
            }
            else
            {
                result.Archieve = false;
                notesDBContext.SaveChanges();
                return false;
            }
        }

        public bool IsTrashorNot(int noteid, int Userid)
        {
            var result = notesDBContext.Notebook.ToList().Find(x => x.Id == noteid && x.UserId == Userid);
            if (result.Trash == false)
            {
                result.Trash = true;
                notesDBContext.SaveChanges();
                return true;
            }
            else
            {
                result.Trash = false;
                notesDBContext.SaveChanges();
                return false;
            }

        }

        public bool IsColor(int noteid,int Userid,string UpdateColor)
        {
            var result = notesDBContext.Notebook.ToList().Find(x=>x.UserId == Userid&&x.Id==noteid);
            if (result != null)
            {
                result.Color = UpdateColor;
                notesDBContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool DeleteForever(int noteid,int Userid)
        {
            var result = notesDBContext.Notebook.ToList().Find(x=>x.Id==noteid&&x.UserId==Userid);
            if (result.Trash == true)
            {
                notesDBContext.Remove(result);
                notesDBContext.SaveChanges();
                return true;
            }
            else {
                return false;
            }
        }

        public string UploadImage(int noteid,int Userid,IFormFile img)
        {
            try
            {
                var result = notesDBContext.Notebook.FirstOrDefault(x => x.Id == noteid && x.UserId == Userid);
                if (result != null)
                {
                    Account account = new Account(
                        config["CloudinarySettings:CloudName"],
                        config["CloudinarySettings:Apikey"],
                        config["CloudinarySettings:ApiSecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    notesDBContext.SaveChanges();
                    return "Image uploaded successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NoteEntity UpdateRemainder(int noteid,DateTime updateRemainder,int userid)
        {
            var result= notesDBContext.Notebook.ToList().Find(x=>x.Id==noteid && x.UserId == userid);
            if(result != null)
            {
                result.Remainder = updateRemainder;
                notesDBContext.SaveChanges();
                return result;
            }
            else { return null; }

        }










    }
}
