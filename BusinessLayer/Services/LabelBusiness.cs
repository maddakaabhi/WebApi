using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBusiness : ILabelBusiness
    {
        private readonly ILabelRepo labelRepo;
        public LabelBusiness(ILabelRepo labelRepo)
        {
            this.labelRepo = labelRepo;
        }

        public LabelEntity AddLabel(LabelModel labelModel, int Userid)
        {
            return labelRepo.AddLabel(labelModel, Userid);
        }
        public List<LabelEntity> GetLabels(int Userid, int noteid)
        {
            return labelRepo.GetLabels(Userid,noteid);
        }
        public LabelEntity UpdateLabel(LabelModel labelModel, int Labelid, int Userid)
        {
            return labelRepo.UpdateLabel(labelModel, Labelid, Userid);
        }

        public bool DeleteLabel(int Labelid, int noteid, int userId)
        {
            return labelRepo.DeleteLabel(Labelid, noteid, userId);
        }
    }
}
