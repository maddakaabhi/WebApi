using ModelLayer.Models;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBusiness
    {
        LabelEntity AddLabel(LabelModel labelModel, int Userid);
        List<LabelEntity> GetLabels(int Userid);
    }
}