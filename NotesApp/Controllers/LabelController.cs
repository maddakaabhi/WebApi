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
            var result=labelBusiness.AddLabel(labelModel, Userid);
            if (result != null)
            {
                return Ok(new ResponseModel<LabelEntity> { Success = true, Message = "Label added successfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<LabelEntity> { Success = false, Message = "Label not added" });
            }

        }

        [HttpGet]
        [Route("getalllabels")]
        public List<LabelEntity> GetLabels()
        {
            int Userid = int.Parse(User.FindFirst("Userid").Value);
            var result = labelBusiness.GetLabels(Userid);
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }


        }
    }
}
