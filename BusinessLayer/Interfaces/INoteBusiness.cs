using Microsoft.AspNetCore.Http;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface INoteBusiness
    {
        NoteEntity AddNotes(NotesModel notesModel, int UserId);

        List<NoteEntity> GetAllNotes(int Userid);

        NoteEntity UpdateNote(int noteid, NotesModel notesModel, int Userid);

        bool IsPinOrNot(int noteid,int Userid);

        bool IsArchieveOrNot(int noteid, int Userid);

        bool IsTrashorNot(int noteid, int Userid);

        bool IsColor(int noteid, int Userid, string UpdateColor);

        bool DeleteForever(int noteid, int Userid);

        string UploadImage(int noteid, int Userid, IFormFile img);

        NoteEntity UpdateRemainder(int noteid, DateTime updateRemainder, int userid);

        NoteEntity getNote(int Userid,int noteid);

        NoteEntity GetNotebydate(DateTime createdat);




    }
}