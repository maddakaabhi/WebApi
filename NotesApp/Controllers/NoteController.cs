using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace NotesApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteBusiness noteBusiness;
        public NoteController(INoteBusiness noteBusiness)
        {
            this.noteBusiness = noteBusiness;
        }

        [HttpPost]
        [Route("addnotes")]
        public ActionResult AddNotes(NotesModel notesModel)
        {
            int UserId =int.Parse( User.FindFirst("Userid").Value);
            var result = noteBusiness.AddNotes(notesModel, UserId);
            if(result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Notes Added successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = true, Message = "Notes Added successfully", Data = result });
            }

        }

        [HttpGet]
        [Route("getallnotes")]
        public List<NoteEntity> GetAllNotes()
        {
            int UserId = int.Parse(User.FindFirst("Userid").Value);
            var result = noteBusiness.GetAllNotes(UserId);
            if( result != null )
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Route("updatedata")]
        public ActionResult UpdateNote(int noteid, NotesModel notesModel)
        {
            int UserId = int.Parse(User.FindFirst("Userid").Value);
            var result=noteBusiness.UpdateNote(noteid, notesModel, UserId);
            if(result != null) 
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = $"Note of {UserId} is updated successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<NoteEntity> { Success = false, Message = $"Note of {UserId} is not updated"});
            }
        }

        [HttpPost]
        [Route("pinroute")]

        public ActionResult IsPinOrNot(int noteid)
        {
            int UserId = int.Parse(User.FindFirst("Userid").Value);
            var result= noteBusiness.IsPinOrNot(noteid,UserId);
            if (result)
            {
                return Ok(new ResponseModel<bool> {Success=true, Message ="Note is Pinned",Data= result});
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note is UnPinned", Data = result });
            }
        }

        [HttpPost]
        [Route("archieveroute")]

        public ActionResult IsArchieveOrNot(int noteid, int Userid)
        {
            int UserId = int.Parse(User.FindFirst("Userid").Value);
            var result = noteBusiness.IsPinOrNot(noteid, UserId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Note is Archieved", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note is UnArchieved", Data = result });
            }
        }

        [HttpPost]
        [Route("trashroute")]
        public ActionResult IsTrashorNot(int noteid)
        {
            int UserId = int.Parse(User.FindFirst("Userid").Value);
            var result = noteBusiness.IsTrashorNot(noteid, UserId);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Note is send to trash", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note is not send to trash", Data = result });
            }
        }

        [HttpPost]
        [Route("colorroute")]

        public ActionResult IsColor(int noteid,string UpdateColor)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result = noteBusiness.IsColor(noteid,Userid, UpdateColor);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Color is Updated ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Color is not Updated", Data = result });
            }
        }

        [HttpPost]
        [Route("deleteNote")]

        public ActionResult DeleteNotes(int noteid)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result = noteBusiness.DeleteForever(noteid,Userid);
            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Note is deleted successfully ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note is not found", Data = result });
            }
        }
    }
}
