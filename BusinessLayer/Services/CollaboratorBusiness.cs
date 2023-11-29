using BusinessLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollaboratorBusiness : ICollaboratorBusiness
    {
        private readonly ICollaboratorRepo collaboratorRepo;
        public CollaboratorBusiness(ICollaboratorRepo collaboratorRepo)
        {
            this.collaboratorRepo = collaboratorRepo;
        }

        public CollaboratorEntity AddCollaborator(string email, int Userid, int noteid)
        {
            return collaboratorRepo.AddCollaborator(email, Userid, noteid);
        }

        public List<string> GetCollaboratories(int noteid, int Userid)
        {
            return collaboratorRepo.GetCollaboratories(noteid, Userid);
        }

        public bool deletecollaborator(int collaboratorid, int noteid, int Userid)
        {
            return collaboratorRepo.deletecollaborator(collaboratorid, noteid, Userid);
        }
    }
}
