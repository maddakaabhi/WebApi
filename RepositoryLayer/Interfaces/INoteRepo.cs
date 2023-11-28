using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRepo
    {
        NoteEntity AddNotes(NotesModel noteModel, int UserId);

        List<NoteEntity> GetAllNotes(int Userid);

        NoteEntity UpdateNote(int noteid, NotesModel notesModel, int Userid);

        bool IsPinOrNot(int noteid,int Userid);

        bool IsArchieveOrNot(int noteid, int Userid);

        bool IsTrashorNot(int noteid, int Userid);

        bool IsColor(int noteid,int Userid,string UpdateColor);

        bool DeleteForever(int noteid, int Userid);
    }
}