using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollaboratorRepo : ICollaboratorRepo
    {
        private readonly NotesDBContext notesDBContext;
        public CollaboratorRepo(NotesDBContext notesDBContext)
        {
            this.notesDBContext = notesDBContext;
        }

        public CollaboratorEntity AddCollaborator(string email, int Userid, int noteid)
        {
            var checkemmail = notesDBContext.Collaborator.ToList().Find(x => x.CollaboratorMail == email);
            if (checkemmail == null)
            {
                CollaboratorEntity collaboratorentity = new CollaboratorEntity();
                collaboratorentity.CollaboratorMail = email;
                collaboratorentity.UserId = Userid;
                collaboratorentity.NoteId = noteid;
                collaboratorentity.Createdat = DateTime.Now;
                collaboratorentity.Updatedat = DateTime.Now;
                notesDBContext.Collaborator.Add(collaboratorentity);
                notesDBContext.SaveChanges();
                return collaboratorentity;
                 
            }
            else
            {
                return null;
            }
        }

        public  List<string> GetCollaboratories(int noteid,int Userid)
        {
            var result= notesDBContext.Collaborator.ToList().FindAll(x=>x.NoteId==noteid&&x.UserId==Userid);
            List<string> list = result.Select(x=>x.CollaboratorMail).ToList();
            if(result != null)
            {
                return list;
            }
            return null;

        }

        public bool deletecollaborator(int collaboratorid,int noteid,int Userid)
        {
            var result = notesDBContext.Collaborator.FirstOrDefault(x => x.CollaboratorId == collaboratorid && x.NoteId == noteid && x.UserId == Userid);
            if(result != null)
            {
                notesDBContext.Collaborator.Remove(result);
                notesDBContext.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
