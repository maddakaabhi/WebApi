using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ICollaboratorBusiness
    {
        CollaboratorEntity AddCollaborator(string email, int Userid, int noteid);

        List<string> GetCollaboratories(int noteid, int Userid);

        bool deletecollaborator(int collaboratorid, int noteid, int Userid);
    }
}