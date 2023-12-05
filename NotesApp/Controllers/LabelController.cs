using BusinessLayer.Interfaces;
using BusinessLayer.Services;
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
    public class LabelController : ControllerBase
    {
        private readonly ILabelBusiness labelBusiness;
        public LabelController(ILabelBusiness labelBusiness)
        {
            this.labelBusiness = labelBusiness;
        }
        [HttpPost]
        [Route("addlabel")]

        public ActionResult addlabel(LabelModel labelModel)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result = labelBusiness.AddLabel(labelModel, Userid);
            if (result != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label added successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Label already exists" });
            }

        }

        [HttpGet]
        [Route("getalllabels")]
        public List<LabelEntity> GetLabels(int noteid)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result = labelBusiness.GetLabels(Userid, noteid );
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }


        }
        [HttpPost]
        [Route("updatelabel")]

        public ActionResult updatelabel(LabelModel labelModel, int LabelId)
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result = labelBusiness.UpdateLabel(labelModel, LabelId, Userid);
            if (result != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Labename updated successfully", Data = result });
            }
            else
            {
                return Ok(new ResponseModel<LabelEntity> { Success = false, Message = "Labelname not updated" });
            }
        }

        [HttpPost]
        [Route("deletelabel")]

        public ActionResult deletelabel(int LabelId,int noteid)
        {
            int Userid=int.Parse(User.FindFirst("Userid").Value);
            var result= labelBusiness.DeleteLabel(LabelId, noteid, Userid);

            if (result)
            {
                return Ok(new ResponseModel<bool> { Success = true, Message = "Label is deleted", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<bool> { Success = true, Message = "Label not found" }); 
            }
        }

        
    }
}

