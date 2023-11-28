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
        public List<LabelEntity> GetLabels(int Userid)
        {
            return labelRepo.GetLabels(Userid);
        }
    }
}
