using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface ICollaboratorRepo
    {
        CollaboratorEntity AddCollaborator(string email, int Userid, int noteid);
        List<string> GetCollaboratories(int noteid, int Userid);

        bool deletecollaborator(int collaboratorid, int noteid, int Userid);
    }
}