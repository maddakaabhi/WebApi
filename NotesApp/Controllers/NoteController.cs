﻿using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using System;
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

        [AllowAnonymous]
        [HttpPost]
        [Route("addnotes")]
        public ActionResult AddNotes(NotesModel notesModel)
        {
            int UserId = int.Parse(User.FindFirst("Userid").Value);
            //int UserId =(int)HttpContext.Session.GetInt32("UserId");
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
                return Ok(new ResponseModel<bool> { Success = true, Message = "Note deleted successfully ", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = false, Message = "Note not found", Data = result });
            }
        }

        [HttpPost]
        [Route("uploadimage")]
        public ActionResult uploadimage(int noteid,IFormFile img)
        {
            int Userid=int.Parse(User.FindFirst("Userid").Value);
            var result=noteBusiness.UploadImage(noteid,Userid,img);
            if( result != null )
            {
                return Ok(new ResponseModel<string> { Success = true,Message="Image Uploaded",Data = result});
            }
            else
            {
                return BadRequest(new ResponseModel<string> { Success = true, Message = "Image not Uploaded", Data = result });
            }
        }

        [HttpPost]
        [Route("updateremainder")]

        public ActionResult updateremainder(int noteid,DateTime updateremainder)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result=noteBusiness.UpdateRemainder(noteid,updateremainder,Userid);
            if( result != null )
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true,Message="Remainder is updated",Data=result});

            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Remainder not found" }); 
            }
        }

        [HttpGet]
        [Route("getnote")]
        public ActionResult getnote(int noteid)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result = noteBusiness.getNote(Userid,noteid);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Details of Note", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note not found" });
            }


        }

        [HttpGet]
        [Route("getnotebydate")]
        public ActionResult getnotebydate(DateTime date)
        {
            
            var result = noteBusiness.GetNotebydate(date);
            if (result != null)
            {
                return Ok(new ResponseModel<NoteEntity> { Success = true, Message = "Details of Note", Data = result });

            }
            else
            {
                return BadRequest(new ResponseModel<NoteEntity> { Success = false, Message = "Note not found" });
            }


        }


    }
}
