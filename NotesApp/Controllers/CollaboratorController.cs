using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using ModelLayer.Models;
using System.Collections.Generic;

namespace NotesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBusiness collaboratorBusiness;
        public CollaboratorController(ICollaboratorBusiness collaboratorBusiness)
        {
            this.collaboratorBusiness = collaboratorBusiness;
        }

        [HttpPost]
        [Route("addcollaborator")]
        public ActionResult Addcollaborator(string email,int noteid)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result= collaboratorBusiness.AddCollaborator(email,Userid,noteid);
            if(result != null)
            {
                return Ok(new ResponseModel<CollaboratorEntity> { Success = true, Message = "Collaborator added successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<CollaboratorEntity> { Success = false, Message = "Collaborator not added"});
            }
        }

        [HttpGet]
        [Route("getcollaboratoremails")]
        public List<string> GetCollaboratories(int noteid)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result=collaboratorBusiness.GetCollaboratories(noteid,Userid);
            if(result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("deletecolloboratorymail")]

        public ActionResult DeleteCollaboratory(int collaboratorid,int noteid)
        {
            int Userid= int.Parse(User.FindFirst("Userid").Value);
            var result=collaboratorBusiness.deletecollaborator(collaboratorid,noteid,Userid);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Collaborator deleted successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<bool> { Success = false, Message = "Collaborator not found"});
            }
        }
       
    }
}
