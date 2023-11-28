using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{

    public class NoteBusiness : INoteBusiness
    {
        private readonly INoteRepo noteRepo;
        public NoteBusiness(INoteRepo noteRepo)
        {
            this.noteRepo = noteRepo;
        }
        public NoteEntity AddNotes(NotesModel notesModel, int UserId)
        {
            return noteRepo.AddNotes(notesModel, UserId);
        }
        public List<NoteEntity> GetAllNotes(int Userid)
        {
            return noteRepo.GetAllNotes(Userid);
        }
        public NoteEntity UpdateNote(int noteid, NotesModel notesModel, int Userid)
        {
            return noteRepo.UpdateNote(noteid, notesModel, Userid);
        }

        public bool IsPinOrNot(int noteid,int Userid)
        {
            return noteRepo.IsPinOrNot(noteid,Userid);
        }
        public bool IsArchieveOrNot(int noteid, int Userid)
        {
            return noteRepo.IsArchieveOrNot(noteid,Userid);
        }
        public bool IsTrashorNot(int noteid, int Userid)
        {
            return noteRepo.IsTrashorNot(noteid,Userid);
        }
        public bool IsColor(int noteid, int Userid, string UpdateColor)
        {
            return noteRepo.IsColor(noteid,Userid, UpdateColor);
        }
        public bool DeleteForever(int noteid, int Userid)
        {
            return noteRepo.DeleteForever(noteid, Userid);
        }
        public string UploadImage(int noteid, int Userid, IFormFile img)
        {
            return noteRepo.UploadImage(noteid, Userid, img);
        }
        public NoteEntity UpdateRemainder(int noteid, DateTime updateRemainder, int userid)
        {
            return noteRepo.UpdateRemainder(noteid, updateRemainder, userid);
        }
    }
}
